'--------------------------------------------
' KeyholeClip.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class KeyholeClip
    Inherits PrintableForm

    Protected img As Image
    Protected path As GraphicsPath

    Shared Shadows Sub Main()
        Application.Run(New KeyholeClip())
    End Sub

    Sub New()
        Text = "Keyhole Clip"

        img = Image.FromFile( _
                    "..\..\..\Images and Bitmaps\Apollo11FullColor.jpg")

        path = New GraphicsPath()
        path.AddArc(80, 0, 80, 80, 45, -270)
        path.AddLine(70, 180, 170, 180)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.SetClip(path)
        grfx.DrawImage(img, 0, 0, img.Width, img.Height)
    End Sub
End Class
