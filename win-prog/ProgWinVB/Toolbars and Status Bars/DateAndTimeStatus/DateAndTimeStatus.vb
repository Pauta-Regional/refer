'--------------------------------------------------
' DateAndTimeStatus.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DateAndTimeStatus
    Inherits Form

    Private sbpMenu, sbpDate, sbpTime As StatusBarPanel

    Shared Sub Main()
        Application.Run(New DateAndTimeStatus())
    End Sub

    Sub New()
        Text = "Date and Time Status"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create status bar.
        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.ShowPanels = True

        ' Create status bar panels.
        sbpMenu = New StatusBarPanel()
        sbpMenu.Text = "Reserved for menu help"
        sbpMenu.BorderStyle = StatusBarPanelBorderStyle.None
        sbpMenu.AutoSize = StatusBarPanelAutoSize.Spring

        sbpDate = New StatusBarPanel()
        sbpDate.AutoSize = StatusBarPanelAutoSize.Contents
        sbpDate.ToolTipText = "The current date"

        sbpTime = New StatusBarPanel()
        sbpTime.AutoSize = StatusBarPanelAutoSize.Contents
        sbpTime.ToolTipText = "The current time"

        ' Attach status bar panels to status bar.
        sbar.Panels.AddRange(New StatusBarPanel() _
                                    {sbpMenu, sbpDate, sbpTime})

        ' Set the timer for 1 second.
        Dim tmr As New Timer()
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Interval = 1000
        tmr.Start()
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dt As DateTime = DateTime.Now

        sbpDate.Text = dt.ToShortDateString()
        sbpTime.Text = dt.ToShortTimeString()
    End Sub
End Class
