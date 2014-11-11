'------------------------------------------
' Thumbnail.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Thumbnail
    Inherits PrintableForm

    Const iSquare As Integer = 64
    Private imgThumbnail As Image

    Shared Shadows Sub Main()
        Application.Run(new Thumbnail())
    End Sub

    Sub New()
        Text = "Thumbnail"
        Dim img As Image = Image.FromFile("..\..\Apollo11FullColor.jpg")
        Dim cxThumbnail, cyThumbnail As Integer

        If img.Width > img.Height Then
            cxThumbnail = iSquare
            cyThumbnail = iSquare * img.Height \ img.Width
        Else
            cyThumbnail = iSquare
            cxThumbnail = iSquare * img.Width \ img.Height
        End If
        imgThumbnail = img.GetThumbnailImage(cxThumbnail, cyThumbnail, _
                                      Nothing, IntPtr.Zero)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim x, y As Integer

        For y = 0 To cy Step iSquare
            For x = 0 To cx Step iSquare
                grfx.DrawImage(imgThumbnail, _
                        x + (iSquare - imgThumbnail.Width) \ 2, _
                        y + (iSquare - imgThumbnail.Height) \ 2, _
                        imgThumbnail.Width, imgThumbnail.Height)
            Next x
        Next y
    End Sub
End Class
