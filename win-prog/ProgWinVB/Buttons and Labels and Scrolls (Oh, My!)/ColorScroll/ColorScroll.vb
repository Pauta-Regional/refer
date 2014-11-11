'--------------------------------------------
' ColorScroll.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ColorScroll
    Inherits Form

    Private pnl As Panel
    Private alblName(2) As Label
    Private alblValue(2) As Label
    Private ascrbar(2) As VScrollBar

    Shared Sub Main()
        Application.Run(New ColorScroll())
    End Sub

    Sub New()
        Text = "Color Scroll"
        Dim aclr() As Color = {Color.Red, Color.Green, Color.Blue}

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

            ascrbar(i) = New VScrollBar()
            ascrbar(i).Parent = pnl
            ascrbar(i).SmallChange = 1
            ascrbar(i).LargeChange = 16
            ascrbar(i).Minimum = 0
            ascrbar(i).Maximum = 255 + ascrbar(i).LargeChange - 1
            AddHandler ascrbar(i).ValueChanged, _
                                            AddressOf ScrollOnValueChanged
            ascrbar(i).TabStop = True

            alblValue(i) = New Label()
            alblValue(i).Parent = pnl
            alblValue(i).TextAlign = ContentAlignment.MiddleCenter
        Next i

        Dim clr As Color = BackColor
        ascrbar(0).Value = clr.R          ' Generates ValueChanged event.
        ascrbar(1).Value = clr.G
        ascrbar(2).Value = clr.B

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
            ascrbar(i).Location = New Point((4 * i + 1) * cx \ 24, _
                                            2 * cyFont)
            ascrbar(i).Size = New Size(cx \ 12, cy - 4 * cyFont)
            alblValue(i).Location = New Point(i * cx \ 6, _
                                              cy - 3 * cyFont \ 2)
            alblValue(i).Size = New Size(cx \ 6, cyFont)
        Next i
    End Sub

    Private Sub ScrollOnValueChanged(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer

        For i = 0 To 2
            If obj Is ascrbar(i) Then
                alblValue(i).Text = ascrbar(i).Value.ToString()
            End If
        Next i
        BackColor = Color.FromArgb(ascrbar(0).Value, _
                                  ascrbar(1).Value, _
                                  ascrbar(2).Value)
    End Sub
End Class
