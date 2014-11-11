'----------------------------------------------
' ColorTrackBar.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ColorTrackBar
    Inherits Form

    Private pnl As Panel
    Private alblName(2) As Label
    Private alblValue(2) As Label
    Private atrkbar(2) As TrackBar

    Shared Sub Main()
        Application.Run(New ColorTrackBar())
    End Sub

    Sub New()
        Text = "Color Track Bar"
        Dim aclr As Color() = {Color.Red, Color.Green, Color.Blue}

        ' Create the panel.
        pnl = New Panel()
        pnl.Parent = Me
        pnl.Location = New Point(0, 0)
        pnl.BackColor = Color.White

        ' Loop through the three colors.
        Dim i As Integer
        For i = 0 To 2
            alblName(i) = New Label()
            alblName(i).Parent = pnl
            alblName(i).ForeColor = aclr(i)
            alblName(i).Text = "&" & aclr(i).ToKnownColor().ToString()
            alblName(i).TextAlign = ContentAlignment.MiddleCenter

            atrkbar(i) = New TrackBar()
            atrkbar(i).Parent = pnl
            atrkbar(i).Orientation = Orientation.Vertical
            atrkbar(i).BackColor = aclr(i)
            atrkbar(i).SmallChange = 1
            atrkbar(i).LargeChange = 16
            atrkbar(i).Minimum = 0
            atrkbar(i).Maximum = 255
            atrkbar(i).TickFrequency = 16
            AddHandler atrkbar(i).ValueChanged, _
                                        AddressOf TrackBarOnValueChanged

            alblValue(i) = New Label()
            alblValue(i).Parent = pnl
            alblValue(i).TextAlign = ContentAlignment.MiddleCenter
        Next i

        Dim clr As Color = BackColor
        atrkbar(0).Value = clr.R           ' Generates ValueChanged event.
        atrkbar(1).Value = clr.G
        atrkbar(2).Value = clr.B

        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)

        Dim cx As Integer = ClientSize.Width
        Dim cy As Integer = ClientSize.Height
        Dim cyFont As Integer = Font.Height
        Dim i As Integer

        pnl.Size = New Size(cx \ 2, cy)

        For i = 0 To 2
            alblName(i).Location = New Point(i * cx \ 6, cyFont \ 2)
            alblName(i).Size = New Size(cx \ 6, cyFont)
            atrkbar(i).Height = cy - 4 * cyFont
            atrkbar(i).Location = _
                New Point((1 + 2 * i) * cx \ 12 - atrkbar(i).Width \ 2, _
                          2 * cyFont)
            alblValue(i).Location = New Point(i * cx \ 6, _
                                              cy - 3 * cyFont \ 2)
            alblValue(i).Size = New Size(cx \ 6, cyFont)
        Next i
    End Sub

    Private Sub TrackBarOnValueChanged(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer
        For i = 0 To 2
            If obj Is atrkbar(i) Then
                alblValue(i).Text = atrkbar(i).Value.ToString()
            End If
        Next i
        BackColor = Color.FromArgb(atrkbar(0).Value, _
                                   atrkbar(1).Value, _
                                   atrkbar(2).Value)
    End Sub
End Class
