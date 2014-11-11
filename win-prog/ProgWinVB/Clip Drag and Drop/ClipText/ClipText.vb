'-----------------------------------------
' ClipText.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ClipText
    Inherits Form

    Private strText As String = "Sample text for the clipboard"
    Private miCut, miCopy, miPaste As MenuItem

    Shared Sub Main()
        Application.Run(New ClipText())
    End Sub

    Sub New()
        Text = "Clip Text"
        ResizeRedraw = True

        Menu = New MainMenu()

        ' Edit menu
        Dim mi As New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        Menu.MenuItems.Add(mi)

        ' Edit Cut menu item
        miCut = New MenuItem("Cu&t")
        AddHandler miCut.Click, AddressOf MenuEditCutOnClick
        miCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(0).MenuItems.Add(miCut)

        ' Edit Copy menu item
        miCopy = New MenuItem("&Copy")
        AddHandler miCopy.Click, AddressOf MenuEditCopyOnClick
        miCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(0).MenuItems.Add(miCopy)

        ' Edit Paste menu item
        miPaste = New MenuItem("&Paste")
        AddHandler miPaste.Click, AddressOf MenuEditPasteOnClick
        miPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(0).MenuItems.Add(miPaste)
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miCopy.Enabled = strText.Length > 0
        miCut.Enabled = miCopy.Enabled
        miPaste.Enabled = _
                Clipboard.GetDataObject().GetDataPresent(GetType(String))
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        MenuEditCopyOnClick(obj, ea)
        strText = ""
        Invalidate()
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Clipboard.SetDataObject(strText, True)
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(String)) Then
            strText = DirectCast(data.GetData(GetType(String)), String)
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim strfmt As New StringFormat()
        strfmt.LineAlignment = StringAlignment.Center
        strfmt.Alignment = StringAlignment.Center

        grfx.DrawString(strText, Font, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
