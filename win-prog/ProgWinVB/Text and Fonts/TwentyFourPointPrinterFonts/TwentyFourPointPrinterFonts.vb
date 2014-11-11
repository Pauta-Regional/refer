'------------------------------------------------------------
' TwentyFourPointPrinterFonts.vb (c) 2002 by Charles Petzold
'------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwentyFourPointPrinterFonts
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TwentyFourPointPrinterFonts())
    End Sub

    Sub New()
        Text = "Twenty-Four Point Printer Fonts"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim cyFont As Single
        Dim y As Single = 0
        Dim fnt As Font
        Dim strFamily As String = "Times New Roman"

        fnt = New Font(strFamily, 24)
        grfx.DrawString("No GraphicsUnit, 24 points", fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        fnt = New Font(strFamily, 24, GraphicsUnit.Point)
        grfx.DrawString("GraphicsUnit.Point, 24 units", fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        cyFont = 1 / 3
        fnt = New Font(strFamily, cyFont, GraphicsUnit.Inch)
        grfx.DrawString("GraphicsUnit.Inch, " & cyFont & " units", _
                        fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        cyFont = 25.4 / 3
        fnt = New Font(strFamily, cyFont, GraphicsUnit.Millimeter)
        grfx.DrawString("GraphicsUnit.Millimeter, " & cyFont & " units", _
                        fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        fnt = New Font(strFamily, 100, GraphicsUnit.Document)
        grfx.DrawString("GraphicsUnit.Document, 100 units", _
                        fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        cyFont = 100 / 3
        fnt = New Font(strFamily, cyFont, GraphicsUnit.Pixel)
        grfx.DrawString("GraphicsUnit.Pixel, " & cyFont & " units", _
                        fnt, br, 0, y)
        y += fnt.GetHeight(grfx)

        fnt = New Font(strFamily, cyFont, GraphicsUnit.World)
        grfx.DrawString("GraphicsUnit.World, " & cyFont & " units", _
                        fnt, br, 0, y)
    End Sub
End Class
