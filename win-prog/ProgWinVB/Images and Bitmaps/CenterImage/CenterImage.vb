'--------------------------------------------
' CenterImage.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CenterImage
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new CenterImage())
    End Sub

    Sub New()
        Text = "Center Image"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.PageUnit = GraphicsUnit.Pixel
        grfx.PageScale = 1
        Dim rectf As RectangleF = grfx.VisibleClipBounds
        Dim cxImage As Single = grfx.DpiX * img.Width / _
                                            img.HorizontalResolution
        Dim cyImage As Single = grfx.DpiY * img.Height / _
                                            img.VerticalResolution
        grfx.DrawImage(img, (rectf.Width - cxImage) / 2, _
                            (rectf.Height - cyImage) / 2)
    End Sub
End Class
