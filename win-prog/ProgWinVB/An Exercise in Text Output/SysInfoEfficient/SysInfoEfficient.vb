'-------------------------------------------------
' SysInfoEfficient.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoEfficient
    Inherits SysInfoUpdate

    Shared Shadows Sub Main()
        Application.Run(New SysInfoEfficient())
    End Sub

    Sub New()
        Text = "System Information: Efficient"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim pt As Point = AutoScrollPosition
        Dim i As Integer
        Dim iFirst As Integer = (pea.ClipRectangle.Top - pt.Y) \ cySpace
        Dim iLast As Integer = (pea.ClipRectangle.Bottom - pt.Y) \ cySpace

        iLast = Math.Min(iCount - 1, iLast)

        For i = iFirst To iLast
            grfx.DrawString(astrLabels(i), Font, br, _
                            pt.X, pt.Y + i * cySpace)
            grfx.DrawString(astrValues(i), Font, br, _
                            pt.X + cxCol, pt.Y + i * cySpace)
        Next i
    End Sub
End Class
