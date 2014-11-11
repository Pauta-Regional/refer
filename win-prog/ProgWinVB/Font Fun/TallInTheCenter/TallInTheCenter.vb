'------------------------------------------------
' TallInTheCenter.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TallInTheCenter
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New TallInTheCenter())
    End Sub

    Sub New()
        Text = "Tall in the Center"
        Width *= 2
        strText = Text
        fnt = New Font("Times New Roman", 48)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim fFontSize As Single = PointsToPageUnits(grfx, fnt)

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, New PointF(0, 0), New StringFormat())

        ' Shift the origin to the center of the path.
        Dim rectf As RectangleF = path.GetBounds()
        path.Transform(New Matrix(1, 0, 0, 1, _
                             -(rectf.Left + rectf.Right) / 2, _
                             -(rectf.Top + rectf.Bottom) / 2))
        rectf = path.GetBounds()

        ' Modify the path.
        Dim aptf() As PointF = path.PathPoints
        Dim i As Integer
        For i = 0 To aptf.GetUpperBound(0)
            aptf(i).Y *= 4 * (rectf.Width - Math.Abs(aptf(i).X)) / _
                                                            rectf.Width
        Next i
        path = New GraphicsPath(aptf, path.PathTypes)

        ' Fill the path.
        grfx.TranslateTransform(cx \ 2, cy \ 2)
        grfx.FillPath(New SolidBrush(clr), path)
    End Sub
End Class
