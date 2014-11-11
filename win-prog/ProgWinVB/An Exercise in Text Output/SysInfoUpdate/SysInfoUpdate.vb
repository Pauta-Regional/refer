'----------------------------------------------
' SysInfoUpdate.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoUpdate
    Inherits Form

    Protected iCount, cySpace As Integer
    Protected astrLabels(), astrValues() As String
    Protected cxCol As Single

    Shared Sub Main()
        Application.Run(New SysInfoUpdate())
    End Sub

    Sub New()
        Text = "System Information: Update"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        AutoScroll = True

        AddHandler SystemEvents.UserPreferenceChanged, _
                                    AddressOf UserPreferenceChanged
        AddHandler SystemEvents.DisplaySettingsChanged, _
                                    AddressOf DisplaySettingsChanged
        UpdateAllInfo()
    End Sub

    Private Sub UserPreferenceChanged(ByVal obj As Object, _
            ByVal ea As UserPreferenceChangedEventArgs)
        UpdateAllInfo()
        Invalidate()
    End Sub

    Private Sub DisplaySettingsChanged(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        UpdateAllInfo()
        Invalidate()
    End Sub

    Private Sub UpdateAllInfo()
        iCount = SysInfoStrings.Count
        astrLabels = SysInfoStrings.Labels
        astrValues = SysInfoStrings.Values

        Dim grfx As Graphics = CreateGraphics()
        Dim szf As SizeF = grfx.MeasureString(" ", Font)
        cxCol = szf.Width + SysInfoStrings.MaxLabelWidth(grfx, Font)
        cySpace = Font.Height
        AutoScrollMinSize = New Size( _
            CInt(Math.Ceiling(cxCol + _
                              SysInfoStrings.MaxValueWidth(grfx, Font))), _
            CInt(Math.Ceiling(cySpace * iCount)))
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim pt As Point = AutoScrollPosition
        Dim i As Integer

        For i = 0 To iCount - 1
            grfx.DrawString(astrLabels(i), Font, br, _
                            pt.X, pt.Y + i * cySpace)
            grfx.DrawString(astrValues(i), Font, br, _
                            pt.X + cxCol, pt.Y + i * cySpace)
        Next i
    End Sub
End Class
