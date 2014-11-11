'---------------------------------------------
' StandardMenu.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class StandardMenu
    Inherits Form

    Private miFileOpen, miFileSave As MenuItem
    Private miEditCut, miEditCopy, miEditPaste As MenuItem

    ' Experimental variables for Popup code
    Private bDocumentPresent As Boolean = True
    Private bValidSelection As Boolean = True
    Private bStuffInClipboard As Boolean = False

    Shared Sub Main()
        Application.Run(New StandardMenu())
    End Sub

    Sub New()
        Text = "Standard Menu"
        Menu = New MainMenu()

        ' File
        Dim mi As New MenuItem("&File")
        AddHandler mi.Popup, AddressOf MenuFileOnPopup
        Dim index As Integer = Menu.MenuItems.Add(mi)

        ' File Open
        miFileOpen = New MenuItem("&Open...")
        AddHandler miFileOpen.Click, AddressOf MenuFileOpenOnClick
        miFileOpen.Shortcut = Shortcut.CtrlO
        Menu.MenuItems(index).MenuItems.Add(miFileOpen)

        ' File Save
        miFileSave = New MenuItem("&Save")
        AddHandler miFileSave.Click, AddressOf MenuFileSaveOnClick
        miFileSave.Shortcut = Shortcut.CtrlS
        Menu.MenuItems(index).MenuItems.Add(miFileSave)

        ' Horizontal line
        mi = New MenuItem("-")
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' File Exit
        mi = New MenuItem("E&xit")
        AddHandler mi.Click, AddressOf MenuFileExitOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Edit
        mi = New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        index = Menu.MenuItems.Add(mi)

        ' Edit Cut
        miEditCut = New MenuItem("Cu&t")
        AddHandler miEditCut.Click, AddressOf MenuEditCutOnClick
        miEditCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(index).MenuItems.Add(miEditCut)

        ' Edit Copy
        miEditCopy = New MenuItem("&Copy")
        AddHandler miEditCopy.Click, AddressOf MenuEditCopyOnClick
        miEditCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(index).MenuItems.Add(miEditCopy)

        ' Edit Paste
        miEditPaste = New MenuItem("&Paste")
        AddHandler miEditPaste.Click, AddressOf MenuEditCopyOnClick
        miEditPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(index).MenuItems.Add(miEditPaste)

        ' Help
        mi = New MenuItem("&Help")
        index = Menu.MenuItems.Add(mi)

        ' Help About
        mi = New MenuItem("&About StandardMenu...")
        AddHandler mi.Click, AddressOf MenuHelpAboutOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)
    End Sub

    Private Sub MenuFileOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miFileSave.Enabled = bDocumentPresent
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miEditCut.Enabled = bValidSelection
        miEditCopy.Enabled = bValidSelection
        miEditPaste.Enabled = bStuffInClipboard
    End Sub

    Private Sub MenuFileOpenOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        MessageBox.Show("This should be a File Open dialog box!", Text)
    End Sub

    Private Sub MenuFileSaveOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        MessageBox.Show("This should be a File Save dialog box!", Text)
    End Sub

    Private Sub MenuFileExitOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Close()
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        ' Copy selection to Clipboard; delete from document.
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        ' Copy selection to Clipboard.
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        ' Copy Clipboard data to document.
    End Sub

    Private Sub MenuHelpAboutOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        MessageBox.Show("StandardMenu " & Chr(169) & _
                        " 2002 by Charles Petzold", Text)
    End Sub
End Class
