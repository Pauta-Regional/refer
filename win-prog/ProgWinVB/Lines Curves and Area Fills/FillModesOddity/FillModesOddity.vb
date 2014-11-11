'------------------------------------------------
' FillModesOddity.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class FillModesOddity
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New FillModesOddity())
    End Sub

    Sub New()
        Text = "Alternate and Winding Fill Modes (An Oddity)"
        ClientSize = New Size(2 * ClientSize.Height, ClientSize.Height)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim aptf() As PointF = { _
                        New PointF(0.1F, 0.7F), New PointF(0.5F, 0.7F), _
                        New PointF(0.5F, 0.1F), New PointF(0.9F, 0.1F), _
                        New PointF(0.9F, 0.5F), New PointF(0.3F, 0.5F), _
                        New PointF(0.3F, 0.9F), New PointF(0.7F, 0.9F), _
                        New PointF(0.7F, 0.3F), New PointF(0.1F, 0.3F)}
        Dim i As Integer

        For i = 0 To aptf.GetUpperBound(0)
            aptf(i).X *= cx \ 2
            aptf(i).Y *= cy
        Next i

        grfx.FillPolygon(br, aptf, FillMode.Alternate)

        For i = 0 To aptf.GetUpperBound(0)
            aptf(i).X += cx \ 2
        Next i

        grfx.FillPolygon(br, aptf, FillMode.Winding)
    End Sub
End Class
