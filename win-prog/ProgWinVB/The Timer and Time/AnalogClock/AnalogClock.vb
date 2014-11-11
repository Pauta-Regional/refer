'---------------------------------------------
' AnalogClock.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class AnalogClock
    Inherits Form

    Private clkctrl As ClockControl

    Shared Sub Main()
        Application.Run(new AnalogClock())
    End Sub

    Sub New()
        Text = "Analog Clock"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        clkctrl = New ClockControl()
        clkctrl.Parent = Me
        clkctrl.Time = DateTime.Now
        clkctrl.Dock = DockStyle.Fill
        clkctrl.BackColor = Color.Black
        clkctrl.ForeColor = Color.White

        Dim tmr As New Timer()
        tmr.Interval = 100
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Start()
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        clkctrl.Time = DateTime.Now
    End Sub
End Class
