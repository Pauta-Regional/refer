'------------------------------------------
' BezierArt.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BezierArt
    Inherits PrintableForm

    Const iNum As Integer = 100

    Shared Shadows Sub Main()
        Application.Run(New BezierArt())
    End Sub

    Sub New()
        Text = "Bezier Art"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr)
        Dim aptf(3) As PointF
        Dim i As Integer

        For i = 0 To iNum - 1
            Dim rAngle As Double = 2 * i * Math.PI / iNum

            aptf(0).X = CSng(cx / 2 + cx / 2 * Math.Cos(rAngle))
            aptf(0).Y = CSng(5 * cy / 8 + cy / 16 * Math.Sin(rAngle))

            aptf(1) = New PointF(CSng(cx / 2), -cy)
            aptf(2) = New PointF(CSng(cx / 2), 2 * cy)

            rAngle += Math.PI

            aptf(3).X = CSng(cx / 2 + cx / 4 * Math.Cos(rAngle))
            aptf(3).Y = CSng(cy / 2 + cy / 16 * Math.Sin(rAngle))

            grfx.DrawBeziers(pn, aptf)
        Next i
    End Sub
End Class
