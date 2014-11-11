'-------------------------------------------------
' HelloWorldBitmap.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HelloWorldBitmap
    Inherits PrintableForm

    Const fResolution As Single = 300
    Private bm As bitmap

    Shared Shadows Sub Main()
        Application.Run(new HelloWorldBitmap())
    End Sub

    Sub New()
        Text = "Hello, World!"

        bm = New Bitmap(1, 1)
        bm.SetResolution(fResolution, fResolution)

        Dim grfx As Graphics = Graphics.FromImage(bm)
        Dim fnt As New Font("Times New Roman", 72)
        Dim sz As Size = grfx.MeasureString(Text, fnt).ToSize()

        bm = New Bitmap(bm, sz)
        bm.SetResolution(fResolution, fResolution)

        grfx = Graphics.FromImage(bm)
        grfx.Clear(Color.White)
        grfx.DrawString(Text, fnt, Brushes.Black, 0, 0)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(bm, 0, 0)
    End Sub
End Class
