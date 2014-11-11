'-----------------------------------------
' Infinity.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Infinity
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New Infinity())
    End Sub

    Sub New()
        Text = "Infinity Sign Using Bezier Splines"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        cx -= 1
        cy -= 1

        Dim apt() As Point = _
        { _
            New Point(0, cy \ 2), _
            New Point(0, 0), _
            New Point(cx \ 3, 0), _
            New Point(cx \ 2, cy \ 2), _
            New Point(2 * cx \ 3, cy), _
            New Point(cx, cy), _
            New Point(cx, cy \ 2), _
            New Point(cx, 0), _
            New Point(2 * cx \ 3, 0), _
            New Point(cx \ 2, cy \ 2), _
            New Point(cx \ 3, cy), _
            New Point(0, cy), _
            New Point(0, cy \ 2) _
        }
        grfx.DrawBeziers(New Pen(clr), apt)
    End Sub
End Class
