'--------------------------------------------
' DotsPerInch.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DotsPerInch
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new DotsPerInch())
    End Sub

    Sub New()
        Text = "Dots Per Inch"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawString(String.Format("DpiX = {0}" & vbLf & "DpiY = {1}", _
                                      grfx.DpiX, grfx.DpiY), _
                        Font, New SolidBrush(clr), 0, 0)
    End Sub
End Class
