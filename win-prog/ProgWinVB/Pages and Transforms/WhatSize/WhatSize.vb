'-----------------------------------------
' WhatSize.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class WhatSize
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new WhatSize())
    End Sub

    Sub New()
        Text = "What Size?"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim y As Integer = 0
        DoIt(grfx, br, y, GraphicsUnit.Pixel)
        DoIt(grfx, br, y, GraphicsUnit.Display)
        DoIt(grfx, br, y, GraphicsUnit.Document)
        DoIt(grfx, br, y, GraphicsUnit.Inch)
        DoIt(grfx, br, y, GraphicsUnit.Millimeter)
        DoIt(grfx, br, y, GraphicsUnit.Point)
    End Sub

    Private Sub DoIt(ByVal grfx As Graphics, ByVal br As Brush, _
            ByRef y As Integer, ByVal gu As GraphicsUnit)
        Dim gs As GraphicsState = grfx.Save()

        grfx.PageUnit = gu
        grfx.PageScale = 1

        Dim szf As SizeF = grfx.VisibleClipBounds.Size

        grfx.Restore(gs)
        grfx.DrawString(gu.ToString() & ": " & szf.ToString(), _
                        Font, br, 0, y)
        y += CInt(Math.Ceiling(Font.GetHeight(grfx)))
    End Sub
End Class
