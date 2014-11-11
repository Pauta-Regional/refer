'----------------------------------------------------
' HundredPixelsSquare.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HundredPixelsSquare
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new HundredPixelsSquare())
    End Sub

    Sub New()
        Text = "Hundred Pixels Square"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.FillRectangle(New SolidBrush(clr), 100, 100, 100, 100)
    End Sub
End Class
