'--------------------------------------------------
' RotatedRectangles.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class RotatedRectangles
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new RotatedRectangles())
    End Sub

    Sub New()
        Text = "Rotated Rectangles"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim i As Integer
        Dim pn As New Pen(clr)

        grfx.PageUnit = GraphicsUnit.Pixel
        Dim aptf() As PointF = {grfx.VisibleClipBounds.Size.ToPointF()}
        grfx.PageUnit = GraphicsUnit.Inch
        grfx.PageScale = 0.01

        grfx.TransformPoints(CoordinateSpace.Page, _
                             CoordinateSpace.Device, aptf)

        grfx.TranslateTransform(aptf(0).X / 2, aptf(0).Y / 2)

        For i = 0 To 35
            grfx.DrawRectangle(pn, 0, 0, 100, 100)
            grfx.RotateTransform(10)
        Next i
    End Sub
End Class
