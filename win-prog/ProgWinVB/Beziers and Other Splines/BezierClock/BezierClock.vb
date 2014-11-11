'--------------------------------------------
' BezierClock.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BezierClock
    Inherits Form

    Private clkctrl As BezierClockControl

    Shared Sub Main()
        Application.Run(New BezierClock())
    End Sub

    Sub New()
        Text = "Bezier Clock"

        clkctrl = New BezierClockControl()
        clkctrl.Parent = Me
        clkctrl.Time = DateTime.Now
        clkctrl.Dock = DockStyle.Fill
        clkctrl.BackColor = Color.Black
        clkctrl.ForeColor = Color.White

        Dim tmr As New Timer()
        tmr.Interval = 100
        AddHandler tmr.Tick, AddressOf OnTimerTick
        tmr.Start()
    End Sub

    Private Sub OnTimerTick(ByVal obj As Object, ByVal ea As EventArgs)
        clkctrl.Time = DateTime.Now
    End Sub
End Class
