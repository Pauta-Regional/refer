'--------------------------------------------------
' MetafilePageUnits.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing    ' Not used for anything yet!
Imports System.Windows.Forms

Class MetafilePageUnits
    Inherits PrintableForm

    Private mf As Metafile

    Shared Shadows Sub Main()
        Application.Run(New MetafilePageUnits())
    End Sub

    Sub New()
        Text = "Metafile Page Units"

        ' Create metafile.
        Dim grfx As Graphics = CreateGraphics()
        Dim ipHdc As IntPtr = grfx.GetHdc()
        mf = New Metafile("MetafilePageUnits.emf", ipHdc)
        grfx.ReleaseHdc(ipHdc)
        grfx.Dispose()

        ' Get Graphics Object for drawing on metafile.
        grfx = Graphics.FromImage(mf)
        grfx.Clear(Color.White)

        ' Draw in units of pixels (1-point pen width).
        grfx.PageUnit = GraphicsUnit.Pixel
        Dim pn As New Pen(Color.Black, grfx.DpiX / 72)
        grfx.DrawRectangle(pn, 0, 0, grfx.DpiX, grfx.DpiY)

        ' Draw in units of 1/100 inch (1-point pen width).
        grfx.PageUnit = GraphicsUnit.Inch
        grfx.PageScale = 0.01F
        pn = New Pen(Color.Black, 100.0F / 72)
        grfx.DrawRectangle(pn, 25, 25, 100, 100)

        ' Draw in units of millimeters (1-point pen width).
        grfx.PageUnit = GraphicsUnit.Millimeter
        grfx.PageScale = 1
        pn = New Pen(Color.Black, 25.4F / 72)
        grfx.DrawRectangle(pn, 12.7F, 12.7F, 25.4F, 25.4F)

        ' Draw in units of pts (1-point pen width).
        grfx.PageUnit = GraphicsUnit.Point
        pn = New Pen(Color.Black, 1)
        grfx.DrawRectangle(pn, 54, 54, 72, 72)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(mf, 0, 0)
    End Sub
End Class
