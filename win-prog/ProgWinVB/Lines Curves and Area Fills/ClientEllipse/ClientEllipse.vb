'----------------------------------------------
' ClientEllipse.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ClientEllipse
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new ClientEllipse())
    End Sub

    Sub New()
        Text = "Client Ellipse"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawEllipse(New Pen(clr), 0, 0, cx - 1, cy - 1)
    End Sub
End Class
