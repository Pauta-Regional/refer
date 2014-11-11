'-------------------------------------------------------
' HollowFontCenteredPath.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HollowFontCenteredPath
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New HollowFontCenteredPath())
    End Sub

    Sub New()
        Text = "Hollow Font (Centered Path)"
        Width *= 2
        strText = "Hollow"
        fnt = New Font("Times New Roman", 108)
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
        Dim pn As New Pen(clr, fFontSize / 50)
        pn.DashStyle = DashStyle.Dot
        grfx.DrawPath(pn, path)
    End Sub
End Class
