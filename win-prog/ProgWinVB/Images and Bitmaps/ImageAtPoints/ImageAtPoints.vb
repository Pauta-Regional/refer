'----------------------------------------------
' ImageAtPoints.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageAtPoints
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new ImageAtPoints())
    End Sub

    Sub New()
        Text = "Image At Points"
        img = image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, New Point() {New Point(cx \ 2, 0), _
                                         New Point(cx, cy \ 2), _
                                         New Point(0, cy \ 2)})
    End Sub
End Class
