'------------------------------------------------
' ImageReflection.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageReflection
    Inherits PrintableForm

    Private img As Image

    Shared Shadows Sub Main()
        Application.Run(New ImageReflection())
    End Sub

    Sub New()
        Text = "Image Reflection"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, cx \ 2, cy \ 2, img.Width, img.Height)
        grfx.DrawImage(img, cx \ 2, cy \ 2, -img.Width, img.Height)
        grfx.DrawImage(img, cx \ 2, cy \ 2, img.Width, -img.Height)
        grfx.DrawImage(img, cx \ 2, cy \ 2, -img.Width, -img.Height)
    End Sub
End Class
