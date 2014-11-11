'-----------------------------------------------------
' NotepadCloneWithEdit.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NotepadCloneWithEdit
    Inherits NotepadCloneWithFile

    Private miEditUndo, miEditCut, miEditCopy As MenuItem
    Private miEditPaste, miEditDelete As MenuItem
    Private strFind As String = ""
    Private strReplace As String = ""
    Private bMatchCase As Boolean = False
    Private bFindDown As Boolean = True

    Shared Shadows Sub Main()
        Application.Run(New NotepadCloneWithEdit())
    End Sub

    Sub New()
        strProgName = "Notepad Clone with Edit"
        MakeCaption()

        ' Edit menu
        Dim mi As New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        Menu.MenuItems.Add(mi)
        Dim index As Integer = Menu.MenuItems.Count - 1

        ' Edit Undo menu item
        miEditUndo = New MenuItem("&Undo")
        AddHandler miEditUndo.Click, AddressOf MenuEditUndoOnClick
        miEditUndo.Shortcut = Shortcut.CtrlZ
        Menu.MenuItems(index).MenuItems.Add(miEditUndo)
        Menu.MenuItems(index).MenuItems.Add("-")

        ' Edit Cut menu item
        miEditCut = New MenuItem("Cu&t")
        AddHandler miEditCut.Click, AddressOf MenuEditCutOnClick
        miEditCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(index).MenuItems.Add(miEditCut)

        ' Edit Copy menu item
        miEditCopy = New MenuItem("&Copy")
        AddHandler miEditCopy.Click, AddressOf MenuEditCopyOnClick
        miEditCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(index).MenuItems.Add(miEditCopy)

        ' Edit Paste menu item
        miEditPaste = New MenuItem("&Paste")
        AddHandler miEditPaste.Click, AddressOf MenuEditPasteOnClick
        miEditPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(index).MenuItems.Add(miEditPaste)

        ' Edit Delete menu item
        miEditDelete = New MenuItem("De&lete")
        AddHandler miEditDelete.Click, AddressOf MenuEditDeleteOnClick
        miEditDelete.Shortcut = Shortcut.Del
        Menu.MenuItems(index).MenuItems.Add(miEditDelete)
        Menu.MenuItems(index).MenuItems.Add("-")

        ' Edit Find menu item
        mi = New MenuItem("&Find...")
        AddHandler mi.Click, AddressOf MenuEditFindOnClick
        mi.Shortcut = Shortcut.CtrlF
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Edit Find Next menu item
        mi = New MenuItem("Find &Next")
        AddHandler mi.Click, AddressOf MenuEditFindNextOnClick
        mi.Shortcut = Shortcut.F3
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Edit Replace menu item
        mi = New MenuItem("&Replace...")
        AddHandler mi.Click, AddressOf MenuEditReplaceOnClick
        mi.Shortcut = Shortcut.CtrlH
        Menu.MenuItems(index).MenuItems.Add(mi)
        Menu.MenuItems(index).MenuItems.Add("-")

        ' Edit Select All menu item
        mi = New MenuItem("Select &All")
        AddHandler mi.Click, AddressOf MenuEditSelectAllOnClick
        mi.Shortcut = Shortcut.CtrlA
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Edit Time/Date menu item
        mi = New MenuItem("Time/&Date")
        AddHandler mi.Click, AddressOf MenuEditTimeDateOnClick
        mi.Shortcut = Shortcut.F5
        Menu.MenuItems(index).MenuItems.Add(mi)
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miEditUndo.Enabled = txtbox.CanUndo

        miEditCopy.Enabled = txtbox.SelectionLength > 0
        miEditCut.Enabled = miEditCopy.Enabled
        miEditDelete.Enabled = miEditCopy.Enabled

        miEditPaste.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(GetType(String))
    End Sub

    Private Sub MenuEditUndoOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        txtbox.Undo()
        txtbox.ClearUndo()
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        txtbox.Cut()
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        txtbox.Copy()
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        txtbox.Paste()
    End Sub

    Private Sub MenuEditDeleteOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        txtbox.Clear()
    End Sub

    Private Sub MenuEditFindOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        If OwnedForms.Length > 0 Then Return

        txtbox.HideSelection = False
        Dim dlg As New FindDialog()
        dlg.Owner = Me
        dlg.FindText = strFind
        dlg.MatchCase = bMatchCase
        dlg.FindDown = bFindDown
        AddHandler dlg.FindNext, AddressOf FindDialogOnFindNext
        AddHandler dlg.CloseDlg, AddressOf FindReplaceDialogOnCloseDlg
        dlg.Show()
    End Sub

    Private Sub MenuEditFindNextOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        If strFind.Length = 0 Then
            If OwnedForms.Length > 0 Then Return
            MenuEditFindOnClick(obj, ea)
        Else
            FindNext()
        End If
    End Sub

    Private Sub MenuEditReplaceOnClick(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        If OwnedForms.Length > 0 Then Return

        txtbox.HideSelection = False
        Dim dlg As New ReplaceDialog()
        dlg.Owner = Me
        dlg.FindText = strFind
        dlg.ReplaceText = strReplace
        dlg.MatchCase = bMatchCase
        dlg.FindDown = bFindDown
        AddHandler dlg.FindNext, AddressOf FindDialogOnFindNext
        AddHandler dlg.Replace, AddressOf ReplaceDialogOnReplace
        AddHandler dlg.ReplaceAll, AddressOf ReplaceDialogOnReplaceAll
        AddHandler dlg.CloseDlg, AddressOf FindReplaceDialogOnCloseDlg
        dlg.Show()
    End Sub

    Private Sub MenuEditSelectAllOnClick(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        txtbox.SelectAll()
    End Sub

    Private Sub MenuEditTimeDateOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        Dim dt As DateTime = DateTime.Now
        txtbox.SelectedText = dt.ToString("t") & " " & dt.ToString("d")
    End Sub

    Private Sub FindDialogOnFindNext(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim dlg As FindReplaceDialog = DirectCast(obj, FindReplaceDialog)
        strFind = dlg.FindText
        bMatchCase = dlg.MatchCase
        bFindDown = dlg.FindDown
        FindNext()
    End Sub

    Private Function FindNext() As Boolean
        If bFindDown Then
            Dim iStart As Integer = txtbox.SelectionStart + _
                                    txtbox.SelectionLength
            While iStart + strFind.Length <= txtbox.TextLength
                If String.Compare(strFind, 0, txtbox.Text, iStart, _
                                  strFind.Length, Not bMatchCase) = 0 Then
                    txtbox.SelectionStart = iStart
                    txtbox.SelectionLength = strFind.Length
                    Return True
                End If
                iStart += 1
            End While
        Else
            Dim iStart As Integer = txtbox.SelectionStart - strFind.Length
            While iStart >= 0
                If String.Compare(strFind, 0, txtbox.Text, iStart, _
                                  strFind.Length, Not bMatchCase) = 0 Then
                    txtbox.SelectionStart = iStart
                    txtbox.SelectionLength = strFind.Length
                    Return True
                End If
                iStart -= 1
            End While
        End If
        MessageBox.Show("Cannot find """ & strFind & """", strProgName, _
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return False
    End Function

    Private Sub ReplaceDialogOnReplace(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        Dim dlg As FindReplaceDialog = DirectCast(obj, FindReplaceDialog)
        strFind = dlg.FindText
        strReplace = dlg.ReplaceText
        bMatchCase = dlg.MatchCase
        If String.Compare(strFind, txtbox.SelectedText, _
                          Not bMatchCase) = 0 Then
            txtbox.SelectedText = strReplace
        End If
        FindNext()
    End Sub

    Private Sub ReplaceDialogOnReplaceAll(ByVal obj As Object, _
                                          ByVal ea As EventArgs)
        Dim dlg As FindReplaceDialog = DirectCast(obj, FindReplaceDialog)
        Dim str As String = txtbox.Text
        strFind = dlg.FindText
        strReplace = dlg.ReplaceText
        bMatchCase = dlg.MatchCase
        If bMatchCase Then
            str = str.Replace(strFind, strReplace)
        Else
            Dim i As Integer
            For i = 0 To str.Length - strFind.Length - 1 Step 0
                If String.Compare(str, i, strFind, 0, _
                                  strFind.Length, True) = 0 Then
                    str = str.Remove(i, strFind.Length)
                    str = str.Insert(i, strReplace)
                    i += strReplace.Length
                Else
                    i += 1
                End If
            Next i
        End If
        If str <> txtbox.Text Then
            txtbox.Text = str
            txtbox.SelectionStart = 0
            txtbox.SelectionLength = 0
            txtbox.Modified = True
        End If
    End Sub

    Private Sub FindReplaceDialogOnCloseDlg(ByVal obj As Object, _
                                            ByVal ea As EventArgs)
        txtbox.HideSelection = True
    End Sub
End Class
