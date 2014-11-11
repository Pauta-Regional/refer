'----------------------------------------------------
' ImageScaleIsotropic.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageScaleIsotropic
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new ImageScaleIsotropic())
    End Sub

    Sub New()
        Text = "Image Scale Isotropic"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        ScaleImageIsotropically(grfx, img, New Rectangle(0, 0, cx, cy))
    End Sub

    Private Sub ScaleImageIsotropically(ByVal grfx As Graphics, _
            ByVal img As Image, ByVal rect As Rectangle)
        Dim szf As New SizeF(img.Width / img.HorizontalResolution, _
                             img.Height / img.VerticalResolution)
        Dim fScale As Single = Math.Min(rect.Width / szf.Width, _
                                        rect.Height / szf.Height)
        szf.Width *= fScale
        szf.Height *= fScale
        grfx.DrawImage(img, rect.X + (rect.Width - szf.Width) / 2, _
                            rect.Y + (rect.Height - szf.Height) / 2, _
                            szf.Width, szf.Height)
    End Sub
End Class
