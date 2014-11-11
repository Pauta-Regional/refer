'--------------------------------------------------
' HollowFontWidePen.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HollowFontWidePen
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New HollowFontWidePen())
    End Sub

    Sub New()
        Text = "Hollow Font (Wide Pen)"
        Width *= 2
        strText = "Wide Pen"
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

        ' Draw the path.
        Dim br As New HatchBrush(HatchStyle.Trellis, _
                                 Color.White, Color.Black)
        Dim pn As New Pen(br, fFontSize / 20)
        grfx.DrawPath(pn, path)
    End Sub
End Class
