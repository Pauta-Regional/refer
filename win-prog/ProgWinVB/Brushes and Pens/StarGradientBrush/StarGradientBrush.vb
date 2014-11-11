'--------------------------------------------------
' StarGradientBrush.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class StarGradientBrush
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New StarGradientBrush())
    End Sub

    Sub New()
        Text = "Star Gradient Brush"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim apt(4) As Point
        Dim i As Integer

        For i = 0 To apt.GetUpperBound(0)
            Dim rAngle As Double = (i * 0.8 - 0.5) * Math.PI
            apt(i).X = CInt(cx * (0.5 + 0.48 * Math.Cos(rAngle)))
            apt(i).Y = CInt(cy * (0.5 + 0.48 * Math.Sin(rAngle)))
        Next i

        Dim pgbr As New PathGradientBrush(apt)
        pgbr.CenterColor = Color.White
        pgbr.SurroundColors = New Color() {Color.Black}
        grfx.FillRectangle(pgbr, 0, 0, cx, cy)
    End Sub
End Class
