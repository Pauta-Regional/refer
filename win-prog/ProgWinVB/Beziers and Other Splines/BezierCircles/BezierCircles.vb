'----------------------------------------------
' BezierCircles.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BezierCircles
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New BezierCircles())
    End Sub

    Sub New()
        Text = "Bezier Circles"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim iRadius As Integer = Math.Min(cx - 1, cy - 1) \ 2

        ' Circle drawn normally
        grfx.DrawEllipse(New Pen(clr), cx \ 2 - iRadius, cy \ 2 - iRadius, _
                                       2 * iRadius, 2 * iRadius)

        ' Two-segment (180-degree) approximation
        Dim L As Integer = CInt(iRadius * 4.0F / 3 * Math.Tan(Math.PI / 4))
        Dim apt() As Point = { _
                         New Point(cx \ 2, cy \ 2 - iRadius), _
                         New Point(cx \ 2 + L, cy \ 2 - iRadius), _
                         New Point(cx \ 2 + L, cy \ 2 + iRadius), _
                         New Point(cx \ 2, cy \ 2 + iRadius), _
                         New Point(cx \ 2 - L, cy \ 2 + iRadius), _
                         New Point(cx \ 2 - L, cy \ 2 - iRadius), _
                         New Point(cx \ 2, cy \ 2 - iRadius) _
                         }
        grfx.DrawBeziers(Pens.Blue, apt)

        ' Four-segment (90-degree) approximation
        L = CInt(iRadius * 4.0F / 3 * Math.Tan(Math.PI / 8))
        apt = New Point() { _
                       New Point(cx \ 2, cy \ 2 - iRadius), _
                       New Point(cx \ 2 + L, cy \ 2 - iRadius), _
                       New Point(cx \ 2 + iRadius, cy \ 2 - L), _
                       New Point(cx \ 2 + iRadius, cy \ 2), _
                       New Point(cx \ 2 + iRadius, cy \ 2 + L), _
                       New Point(cx \ 2 + L, cy \ 2 + iRadius), _
                       New Point(cx \ 2, cy \ 2 + iRadius), _
                       New Point(cx \ 2 - L, cy \ 2 + iRadius), _
                       New Point(cx \ 2 - iRadius, cy \ 2 + L), _
                       New Point(cx \ 2 - iRadius, cy \ 2), _
                       New Point(cx \ 2 - iRadius, cy \ 2 - L), _
                       New Point(cx \ 2 - L, cy \ 2 - iRadius), _
                       New Point(cx \ 2, cy \ 2 - iRadius) _
                       }
        grfx.DrawBeziers(Pens.Red, apt)
    End Sub
End Class
