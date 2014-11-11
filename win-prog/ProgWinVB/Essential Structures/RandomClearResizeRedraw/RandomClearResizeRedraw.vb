'--------------------------------------------------------
' RandomClearResizeRedraw.vb (c) 2002 by Charles Petzold
'--------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RandomClearResizeRedraw
    Inherits Form

    Shared Sub Main()
        Application.Run(New RandomClearResizeRedraw())
    End Sub

    Sub New()
        Text = "Random Clear with Resize Redraw"
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim rand As New Random()
        grfx.Clear(Color.FromArgb(rand.Next(256), _
                                  rand.Next(256), _
                                  rand.Next(256)))
    End Sub
End Class
