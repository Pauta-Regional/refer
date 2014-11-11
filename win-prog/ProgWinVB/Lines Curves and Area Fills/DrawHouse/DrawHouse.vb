'------------------------------------------
' DrawHouse.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DrawHouse
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New DrawHouse())
    End Sub

    Sub New()
        Text = "Draw a House in One Line"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawLines(New Pen(clr), _
                       New Point() _
                       { _
                           New Point(cx \ 4, 3 * cy \ 4), _
                           New Point(cx \ 4, cy \ 2), _
                           New Point(cx \ 2, cy \ 4), _
                           New Point(3 * cx \ 4, cy \ 2), _
                           New Point(3 * cx \ 4, 3 * cy \ 4), _
                           New Point(cx \ 4, cy \ 2), _
                           New Point(3 * cx \ 4, cy \ 2), _
                           New Point(cx \ 4, 3 * cy \ 4), _
                           New Point(3 * cx \ 4, 3 * cy \ 4) _
                       })
    End Sub
End Class
