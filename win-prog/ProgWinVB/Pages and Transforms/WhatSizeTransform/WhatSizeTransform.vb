'--------------------------------------------------
' WhatSizeTransform.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class WhatSizeTransform
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new WhatSizeTransform())
    End Sub

    Sub New()
        Text = "What Size? With TransformPoints"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim y As Integer = 0
        Dim apt() As Point = {New Point(cx, cy)}

        grfx.TransformPoints(CoordinateSpace.Device, _
                         CoordinateSpace.Page, apt)

        DoIt(grfx, br, y, apt(0), GraphicsUnit.Pixel)
        DoIt(grfx, br, y, apt(0), GraphicsUnit.Display)
        DoIt(grfx, br, y, apt(0), GraphicsUnit.Document)
        DoIt(grfx, br, y, apt(0), GraphicsUnit.Inch)
        DoIt(grfx, br, y, apt(0), GraphicsUnit.Millimeter)
        DoIt(grfx, br, y, apt(0), GraphicsUnit.Point)
    End Sub

    Private Sub DoIt(ByVal grfx As Graphics, ByVal br As Brush, ByRef y As Integer, ByVal pt As Point, ByVal gu As GraphicsUnit)
        Dim gs As GraphicsState = grfx.Save()

        grfx.PageUnit  = gu
        grfx.PageScale = 1

        Dim aptf() As PointF = {Point.op_Implicit(pt)}
        grfx.TransformPoints(CoordinateSpace.Page, _
                         CoordinateSpace.Device, aptf)

        Dim szf As New SizeF(aptf(0))
        grfx.Restore(gs)
        grfx.DrawString(gu.ToString() & ": " & szf.ToString(), Font, br, 0, y)
        y += CInt(Math.Ceiling(Font.GetHeight(grfx)))
    End Sub
End Class
