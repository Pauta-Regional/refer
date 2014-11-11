' --------------------------------------------------
' ArbitraryCoordinates.vb (c) 2002 by Charles Petzold
' --------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ArbitraryCoordinates
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New ArbitraryCoordinates())
    End Sub

    Sub New()
        Text = "Arbitrary Coordinates"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.PageUnit = GraphicsUnit.Pixel
        Dim szf As SizeF = grfx.VisibleClipBounds.Size

        grfx.PageUnit = GraphicsUnit.Inch
        grfx.PageScale = Math.Min(szf.Width / grfx.DpiX / 1000, _
                                  szf.Height / grfx.DpiY / 1000)

        grfx.DrawEllipse(New Pen(clr), 0, 0, 990, 990)
    End Sub
End Class
