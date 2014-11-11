'----------------------------------------------------
' KeyholeClipCentered.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class KeyholeClipCentered
    Inherits KeyholeClip

    Shared Shadows Sub Main()
        Application.Run(new KeyholeClipCentered())
    End Sub

    Sub New()
        Text &= " Centered"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.SetClip(path)

        Dim rectf As RectangleF = path.GetBounds()
        Dim xOffset As Integer = CInt((cx - rectf.Width) / 2 - rectf.X)
        Dim yOffset As Integer = CInt((cy - rectf.Height) / 2 - rectf.Y)

        grfx.TranslateClip(xOffset, yOffset)
        grfx.DrawImage(img, xOffset, yOffset, img.Width, img.Height)
    End Sub
End Class
