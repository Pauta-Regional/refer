'--------------------------------------------
' SysInfoList.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoList
    Inherits Form

    ReadOnly cxCol As Single
    ReadOnly cySpace As Integer

    Shared Sub Main()
        Application.Run(New SysInfoList())
    End Sub

    Sub New()
        Text = "System Information: List"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        Dim grfx As Graphics = CreateGraphics()
        Dim szf As SizeF = grfx.MeasureString(" ", Font)
        cxCol = szf.Width + SysInfoStrings.MaxLabelWidth(grfx, Font)
        grfx.Dispose()

        cySpace = Font.Height
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim iCount As Integer = SysInfoStrings.Count
        Dim astrLabels() As String = SysInfoStrings.Labels
        Dim astrValues() As String = SysInfoStrings.Values
        Dim i As Integer

        For i = 0 To iCount - 1
            grfx.DrawString(astrLabels(i), Font, br, 0, i * cySpace)
            grfx.DrawString(astrValues(i), Font, br, cxCol, i * cySpace)
        Next i
    End Sub
End Class
