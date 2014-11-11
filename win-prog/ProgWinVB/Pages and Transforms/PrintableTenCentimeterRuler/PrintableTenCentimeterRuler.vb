'------------------------------------------------------------
' PrintableTenCentimeterRuler.vb (c) 2002 by Charles Petzold
'------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PrintableTenCentimeterRuler
    Inherits TenCentimeterRuler

    Shared Shadows Sub Main()
        Application.Run(new PrintableTenCentimeterRuler())
    End Sub

    Sub New()
        Text = "Printable " & Text
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.PageUnit = GraphicsUnit.Pixel
        MyBase.DoPage(grfx, clr, cx, cy)
    End Sub
End Class
