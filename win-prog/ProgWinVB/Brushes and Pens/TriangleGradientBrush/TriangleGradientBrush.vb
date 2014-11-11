'------------------------------------------------------
' TriangleGradientBrush.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TriangleGradientBrush
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TriangleGradientBrush())
    End Sub

    Sub New()
        Text = "Triangle Gradient Brush"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim apt() As Point = {New Point(cx, 0), _
                              New Point(cx, cy), _
                              New Point(0, cy)}
        Dim pgbr As New PathGradientBrush(apt)
        grfx.FillRectangle(pgbr, 0, 0, cx, cy)
    End Sub
End Class
