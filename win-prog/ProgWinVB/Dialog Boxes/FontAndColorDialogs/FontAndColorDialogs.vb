'----------------------------------------------------
' FontAndColorDialogs.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FontAndColorDialogs
    Inherits Form

    Shared Sub Main()
        Application.Run(New FontAndColorDialogs())
    End Sub

    Sub New()
        Text = "Font and Color Dialogs"
        ResizeRedraw = True

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Format")
        Menu.MenuItems(0).MenuItems.Add("&Font...", _
                                  AddressOf MenuFontOnClick)
        Menu.MenuItems(0).MenuItems.Add("&Background Color...", _
                                  AddressOf MenuColorOnClick)
    End Sub

    Sub MenuFontOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim fntdlg As New FontDialog()
        fntdlg.Font = Font
        fntdlg.Color = ForeColor
        fntdlg.ShowColor = True

        If fntdlg.ShowDialog() = DialogResult.OK Then
            Font = fntdlg.Font
            ForeColor = fntdlg.Color
            Invalidate()
        End If
    End Sub

    Sub MenuColorOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim clrdlg As New ColorDialog()
        clrdlg.Color = BackColor

        If clrdlg.ShowDialog() = DialogResult.OK Then
            BackColor = clrdlg.Color
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        grfx.DrawString("Hello common dialog boxes!", Font, _
                        New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
