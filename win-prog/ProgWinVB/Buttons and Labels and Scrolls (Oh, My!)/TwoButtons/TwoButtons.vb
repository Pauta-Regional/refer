'-------------------------------------------
' TwoButtons.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwoButtons
    Inherits Form

    ReadOnly btnLarger, btnSmaller As Button
    ReadOnly cxBtn, cyBtn, dxBtn As Integer

    Shared Sub Main()
        Application.Run(New TwoButtons())
    End Sub

    Sub New()
        Text = "Two Buttons"
        ResizeRedraw = True

        cxBtn = 5 * Font.Height
        cyBtn = 2 * Font.Height
        dxBtn = Font.Height

        btnLarger = New Button()
        btnLarger.Parent = Me
        btnLarger.Text = "&Larger"
        btnLarger.Size = New Size(cxBtn, cyBtn)
        AddHandler btnLarger.Click, AddressOf ButtonOnClick

        btnSmaller = New Button()
        btnSmaller.Parent = Me
        btnSmaller.Text = "&Smaller"
        btnSmaller.Size = New Size(cxBtn, cyBtn)
        AddHandler btnSmaller.Click, AddressOf ButtonOnClick

        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)
        btnLarger.Location = _
                New Point(ClientSize.Width \ 2 - cxBtn - dxBtn \ 2, _
                         (ClientSize.Height - cyBtn) \ 2)
        btnSmaller.Location = _
                New Point(ClientSize.Width \ 2 + dxBtn \ 2, _
                         (ClientSize.Height - cyBtn) \ 2)
    End Sub

    Private Sub ButtonOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim btn As Button = DirectCast(obj, Button)

        If btn Is btnLarger Then
            Left -= CInt(0.05 * Width)
            Top -= CInt(0.05 * Height)
            Width += CInt(0.1 * Width)
            Height += CInt(0.1 * Height)
        Else
            Left += CInt(Width / 22)
            Top += CInt(Height / 22)
            Width -= CInt(Width / 11)
            Height -= CInt(Height / 11)
        End If
    End Sub
End Class
