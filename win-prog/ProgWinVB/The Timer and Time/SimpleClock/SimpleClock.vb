'--------------------------------------------
' SimpleClock.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleClock
    Inherits Form

    Shared Sub Main()
        Application.Run(new SimpleClock())
    End Sub

    Sub New()
        Text = "Simple Clock"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        Dim tmr As New Timer()
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Interval = 1000
        tmr.Start()
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center
        pea.Graphics.DrawString(DateTime.Now.ToString("F"), _
                            Font, New SolidBrush(ForeColor), _
                            RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
