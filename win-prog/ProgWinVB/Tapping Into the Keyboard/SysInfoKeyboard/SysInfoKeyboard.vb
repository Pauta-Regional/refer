'------------------------------------------------
' SysInfoKeyboard.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoKeyboard
    Inherits SysInfoReflection

    Shared Shadows Sub Main()
        Application.Run(new SysInfoKeyboard())
    End Sub

    Sub New()
        Text = "System Information: Keyboard"
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        Dim pt As Point = AutoScrollPosition
        pt.X = -pt.X
        pt.Y = -pt.Y

        Select kea.KeyCode
            Case Keys.Right
                If (kea.Modifiers And Keys.Control) = Keys.Control Then
                    pt.X += ClientSize.Width
                Else
                    pt.X += Font.Height
                End If

            Case Keys.Left
                If (kea.Modifiers And Keys.Control) = Keys.Control Then
                    pt.X -= ClientSize.Width
                Else
                    pt.X -= Font.Height
                End If

            Case Keys.Down
                pt.Y += Font.Height

            Case Keys.Up
                pt.Y -= Font.Height

            Case Keys.PageDown
                pt.Y += Font.Height * (ClientSize.Height \ Font.Height)

            Case Keys.PageUp
                pt.Y -= Font.Height * (ClientSize.Height \ Font.Height)

            Case Keys.Home
                pt = Point.Empty

            Case Keys.End
                pt.Y = 1000000
        End Select

        AutoScrollPosition = pt
    End Sub
End Class
