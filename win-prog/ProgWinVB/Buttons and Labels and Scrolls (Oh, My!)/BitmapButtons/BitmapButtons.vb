'----------------------------------------------
' BitmapButtons.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BitmapButtons
    Inherits Form

    ReadOnly cxBtn, cyBtn, dxBtn As Integer
    ReadOnly btnLarger, btnSmaller As Button

    Shared Sub Main()
        Application.Run(New BitmapButtons())
    End Sub

    Sub New()
        Text = "Bitmap Buttons"
        ResizeRedraw = True

        dxBtn = Font.Height

        ' Create first button.
        btnLarger = New Button()
        btnLarger.Parent = Me
        btnLarger.Image = New Bitmap(Me.GetType(), "LargerButton.bmp")

        ' Calculate button dimensions based on image dimensions.
        cxBtn = btnLarger.Image.Width + 8
        cyBtn = btnLarger.Image.Height + 8
        btnLarger.Size = New Size(cxBtn, cyBtn)
        AddHandler btnLarger.Click, AddressOf ButtonLargerOnClick

        ' Create second button.
        btnSmaller = New Button()
        btnSmaller.Parent = Me
        btnSmaller.Image = New Bitmap(Me.GetType(), "SmallerButton.bmp")
        btnSmaller.Size = New Size(cxBtn, cyBtn)
        AddHandler btnSmaller.Click, AddressOf ButtonSmallerOnClick
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
