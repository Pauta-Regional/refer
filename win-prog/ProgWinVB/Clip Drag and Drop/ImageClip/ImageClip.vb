'------------------------------------------
' ImageClip.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Class ImageClip
    Inherits ImagePrint

    Private miCut, miCopy, miPaste, miDel As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New ImageClip())
    End Sub

    Sub New()
        strProgName = "Image Clip"
        Text = strProgName

        ' Edit menu
        Dim mi As New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        Menu.MenuItems.Add(mi)
        Dim index As Integer = Menu.MenuItems.Count - 1

        ' Edit Cut menu item
        miCut = New MenuItem("Cu&t")
        AddHandler miCut.Click, AddressOf MenuEditCutOnClick
        miCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(index).MenuItems.Add(miCut)

        ' Edit Copy menu item
        miCopy = New MenuItem("&Copy")
        AddHandler miCopy.Click, AddressOf MenuEditCopyOnClick
        miCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(index).MenuItems.Add(miCopy)

        ' Edit Paste menu item
        miPaste = New MenuItem("&Paste")
        AddHandler miPaste.Click, AddressOf MenuEditPasteOnClick
        miPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(index).MenuItems.Add(miPaste)

        ' Edit Delete menu item
        miDel = New MenuItem("De&lete")
        AddHandler miDel.Click, AddressOf MenuEditDelOnClick
        miDel.Shortcut = Shortcut.Del
        Menu.MenuItems(index).MenuItems.Add(miDel)
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        Dim bEnable As Boolean = Not img Is Nothing

        miCopy.Enabled = bEnable
        miCut.Enabled = bEnable
        miDel.Enabled = bEnable

        Dim data As IDataObject = Clipboard.GetDataObject()
        miPaste.Enabled = data.GetDataPresent(GetType(Bitmap)) OrElse _
                          data.GetDataPresent(GetType(Metafile))
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        MenuEditCopyOnClick(obj, ea)
        MenuEditDelOnClick(obj, ea)
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Clipboard.SetDataObject(img, True)
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()

        If data.GetDataPresent(GetType(Metafile)) Then
            img = DirectCast(data.GetData(GetType(Metafile)), Image)
        ElseIf data.GetDataPresent(GetType(Bitmap)) Then
            img = DirectCast(data.GetData(GetType(Bitmap)), Image)
        End If

        strFileName = "Clipboard"
        Text = strProgName & " - " & strFileName
        Invalidate()
    End Sub

    Private Sub MenuEditDelOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        img = Nothing
        strFileName = Nothing
        Text = strProgName
        Invalidate()
    End Sub
End Class
