'--------------------------------------------------
' HollowFontWidened.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HollowFontWidened
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(new HollowFontWidened())
    End Sub

    Sub New()
        Text = "Hollow Font (Widened)"
        Width *= 2
        strText = "Widened"
        fnt = New Font("Times New Roman", 108, _
                       FontStyle.Bold Or FontStyle.Italic)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim fFontSize As Single = PointsToPageUnits(grfx, fnt)

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, New PointF(0, 0), New StringFormat())

        ' Get the path bounds for centering.
        Dim rectfBounds As RectangleF = path.GetBounds()
        grfx.TranslateTransform( _
                    (cx - rectfBounds.Width) / 2 - rectfBounds.Left, _
                    (cy - rectfBounds.Height) / 2 - rectfBounds.Top)

        ' Widen, fill, and draw the path.
        path.Widen(New Pen(Color.Black, fFontSize / 20))
        Dim br As New HatchBrush(HatchStyle.Trellis, _
                                 Color.White, Color.Black)
        grfx.DrawPath(New Pen(Color.Black, 2), path)
        grfx.FillPath(br, path)
    End Sub
End Class
