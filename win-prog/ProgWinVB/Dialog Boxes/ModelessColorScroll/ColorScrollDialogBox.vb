'-----------------------------------------------------
' ColorScrollDialogBox.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ColorScrollDialogBox
    Inherits Form

    Private alblName(2) As Label
    Private alblValue(2) As Label
    Private ascrbar(2) As VScrollBar

    Event Changed As EventHandler

    Sub New()
        Text = "Color Scroll Dialog Box"

        ControlBox = False
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False

        Dim aclr() As Color = {Color.Red, Color.Green, Color.Blue}
        Dim i As Integer

        For i = 0 To 2
            alblName(i) = New Label()
            alblName(i).Parent = Me
            alblName(i).ForeColor = aclr(i)
            alblName(i).Text = "&" & aclr(i).ToKnownColor().ToString()
            alblName(i).TextAlign = ContentAlignment.MiddleCenter

            ascrbar(i) = New VScrollBar()
            ascrbar(i).Parent = Me
            ascrbar(i).SmallChange = 1
            ascrbar(i).LargeChange = 16
            ascrbar(i).Minimum = 0
            ascrbar(i).Maximum = 255 + ascrbar(i).LargeChange - 1
            AddHandler ascrbar(i).ValueChanged, _
                                        AddressOf ScrollOnValueChanged
            ascrbar(i).TabStop = True

            alblValue(i) = New Label()
            alblValue(i).Parent = Me
            alblValue(i).TextAlign = ContentAlignment.MiddleCenter
        Next i

        OnResize(EventArgs.Empty)
    End Sub

    Property Color() As Color
        Set(ByVal Value As Color)
            ascrbar(0).Value = Value.R
            ascrbar(1).Value = Value.G
            ascrbar(2).Value = Value.B
        End Set
        Get
            Return Color.FromArgb(ascrbar(0).Value, _
                                  ascrbar(1).Value, _
                                  ascrbar(2).Value)
        End Get
    End Property

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)

        Dim cx As Integer = ClientSize.Width
        Dim cy As Integer = ClientSize.Height
        Dim cyFont As Integer = Font.Height
        Dim i As Integer

        For i = 0 To 2
            alblName(i).Location = New Point(i * cx \ 3, cyFont \ 2)
            alblName(i).Size = New Size(cx \ 3, cyFont)
            ascrbar(i).Location = New Point((4 * i + 1) * cx \ 12, _
                                            2 * cyFont)
            ascrbar(i).Size = New Size(cx \ 6, cy - 4 * cyFont)
            alblValue(i).Location = New Point(i * cx \ 3, _
                                              cy - 3 * cyFont \ 2)
            alblValue(i).Size = New Size(cx \ 3, cyFont)
        Next i
    End Sub

    Private Sub ScrollOnValueChanged(ByVal obj As Object, ByVal ea As EventArgs)
        Dim i As Integer
        For i = 0 To 2
            If obj Is ascrbar(i) Then
                alblValue(i).Text = ascrbar(i).Value.ToString()
            End If
        Next i

        RaiseEvent Changed(Me, EventArgs.Empty)
    End Sub
End Class
