'-----------------------------------------
' WrapText.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class WrapText
    Inherits FontMenuForm

    Const fRadius As Single = 100

    Shared Shadows Sub Main()
        Application.Run(New WrapText())
    End Sub

    Sub New()
        Text = "Wrap Text"
        strText = "e snake ate the tail of th"
        fnt = New Font("Times New Roman", 48)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim fFontSize As Single = PointsToPageUnits(grfx, fnt)

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, New PointF(0, 0), New StringFormat())

        ' Shift the origin to left baseline, y increasing up.
        Dim rectf As RectangleF = path.GetBounds()
        path.Transform(New Matrix(1, 0, 0, -1, -rectf.Left, _
                                        GetAscent(grfx, fnt)))

        ' Scale so width equals 2*PI.
        Dim fScale As Single = CSng(2 * Math.PI / rectf.Width)
        path.Transform(New Matrix(fScale, 0, 0, fScale, 0, 0))

        ' Modify the path.
        Dim aptf() As PointF = path.PathPoints
        Dim i As Integer
        For i = 0 To aptf.GetUpperBound(0)
            aptf(i) = New PointF( _
                CSng(fRadius * (1 + aptf(i).Y) * Math.Cos(aptf(i).X)), _
                CSng(fRadius * (1 + aptf(i).Y) * Math.Sin(aptf(i).X)))
        Next i
        path = New GraphicsPath(aptf, path.PathTypes)

        ' Fill the path.
        grfx.TranslateTransform(cx \ 2, cy \ 2)
        grfx.FillPath(New SolidBrush(clr), path)
    End Sub
End Class
