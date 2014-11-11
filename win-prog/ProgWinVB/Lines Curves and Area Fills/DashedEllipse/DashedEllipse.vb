'----------------------------------------------
' DashedEllipse.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DashedEllipse
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New DashedEllipse())
    End Sub

    Sub New()
        Text = "Dashed Ellipse Using DrawArc"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr)
        Dim rect As New Rectangle(0, 0, cx - 1, cy - 1)
        Dim iAngle As Integer

        For iAngle = 0 To 345 Step 15
            grfx.DrawArc(pn, rect, iAngle, 10)
        Next iAngle
    End Sub
End Class
