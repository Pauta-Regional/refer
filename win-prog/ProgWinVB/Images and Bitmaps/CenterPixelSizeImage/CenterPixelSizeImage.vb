'-----------------------------------------------------
' CenterPixelSizeImage.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CenterPixelSizeImage
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new CenterPixelSizeImage())
    End Sub

    Sub New()
        Text = "Center Pixel-Size Image"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, (cx - img.Width) \ 2, _
                            (cy - img.Height) \ 2, _
                            img.Width, img.Height)
    End Sub
End Class
