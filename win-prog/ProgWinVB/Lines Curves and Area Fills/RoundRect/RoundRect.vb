'------------------------------------------
' RoundRect.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RoundRect
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New RoundRect())
    End Sub

    Sub New()
        Text = "Rounded Rectangle"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        RoundedRectangle(grfx, Pens.Red, _
                         New Rectangle(0, 0, cx - 1, cy - 1), _
                         New Size(cx \ 5, cy \ 5))
    End Sub

    Private Sub RoundedRectangle(ByVal grfx As Graphics, ByVal pn As Pen, _
                     ByVal rect As Rectangle, ByVal sz As Size)
        grfx.DrawLine(pn, rect.Left + sz.Width \ 2, rect.Top, _
                          rect.Right - sz.Width \ 2, rect.Top)
        grfx.DrawArc(pn, rect.Right - sz.Width, rect.Top, _
                         sz.Width, sz.Height, 270, 90)
        grfx.DrawLine(pn, rect.Right, rect.Top + sz.Height \ 2, _
                          rect.Right, rect.Bottom - sz.Height \ 2)
        grfx.DrawArc(pn, rect.Right - sz.Width, _
                         rect.Bottom - sz.Height, _
                         sz.Width, sz.Height, 0, 90)
        grfx.DrawLine(pn, rect.Right - sz.Width \ 2, rect.Bottom, _
                          rect.Left + sz.Width \ 2, rect.Bottom)
        grfx.DrawArc(pn, rect.Left, rect.Bottom - sz.Height, _
                         sz.Width, sz.Height, 90, 90)
        grfx.DrawLine(pn, rect.Left, rect.Bottom - sz.Height \ 2, _
                          rect.Left, rect.Top + sz.Height \ 2)
        grfx.DrawArc(pn, rect.Left, rect.Top, _
                         sz.Width, sz.Height, 180, 90)
    End Sub
End Class
