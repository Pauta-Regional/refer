'-----------------------------------------
' ClipText.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class ClipText
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New ClipText())
    End Sub

    Sub New()
        Text = "Clip Text"
        Width *= 2
        strText = "Clip Text"
        fnt = New Font("Times New Roman", 108, FontStyle.Bold)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim fFontSize As Single = PointsToPageUnits(grfx, fnt)

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, New PointF(0, 0), New StringFormat())

        ' Set the clipping region.
        grfx.SetClip(path)

        ' Get the path bounds and center the clipping region.
        Dim rectfBounds As RectangleF = path.GetBounds()
        grfx.TranslateClip( _
                    (cx - rectfBounds.Width) / 2 - rectfBounds.Left, _
                    (cy - rectfBounds.Height) / 2 - rectfBounds.Top)

        ' Draw clipped lines.
        Dim rand As New Random()
        Dim y As Integer
        For y = 0 To cy - 1
            Dim pn As New Pen(Color.FromArgb(rand.Next(255), _
                                             rand.Next(255), _
                                             rand.Next(255)))
            grfx.DrawBezier(pn, New Point(0, y), _
                                New Point(cx \ 3, y + cy \ 3), _
                                New Point(2 * cx \ 3, y - cy \ 3), _
                                New Point(cx, y))
        Next y
    End Sub
End Class
