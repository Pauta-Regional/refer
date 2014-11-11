'---------------------------------------
' Spiral.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Spiral
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New Spiral())
    End Sub

    Sub New()
        Text = "Spiral"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const iNumRevs As Integer = 20
        Dim iNumPoints As Integer = iNumRevs * 2 * (cx + cy)
        Dim aptf(iNumPoints) As PointF
        Dim rAngle, rScale As Double
        Dim i As Integer

        For i = 0 To iNumPoints
            rAngle = i * 2 * Math.PI / (iNumPoints / iNumRevs)
            rScale = 1 - i / iNumPoints
            aptf(i).X = CSng(cx / 2 * (1 + rScale * Math.Cos(rAngle)))
            aptf(i).Y = CSng(cy / 2 * (1 + rScale * Math.Sin(rAngle)))
        Next i

        grfx.DrawLines(New Pen(clr), aptf)
    End Sub
End Class
