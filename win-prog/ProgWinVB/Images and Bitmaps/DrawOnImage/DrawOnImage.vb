'--------------------------------------------
' DrawOnImage.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DrawOnImage
    Inherits PrintableForm

    Private img As image
    Private str As String = "Apollo11"

    Shared Shadows Sub Main()
        Application.Run(new DrawOnImage())
    End Sub

    Sub New()
        Text  = "Draw on Image"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")

        Dim grfxImage As Graphics = Graphics.FromImage(img)
        grfxImage.PageUnit = GraphicsUnit.Inch
        grfxImage.PageScale = 1

        Dim szf As SizeF = grfxImage.MeasureString(str, Font)
        grfxImage.DrawString(str, Font, Brushes.White,  _
                    grfxImage.VisibleClipBounds.Width - szf.Width, 0)
        grfxImage.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.PageUnit = GraphicsUnit.Pixel
        grfx.DrawImage(img, 0, 0)
        grfx.DrawString(str, Font, New SolidBrush(clr), _
                        grfx.DpiX * img.Width / img.HorizontalResolution, 0)
    End Sub
End Class
