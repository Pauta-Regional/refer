'----------------------------------------------------------
' BouncingGradientBrushBall.vb (c) 2002 by Charles Petzold
'----------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class BouncingGradientBrushBall
    Inherits Bounce

    Shared Shadows Sub Main()
        Application.Run(New BouncingGradientBrushBall())
    End Sub

    Sub New()
        Text = "Bouncing Gradient Brush Ball"
    End Sub

    Protected Overrides Sub DrawBall(ByVal grfx As Graphics, _
                                     ByVal rect As Rectangle)
        Dim path As New GraphicsPath()
        path.AddEllipse(rect)

        Dim pgbr As New PathGradientBrush(path)
        pgbr.CenterPoint = New PointF((rect.Left + rect.Right) \ 3, _
                                      (rect.Top + rect.Bottom) \ 3)
        pgbr.CenterColor = Color.White
        pgbr.SurroundColors = New Color() {Color.Red}
        grfx.FillRectangle(pgbr, rect)
    End Sub
End Class
