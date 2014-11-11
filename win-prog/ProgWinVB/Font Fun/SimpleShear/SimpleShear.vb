'--------------------------------------------
' SimpleShear.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class SimpleShear
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New SimpleShear())
    End Sub

    Sub New()
        Text = "Simple Shear"
        strText = "Shear"
        fnt = New Font("Times New Roman", 72)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim matx As New Matrix()

        matx.Shear(0.5, 0)
        grfx.Transform = matx
        grfx.DrawString(strText, fnt, br, 0, 0)
    End Sub
End Class
