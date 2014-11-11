'----------------------------------------------------
' CheckerWithKeyboard.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckerWithKeyboard
    Inherits Checker

    Shared Shadows Sub Main()
        Application.Run(New CheckerWithKeyboard())
    End Sub

    Sub New()
        Text &= " with Keyboard Interface"
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal ea As EventArgs)
        Cursor.Show()
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal ea As EventArgs)
        Cursor.Hide()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        Dim ptCursor As Point = PointToClient(Cursor.Position)
        Dim x As Integer = Math.Max(0, _
                           Math.Min(xNum - 1, ptCursor.X \ cxBlock))
        Dim y As Integer = Math.Max(0, _
                           Math.Min(yNum - 1, ptCursor.Y \ cyBlock))
        Select Case kea.KeyCode
            Case Keys.Up
                y -= 1
            Case Keys.Down
                y += 1
            Case Keys.Left
                x -= 1
            Case Keys.Right
                x += 1
            Case Keys.Home
                x = 0
                y = 0
            Case Keys.End : x = xNum - 1
                y = yNum - 1
            Case Keys.Enter, Keys.Space
                abChecked(y, x) = Not abChecked(y, x)
                Invalidate(New Rectangle(x * cxBlock, y * cyBlock, _
                                         cxBlock, cyBlock))
                Return
            Case Else
                Return
        End Select

        x = (x + xNum) Mod xNum
        y = (y + yNum) Mod yNum

        Cursor.Position = PointToScreen( _
                            New Point(x * cxBlock + cxBlock \ 2, _
                                      y * cyBlock + cyBlock \ 2))
    End Sub
End Class
