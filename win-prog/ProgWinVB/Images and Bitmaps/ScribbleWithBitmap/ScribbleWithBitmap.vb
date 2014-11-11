'---------------------------------------------------
' ScribbleWithBitmap.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ScribbleWithBitmap
    Inherits Form

    Private bTracking As Boolean
    Private ptLast As Point
    Private bm As bitmap
    Private grfxBm As Graphics

    Shared Sub Main()
        Application.Run(new ScribbleWithBitmap())
    End Sub

    Sub New()
        Text = "Scribble with Bitmap"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create bitmap
        Dim sz As Size = SystemInformation.PrimaryMonitorMaximizedWindowSize
        bm = New Bitmap(sz.Width, sz.Height)

        ' Create Graphics object from bitmap
        grfxBm = Graphics.FromImage(bm)
        grfxBm.Clear(BackColor)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button <> MouseButtons.Left Then Return

        ptLast = New Point(mea.X, mea.Y)
        bTracking = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If Not bTracking Then Return

        Dim ptNew As New Point(mea.X, mea.Y)
        Dim pn As New Pen(ForeColor)
        Dim grfx As Graphics = CreateGraphics()

        grfx.DrawLine(pn, ptLast, ptNew)
        grfx.Dispose()

        ' Draw on bitmap
        grfxBm.DrawLine(pn, ptLast, ptNew)
        ptLast = ptNew
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        bTracking = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics

        ' Display bitmap
        grfx.DrawImage(bm, 0, 0, bm.Width, bm.Height)
    End Sub
End Class
