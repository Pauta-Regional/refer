'------------------------------------------------
' RandomRectangle.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RandomRectangle
    Inherits Form

    Shared Sub Main()
        Application.Run(New RandomRectangle())
    End Sub

    Sub New()
        Text = "Random Rectangle"

        Dim tmr As New Timer()
        tmr.Interval = 1
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Start()
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim rand As New Random()
        Dim x1 As Integer = rand.Next(ClientSize.Width)
        Dim x2 As Integer = rand.Next(ClientSize.Width)
        Dim y1 As Integer = rand.Next(ClientSize.Height)
        Dim y2 As Integer = rand.Next(ClientSize.Height)
        Dim clr As Color = Color.FromArgb(rand.Next(256), _
                                          rand.Next(256), _
                                          rand.Next(256))

        Dim grfx As Graphics = CreateGraphics()
        grfx.FillRectangle(New SolidBrush(clr), _
                           Math.Min(x1, x2), Math.Min(y1, y2), _
                           Math.Abs(x2 - x1), Math.Abs(y2 - y1))
        grfx.Dispose()
    End Sub
End Class
