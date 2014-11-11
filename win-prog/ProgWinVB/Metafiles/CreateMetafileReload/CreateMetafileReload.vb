'-----------------------------------------------------
' CreateMetafileReload.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Class CreateMetafileReload
    Inherits PrintableForm

    Const strMetafile As String = "CreateMetafileReload.emf"

    Shared Shadows Sub Main()
        Application.Run(New CreateMetafileReload())
    End Sub

    Sub New()
        Text = "Create Metafile (Reload)"
        If Not File.Exists(strMetafile) Then

            ' Create the metafile.
            Dim grfx As Graphics = CreateGraphics()
            Dim ipHdc As IntPtr = grfx.GetHdc()
            Dim mf As New Metafile(strMetafile, ipHdc)
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
        End If
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim mf As New Metafile(strMetafile)
        Dim x, y As Integer

        For y = 0 To cy Step mf.Height
            For x = 0 To cx - 1 Step mf.Width
                grfx.DrawImage(mf, x, y, mf.Width, mf.Height)
            Next x
        Next y
    End Sub
End Class
