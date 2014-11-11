'---------------------------------------------------
' PartialImageRotate.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PartialImageRotate
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new PartialImageRotate())
    End Sub

    Sub New()
        Text = "Partial Image Rotate"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim aptDst() As Point = {New Point(0, cy \ 2), _
                                 New Point(cx \ 2, 0), _
                                 New Point(cx \ 2, cy)}
        Dim rectSrc As New Rectangle(95, 5, 50, 55)

        grfx.DrawImage(img, aptDst, rectSrc, GraphicsUnit.Pixel)
    End Sub
End Class
