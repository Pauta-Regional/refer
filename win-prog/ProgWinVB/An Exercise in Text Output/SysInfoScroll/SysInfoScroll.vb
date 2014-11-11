'----------------------------------------------
' SysInfoScroll.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoScroll
    Inherits Form

    ReadOnly cxCol As Single
    ReadOnly cySpace As Integer

    Shared Sub Main()
        Application.Run(New SysInfoScroll())
    End Sub

    Sub New()
        Text = "System Information: Scroll"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        Dim grfx As Graphics = CreateGraphics()
        Dim szf As SizeF = grfx.MeasureString(" ", Font)
        cxCol = szf.Width + SysInfoStrings.MaxLabelWidth(grfx, Font)
        cySpace = Font.Height

        ' Set auto-scroll properties.
        AutoScroll = True
        AutoScrollMinSize = New Size( _
            CInt(Math.Ceiling(cxCol + _
                           SysInfoStrings.MaxValueWidth(grfx, Font))), _
            CInt(Math.Ceiling(cySpace * SysInfoStrings.Count)))
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim iCount As Integer = SysInfoStrings.Count
        Dim astrLabels() As String = SysInfoStrings.Labels
        Dim astrValues() As String = SysInfoStrings.Values
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
