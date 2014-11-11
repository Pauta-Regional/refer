'----------------------------------------------
' KeyholeBitmap.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Class KeyholeBitmap
    Inherits PrintableForm

    Private bm As Bitmap

    Shared Shadows Sub Main()
        Application.Run(New KeyholeBitmap())
    End Sub

    Sub New()
        Text = "Keyhole Bitmap"

        ' Load image.
        Dim img As Image = Image.FromFile( _
                    "..\..\..\Images and Bitmaps\Apollo11FullColor.jpg")

        ' Create clipping path.
        Dim path As New GraphicsPath()
        path.AddArc(80, 0, 80, 80, 45, -270)
        path.AddLine(70, 180, 170, 180)

        ' Get size of clipping path.
        Dim rectf As RectangleF = path.GetBounds()

        ' Create new bitmap initialized to transparent.
        bm = New Bitmap(CInt(rectf.Width), CInt(rectf.Height), _
                        PixelFormat.Format32bppArgb)

        ' Create Graphics Object based on new bitmap.
        Dim grfx As Graphics = Graphics.FromImage(bm)

        ' Draw original image on bitmap with clipping.
        grfx.SetClip(path)
        grfx.TranslateClip(-rectf.X, -rectf.Y)
        grfx.DrawImage(img, CInt(-rectf.X), CInt(-rectf.Y), _
                           img.Width, img.Height)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(bm, (cx - bm.Width) \ 2, _
                           (cy - bm.Height) \ 2, _
                           bm.Width, bm.Height)
    End Sub
End Class
