'--------------------------------------------------
' SevenSegmentClock.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Class SevenSegmentClock
    Inherits Form

    Private dt As DateTime

    Shared Sub Main()
        Application.Run(new SevenSegmentClock())
    End Sub

    Sub New()
        Text = "Seven-Segment Clock"
        BackColor = Color.White
        ResizeRedraw = True
        MinimumSize = Size.op_Addition(SystemInformation.MinimumWindowSize, _
                                       New Size(0, 1))
        Dim tmr As New Timer()
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Interval = 100
        tmr.Enabled = True
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dtNow As DateTime = DateTime.Now
        dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, _
                             dtNow.Hour, dtNow.Minute, dtNow.Second)
        If dtNow <> dt Then
            dt = dtNow
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim ssd As New SevenSegmentDisplay(pea.Graphics)
        Dim strTime As String = _
                        dt.ToString("T", DateTimeFormatInfo.InvariantInfo)
        Dim szf As SizeF = ssd.MeasureString(strTime, Font)
        Dim fScale As Single = Math.Min(ClientSize.Width / szf.Width, _
                                        ClientSize.Height / szf.Height)

        Dim fnt As New Font(Font.FontFamily, fScale * Font.SizeInPoints)
        szf = ssd.MeasureString(strTime, fnt)
        ssd.DrawString(strTime, fnt, Brushes.Red, _
                       (ClientSize.Width - szf.Width) / 2, _
                       (ClientSize.Height - szf.Height) / 2)
    End Sub
End Class
