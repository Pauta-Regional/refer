'-----------------------------------------------------
' DrawOnPixelSizeImage.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DrawOnPixelSizeImage
    Inherits PrintableForm

    Private img As Image
    Private str As String = "Apollo11"

    Shared Shadows Sub Main()
        Application.Run(New DrawOnPixelSizeImage())
    End Sub

    Sub New()
        Text = "Draw on Pixel-Size Image"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")

        Dim grfxImage As Graphics = Graphics.FromImage(img)
        Dim grfxScreen As Graphics = CreateGraphics()
        Dim fnt As New Font(Font.FontFamily, _
                            grfxScreen.DpiY / grfxImage.DpiY * Font.SizeInPoints)
        Dim szf As SizeF = grfxImage.MeasureString(str, fnt)

        grfxImage.DrawString(str, fnt, Brushes.White, _
                             img.Width - szf.Width, 0)
        grfxImage.Dispose()
        grfxScreen.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, 0, 0, img.Width, img.Height)
        grfx.DrawString(str, Font, New SolidBrush(clr), img.Width, 0)
    End Sub
End Class
