'-------------------------------------------------
' TwoButtonsAnchor.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwoButtonsAnchor
    Inherits Form

    Shared Sub Main()
        Application.Run(New TwoButtonsAnchor())
    End Sub

    Sub New()
        Text = "Two Buttons with Anchor"
        ResizeRedraw = True

        Dim cxBtn As Integer = 5 * Font.Height
        Dim cyBtn As Integer = 2 * Font.Height
        Dim dxBtn As Integer = Font.Height

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "&Larger"
        btn.Location = New Point(dxBtn, dxBtn)
        btn.Size = New Size(cxBtn, cyBtn)
        AddHandler btn.Click, AddressOf ButtonLargerOnClick

        btn = New Button()
        btn.Parent = Me
        btn.Text = "&Smaller"
        btn.Location = New Point(ClientSize.Width - cxBtn - dxBtn, _
                                 ClientSize.Height - cyBtn - dxBtn)
        btn.Size = New Size(cxBtn, cyBtn)
        btn.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom
        AddHandler btn.Click, AddressOf ButtonSmallerOnClick
    End Sub

    Private Sub ButtonLargerOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Left -= CInt(0.05 * Width)
        Top -= CInt(0.05 * Height)
        Width += CInt(0.1 * Width)
        Height += CInt(0.1 * Height)
    End Sub

    Private Sub ButtonSmallerOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Left += CInt(Width / 22)
        Top += CInt(Height / 22)
        Width -= CInt(Width / 11)
        Height -= CInt(Height / 11)
    End Sub
End Class
