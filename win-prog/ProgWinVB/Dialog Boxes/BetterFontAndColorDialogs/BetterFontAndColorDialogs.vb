'----------------------------------------------------------
' BetterFontAndColorDialogs.vb (c) 2002 by Charles Petzold
'----------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BetterFontAndColorDialogs
    Inherits Form

    Protected clrdlg As ColorDialog = New ColorDialog()

    Shared Sub Main()
        Application.Run(New BetterFontAndColorDialogs())
    End Sub

    Sub New()
        Text = "Better Font and Color Dialogs"

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
        fntdlg.ShowApply = True
        AddHandler fntdlg.Apply, AddressOf FontDialogOnApply

        If fntdlg.ShowDialog() = DialogResult.OK Then
            Font = fntdlg.Font
            ForeColor = fntdlg.Color
            Invalidate()
        End If
    End Sub

    Sub MenuColorOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        clrdlg.Color = BackColor

        If clrdlg.ShowDialog() = DialogResult.OK Then
            BackColor = clrdlg.Color
        End If
    End Sub

    Sub FontDialogOnApply(ByVal obj As Object, ByVal ea As EventArgs)
        Dim fntdlg As FontDialog = DirectCast(obj, FontDialog)
        Font = fntdlg.Font
        ForeColor = fntdlg.Color
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("Hello common dialog boxes!", Font, _
                        New SolidBrush(ForeColor), 0, 0)
    End Sub
End Class
