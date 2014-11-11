'-----------------------------------------
' Scribble.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Scribble
    Inherits Form

    Private bTracking As Boolean
    Private ptLast As Point

    Shared Sub Main()
        Application.Run(new Scribble())
    End Sub

    Sub New()
        Text = "Scribble"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button <> MouseButtons.Left Then Return

        ptLast = New Point(mea.X, mea.Y)
        bTracking = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If Not bTracking Then Return

        Dim ptNew As New Point(mea.X, mea.Y)
        Dim grfx As Graphics = CreateGraphics()
        grfx.DrawLine(New Pen(ForeColor), ptLast, ptNew)
        grfx.Dispose()
        ptLast = ptNew
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        bTracking = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        ' What do I do here?
    End Sub
End Class
