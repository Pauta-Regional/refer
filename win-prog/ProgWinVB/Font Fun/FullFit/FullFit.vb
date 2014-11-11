'-------------------------------------------
' FullFit.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class FullFit
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New FullFit())
    End Sub

    Sub New()
        Text = "Full Fit"
        strText = "Full Fit"
        fnt = New Font("Times New Roman", 108)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       100, New Point(0, 0), New StringFormat())

        ' Set the world transform.
        Dim rectfBounds As RectangleF = path.GetBounds()
        Dim aptfDest() As PointF = {New PointF(0, 0), New PointF(cx, 0), _
                                                      New PointF(0, cy)}
        grfx.Transform = New Matrix(rectfBounds, aptfDest)

        ' Fill the path.
        grfx.FillPath(New SolidBrush(clr), path)
    End Sub
End Class
