'---------------------------------------------
' PartialImage.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PartialImage
    Inherits PrintableForm

    Private img As Image

    Shared Shadows Sub Main()
        Application.Run(new PartialImage())
    End Sub

    Sub New()
        Text = "Partial Image"
        img = Image.FromFile("..\..\Apollo11FullColor.jpg")
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim rect As New Rectangle(95, 5, 50, 55)
        grfx.DrawImage(img, 0, 0, rect, GraphicsUnit.Pixel)
    End Sub
End Class
