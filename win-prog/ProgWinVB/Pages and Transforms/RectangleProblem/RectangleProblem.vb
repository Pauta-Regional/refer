'-------------------------------------------------
' RectangleProblem.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RectangleProblem
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new RectangleProblem())
    End Sub

    Sub New()
        Text = "Rectangle Problem"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr, 0.1f)
        grfx.PageUnit  = GraphicsUnit.Inch
        grfx.PageScale = 0.1f
        grfx.DrawRectangle(pn,  0,  0, 10, 10)
        grfx.DrawRectangle(pn, 10, 10, 10, 10)
    End Sub
End Class
