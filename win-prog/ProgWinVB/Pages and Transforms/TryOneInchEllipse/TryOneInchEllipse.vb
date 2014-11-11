'--------------------------------------------------
' TryOneInchEllipse.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TryOneInchEllipse
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TryOneInchEllipse())
    End Sub

    Sub New()
        Text = "Try One-Inch Ellipse"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawEllipse(New Pen(clr), 0, 0, grfx.DpiX, grfx.DpiY)
    End Sub
End Class
