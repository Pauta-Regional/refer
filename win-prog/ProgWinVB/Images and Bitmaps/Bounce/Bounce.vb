'---------------------------------------
' Bounce.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Bounce
    Inherits Form

    Const iTimerInterval As Integer = 25    ' In milliseconds
    Const iBallSize As Integer = 16         ' As fraction of client area
    Const iMoveSize As Integer = 4          ' As fraction of ball size

    Private bm As Bitmap
    Private xCenter, yCenter As Integer
    Private cxRadius, cyRadius, cxMove, cyMove, cxTotal, cyTotal As Integer

    Shared Sub Main()
        Application.Run(New Bounce())
    End Sub

    Sub New()
        Text = "Bounce"
        ResizeRedraw = True
        BackColor = Color.White

        Dim tmr As New Timer()
        tmr.Interval = iTimerInterval
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Start()

        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        grfx.Clear(BackColor)

        Dim fRadius As Single = Math.Min(ClientSize.Width / grfx.DpiX, _
                                         ClientSize.Height / grfx.DpiY) _
                                                           / iBallSize
        cxRadius = CInt(fRadius * grfx.DpiX)
        cyRadius = CInt(fRadius * grfx.DpiY)
        grfx.Dispose()

        cxMove = Math.Max(1, cxRadius \ iMoveSize)
        cyMove = Math.Max(1, cyRadius \ iMoveSize)
        cxTotal = 2 * (cxRadius + cxMove)
        cyTotal = 2 * (cyRadius + cyMove)

        bm = New Bitmap(cxTotal, cyTotal)
        grfx = Graphics.FromImage(bm)
        grfx.Clear(BackColor)
        DrawBall(grfx, New Rectangle(cxMove, cyMove, _
                                     2 * cxRadius, 2 * cyRadius))
        grfx.Dispose()
        xCenter = ClientSize.Width \ 2
        yCenter = ClientSize.Height \ 2
    End Sub

    Protected Overridable Sub DrawBall(ByVal grfx As Graphics, _
            ByVal rect As Rectangle)
        grfx.FillEllipse(Brushes.Red, rect)
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        grfx.DrawImage(bm, xCenter - cxTotal \ 2, _
                           yCenter - cyTotal \ 2, cxTotal, cyTotal)
        grfx.Dispose()

        xCenter += cxMove
        yCenter += cyMove

        If (xCenter + cxRadius >= ClientSize.Width) OrElse _
           (xCenter - cxRadius <= 0) Then
            cxMove = -cxMove
        End If
        If (yCenter + cyRadius >= ClientSize.Height) OrElse _
           (yCenter - cyRadius <= 0) Then
            cyMove = -cyMove
        End If
    End Sub
End Class
