'-----------------------------------------------
' CreateMetafile.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO                  ' Not used for anything yet!
Imports System.Windows.Forms

Class CreateMetafile
    Inherits PrintableForm

    Private mf As Metafile

    Shared Shadows Sub Main()
        Application.Run(New CreateMetafile())
    End Sub

    Sub New()
        Text = "Create Metafile"

        ' Create the metafile.
        Dim grfx As Graphics = CreateGraphics()
        Dim ipHdc As IntPtr = grfx.GetHdc()
        mf = New Metafile("CreateMetafile.emf", ipHdc)
        grfx.ReleaseHdc(ipHdc)
        grfx.Dispose()

        ' Draw on the metafile.
        grfx = Graphics.FromImage(mf)

        grfx.FillEllipse(Brushes.Gray, 0, 0, 100, 100)
        grfx.DrawEllipse(Pens.Black, 0, 0, 100, 100)
        grfx.FillEllipse(Brushes.Blue, 20, 20, 20, 20)
        grfx.FillEllipse(Brushes.Blue, 60, 20, 20, 20)
        grfx.DrawArc(New Pen(Color.Red, 10), 20, 20, 60, 60, 30, 120)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim x, y As Integer

        For y = 0 To cy Step mf.Height
            For x = 0 To cx Step mf.Width
                grfx.DrawImage(mf, x, y, mf.Width, mf.Height)
            Next x
        Next y
    End Sub
End Class
