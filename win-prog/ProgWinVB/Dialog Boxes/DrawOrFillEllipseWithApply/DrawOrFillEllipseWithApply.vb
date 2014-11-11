'-----------------------------------------------------------
' DrawOrFillEllipseWithApply.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DrawOrFillEllipseWithApply
    Inherits Form

    Private clrEllipse As Color = Color.Red
    Private bFillEllipse As Boolean = False

    Shared Sub Main()
        Application.Run(New DrawOrFillEllipseWithApply())
    End Sub

    Sub New()
        Text = "Draw or Fill Ellipse with Apply"
        ResizeRedraw = True

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Options")
        Menu.MenuItems(0).MenuItems.Add("&Color...", _
                            AddressOf MenuColorOnClick)
    End Sub

    Sub MenuColorOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As ColorFillDialogBoxWithApply = _
                            New ColorFillDialogBoxWithApply()
        dlg.ShowApply = True
        AddHandler dlg.Apply, AddressOf ColorFillDialogOnApply

        dlg.Color = clrEllipse
        dlg.Fill = bFillEllipse

        If dlg.ShowDialog() = DialogResult.OK Then
            clrEllipse = dlg.Color
            bFillEllipse = dlg.Fill
            Invalidate()
        End If
    End Sub

    Sub ColorFillDialogOnApply(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As ColorFillDialogBoxWithApply = _
                    DirectCast(obj, ColorFillDialogBoxWithApply)
        clrEllipse = dlg.Color
        bFillEllipse = dlg.Fill
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim rect As New Rectangle(0, 0, ClientSize.Width - 1, _
                                        ClientSize.Height - 1)
        If bFillEllipse Then
            grfx.FillEllipse(New SolidBrush(clrEllipse), rect)
        Else
            grfx.DrawEllipse(New Pen(clrEllipse), rect)
        End If
    End Sub
End Class
