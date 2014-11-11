'-----------------------------------------------------
' ClosedCurveFillModes.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class ClosedCurveFillModes
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New ClosedCurveFillModes())
    End Sub

    Private Sub New()
        Text = "FillClosedCurve Fill Modes"
        ClientSize = New Size(2 * ClientSize.Height, ClientSize.Height)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim apt(4) As Point
        Dim i As Integer

        For i = 0 To apt.GetUpperBound(0)
            Dim rAngle As Double = (i * 0.8 - 0.5) * Math.PI
            apt(i) = New Point( _
                        CInt(cx * (0.25 + 0.24 * Math.Cos(rAngle))), _
                        CInt(cy * (0.5 + 0.48 * Math.Sin(rAngle))))
        Next i

        grfx.FillClosedCurve(br, apt, FillMode.Alternate)

        For i = 0 To apt.GetUpperBound(0)
            apt(i).X += cx \ 2
        Next i

        grfx.FillClosedCurve(br, apt, FillMode.Winding)
    End Sub
End Class
