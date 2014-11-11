'------------------------------------------------------
' ImageScaleToRectangle.vb (c) 2002 by Charles Petzold
'-------------------- ----------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageScaleToRectangle
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new ImageScaleToRectangle())
    End Sub

    Sub New()
        Text = "Image Scale To Rectangle"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, 0, 0, cx, cy)
    End Sub
End Class
