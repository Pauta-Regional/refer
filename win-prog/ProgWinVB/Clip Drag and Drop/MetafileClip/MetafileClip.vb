'---------------------------------------------
' MetafileClip.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Class MetafileClip
    Inherits MetafileConvert

    Private miCut, miCopy, miPaste, miDel As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New MetafileClip())
    End Sub

    Sub New()
        strProgName = "Metafile Clip"
        Text = strProgName

        ' Edit menu
        AddHandler Menu.MenuItems(1).Popup, AddressOf MenuEditOnPopup

        ' Edit Cut menu item
        miCut = New MenuItem("Cu&t")
        AddHandler miCut.Click, AddressOf MenuEditCutOnClick
        miCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(1).MenuItems.Add(miCut)

        ' Edit Copy menu item
        miCopy = New MenuItem("&Copy")
        AddHandler miCopy.Click, AddressOf MenuEditCopyOnClick
        miCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(1).MenuItems.Add(miCopy)

        ' Edit Paste menu item
        miPaste = New MenuItem("&Paste")
        AddHandler miPaste.Click, AddressOf MenuEditPasteOnClick
        miPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(1).MenuItems.Add(miPaste)

        ' Edit Delete menu item
        miDel = New MenuItem("De&lete")
        AddHandler miDel.Click, AddressOf MenuEditDelOnClick
        miDel.Shortcut = Shortcut.Del
        Menu.MenuItems(1).MenuItems.Add(miDel)
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        Dim bEnable As Boolean = Not mf Is Nothing

        miCopy.Enabled = bEnable
        miCut.Enabled = bEnable
        miDel.Enabled = bEnable
        miPaste.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(GetType(Metafile))
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        MenuEditCopyOnClick(obj, ea)
        MenuEditDelOnClick(obj, ea)
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Clipboard.SetDataObject(mf, True)
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()

        If data.GetDataPresent(GetType(Metafile)) Then
            mf = DirectCast(data.GetData(GetType(Metafile)), Metafile)
        End If

        strFileName = "clipboard"
        Text = strProgName & " - " & strFileName
        Invalidate()
    End Sub

    Private Sub MenuEditDelOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        mf = Nothing
        strFileName = Nothing
        Text = strProgName
        Invalidate()
    End Sub
End Class
