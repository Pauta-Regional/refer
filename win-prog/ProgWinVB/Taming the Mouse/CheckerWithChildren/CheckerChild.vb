'---------------------------------------------
' CheckerChild.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckerChild
    Inherits UserControl

    Private bChecked As Boolean = False

    Sub New()
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnClick(ByVal ea As EventArgs)
        MyBase.OnClick(ea)
        bChecked = Not bChecked
        Invalidate()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        If kea.KeyCode = Keys.Enter OrElse kea.KeyCode = Keys.Space Then
            OnClick(New EventArgs())
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)
        grfx.DrawRectangle(pn, 0, 0, Width - 1, Height - 1)
        If bChecked Then
            grfx.DrawLine(pn, 0, 0, ClientSize.Width, ClientSize.Height)
            grfx.DrawLine(pn, 0, ClientSize.Height, ClientSize.Width, 0)
        End If
    End Sub
End Class
