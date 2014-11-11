'---------------------------------------
' Flower.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class Flower
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New Flower())
    End Sub

    Sub New()
        Text = "Flower"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)

        ' Draw green stem from lower left corner to center.
        grfx.DrawBezier(New Pen(Color.Green, 10), _
                New Point(0, cy), New Point(0, 3 * cy \ 4), _
                New Point(cx \ 4, cy \ 4), New Point(cx \ 2, cy \ 2))

        ' Set up transform for remainder of flower.
        Dim fScale As Single = Math.Min(cx, cy) / 2000.0F
        grfx.TranslateTransform(cx \ 2, cy \ 2)
        grfx.ScaleTransform(fScale, fScale)

        ' Draw red petals.
        Dim path As New GraphicsPath()
        path.AddBezier(New Point(0, 0), New Point(125, 125), _
                       New Point(475, 125), New Point(600, 0))
        path.AddBezier(New Point(600, 0), New Point(475, -125), _
                       New Point(125, -125), New Point(0, 0))

        Dim i As Integer
        For i = 0 To 7
            grfx.FillPath(Brushes.Red, path)
            grfx.DrawPath(Pens.Black, path)
            grfx.RotateTransform(360 \ 8)
        Next i

        ' Draw yellow circle in center.
        Dim rect As New Rectangle(-150, -150, 300, 300)
        grfx.FillEllipse(Brushes.Yellow, rect)
        grfx.DrawEllipse(Pens.Black, rect)
    End Sub
End Class
