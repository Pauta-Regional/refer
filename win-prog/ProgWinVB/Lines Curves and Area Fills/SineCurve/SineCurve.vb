'------------------------------------------
' SineCurve.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SineCurve
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New SineCurve())
    End Sub

    Sub New()
        Text = "Sine Curve"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim aptf(cx - 1) As PointF
        Dim i As Integer

        For i = 0 To cx - 1
            aptf(i).X = i
            aptf(i).Y = CSng(((cy - 1) / 2) * _
                             (1 - Math.Sin(i * 2 * Math.PI / (cx - 1))))
        Next i

        grfx.DrawLines(New Pen(clr), aptf)
    End Sub
End Class
