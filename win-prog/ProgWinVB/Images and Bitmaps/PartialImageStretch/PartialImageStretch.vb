'----------------------------------------------------
' PartialImageStretch.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PartialImageStretch
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new PartialImageStretch())
    End Sub

    Sub New()
        Text = "Partial Image Stretch"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim rectSrc As New Rectangle(95, 5, 50, 55)
        Dim rectDst As New Rectangle(0, 0, cx, cy)

        grfx.DrawImage(img, rectDst, rectSrc, GraphicsUnit.Pixel)
    End Sub
End Class
