'---------------------------------------------
' LineArcCombo.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class LineArcCombo
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New LineArcCombo())
    End Sub

    Sub New()
        Text = "Line & Arc Combo"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr, 25)
        grfx.DrawLine(pn, 25, 100, 125, 100)
        grfx.DrawArc(pn, 125, 50, 100, 100, -180, 180)
        grfx.DrawLine(pn, 225, 100, 325, 100)
    End Sub
End Class
