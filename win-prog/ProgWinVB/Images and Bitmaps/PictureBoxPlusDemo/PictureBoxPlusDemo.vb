'---------------------------------------------------
' PictureBoxPlusDemo.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PictureBoxPlusDemo
    Inherits Form

    Shared Sub Main()
        Application.Run(new PictureBoxPlusDemo())
    End Sub

    Sub New()
        Text = "PictureBoxPlus Demo"

        Dim picbox As New PictureBoxPlus()
        picbox.Parent = Me
        picbox.Dock = DockStyle.Fill
        picbox.Image = Image.FromFile("..\..\Apollo11FullColor.jpg")
        picbox.SizeMode = PictureBoxSizeMode.StretchImage
        picbox.NoDistort = True
    End Sub
End Class
