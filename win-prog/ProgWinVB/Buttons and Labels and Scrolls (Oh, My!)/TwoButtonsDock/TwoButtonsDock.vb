'-----------------------------------------------
' TwoButtonsDock.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwoButtonsDock
    Inherits Form

    Shared Sub Main()
        Application.Run(New TwoButtonsDock())
    End Sub

    Sub New()
        Text = "Two Buttons with Dock"
        ResizeRedraw = True

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "&Larger"
        btn.Height = 2 * Font.Height
        btn.Dock = DockStyle.Top
        AddHandler btn.Click, AddressOf ButtonLargerOnClick

        btn = New Button()
        btn.Parent = Me
        btn.Text = "&Smaller"
        btn.Height = 2 * Font.Height
        btn.Dock = DockStyle.Bottom
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
