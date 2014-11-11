'----------------------------------------------
' ImageFromFile.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageFromFile
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new ImageFromFile())
    End Sub

    Sub New()
        Text = "Image From File"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim img As Image = Image.FromFile("..\..\Apollo11FullColor.jpg")
        grfx.DrawImage(img, 0, 0)
    End Sub
End Class
