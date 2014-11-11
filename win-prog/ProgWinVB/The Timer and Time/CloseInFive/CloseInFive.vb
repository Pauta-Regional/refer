'--------------------------------------------
' CloseInFive.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CloseInFive
    Inherits Form

    Shared Sub Main()
        Application.Run(New CloseInFive())
    End Sub

    Sub New()
        Text = "Closing in Five Minutes"

        Dim tmr As New Timer()
        tmr.Interval = 5 * 60 * 1000
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Enabled = True
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim tmr As Timer = DirectCast(obj, Timer)

        tmr.Stop()
        RemoveHandler tmr.Tick, AddressOf TimerOnTick

        Close()
    End Sub
End Class
