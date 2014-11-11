'--------------------------------------------
' PolyEllipse.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PolyEllipse
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New PolyEllipse())
    End Sub

    Sub New()
        Text = "Ellipse with DrawLines"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim iNum As Integer = 2 * (cx + cy)
        Dim aptf(iNum) As PointF
        Dim i As Integer

        For i = 0 To iNum
            Dim rAng As Double = i * 2 * Math.PI / iNum
            aptf(i).X = CSng((cx - 1) / 2 * (1 + Math.Cos(rAng)))
            aptf(i).Y = CSng((cy - 1) / 2 * (1 + Math.Sin(rAng)))
        Next i

        grfx.DrawLines(New Pen(clr), aptf)
    End Sub
End Class
