'------------------------------------------------
' OutlineClientRectangle.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class OutlineClientRectangle
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New OutlineClientRectangle())
    End Sub

    Sub New()
        Text = "Outline Client Rectangle"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawRectangle(Pens.Red, 0, 0, cx - 1, cy - 1)
    End Sub
End Class
