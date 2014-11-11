'------------------------------------------------------
' CheckerChildWithFocus.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class CheckerChildWithFocus
    Inherits CheckerChild

    Protected Overrides Sub OnGotFocus(ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        MyBase.OnPaint(pea)
        If Focused Then
            Dim grfx As Graphics = pea.Graphics
            grfx.DrawRectangle(New Pen(ForeColor, 5), ClientRectangle)
        End If
    End Sub
End Class
