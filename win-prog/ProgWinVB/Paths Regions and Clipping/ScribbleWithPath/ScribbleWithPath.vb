'-------------------------------------------------
' ScribbleWithPath.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class ScribbleWithPath
    Inherits Form

    Private path As GraphicsPath
    Private bTracking As Boolean
    Private ptLast As Point

    Shared Sub Main()
        Application.Run(New ScribbleWithPath())
    End Sub

    Sub New()
        Text = "Scribble with Path"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create the path.
        path = New GraphicsPath()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button <> MouseButtons.Left Then Return

        ptLast = New Point(mea.X, mea.Y)
        bTracking = True

        ' Start a figure.
        path.StartFigure()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If Not bTracking Then Return

        Dim ptNew As New Point(mea.X, mea.Y)
        Dim grfx As Graphics = CreateGraphics()
        grfx.DrawLine(New Pen(ForeColor), ptLast, ptNew)
        grfx.Dispose()

        ' Add a line.
        path.AddLine(ptLast, ptNew)
        ptLast = ptNew
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        bTracking = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        ' Draw the path.
        pea.Graphics.DrawPath(New Pen(ForeColor), path)
    End Sub
End Class
