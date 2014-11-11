'---------------------------------------------
' DigitalClock.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DigitalClock
    Inherits Form

    Shared Sub Main()
        Application.Run(new DigitalClock())
    End Sub

    Sub New()
        Text = "Digital Clock"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
        MinimumSize = Size.op_Addition(SystemInformation.MinimumWindowSize, _
                                       New Size(0, 1))
        Dim tmr As New Timer()
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Interval = 1000
        tmr.Start()
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim strTime As String = DateTime.Now.ToString("T")
        Dim szf As SizeF = grfx.MeasureString(strTime, Font)
        Dim fScale As Single = Math.Min(ClientSize.Width / szf.Width, _
                                        ClientSize.Height / szf.Height)
        Dim fnt As New Font(Font.FontFamily, fScale * Font.SizeInPoints)
        szf = grfx.MeasureString(strTime, fnt)
        grfx.DrawString(strTime, fnt, New SolidBrush(ForeColor), _
                        (ClientSize.Width - szf.Width) / 2, _
                        (ClientSize.Height - szf.Height) / 2)
    End Sub
End Class
