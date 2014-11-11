'-------------------------------------------------
' OwnerDrawButtons.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class OwnerDrawButtons
    Inherits Form

    ReadOnly cxImage, cyImage As Integer
    ReadOnly cxBtn, cyBtn, dxBtn As Integer
    ReadOnly btnLarger, btnSmaller As Button

    Shared Sub Main()
        Application.Run(New OwnerDrawButtons())
    End Sub

    Sub New()
        Text = "Owner-Draw Buttons"
        ResizeRedraw = True

        cxImage = 4 * Font.Height
        cyImage = 4 * Font.Height
        cxBtn = cxImage + 8
        cyBtn = cyImage + 8
        dxBtn = Font.Height

        btnLarger = New Button()
        btnLarger.Parent = Me
        btnLarger.Size = New Size(cxBtn, cyBtn)
        AddHandler btnLarger.Click, AddressOf ButtonLargerOnClick
        AddHandler btnLarger.Paint, AddressOf ButtonOnPaint

        btnSmaller = New Button()
        btnSmaller.Parent = Me
        btnSmaller.Size = New Size(cxBtn, cyBtn)
        AddHandler btnSmaller.Click, AddressOf ButtonSmallerOnClick
        AddHandler btnSmaller.Paint, AddressOf ButtonOnPaint

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

    Private Sub ButtonLargerOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Left -= CInt(0.05 * Width)
        Top -= CInt(0.05 * Height)
        Width += CInt(0.1 * Width)
        Height += CInt(0.1 * Height)
    End Sub

    Private Sub ButtonSmallerOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Left += CInt(Width / 22)
        Top += CInt(Height / 22)
        Width -= CInt(Width / 11)
        Height -= CInt(Height / 11)
    End Sub

    Private Sub ButtonOnPaint(ByVal obj As Object, _
                              ByVal pea As PaintEventArgs)
        Dim btn As Button = DirectCast(obj, Button)
        Dim grfx As Graphics = pea.Graphics
        Dim bs As ButtonState

        If btn Is DirectCast(GetChildAtPoint( _
                        PointToClient(MousePosition)), Button) AndAlso _
                        btn.Capture Then
            bs = ButtonState.Pushed
        Else
            bs = ButtonState.Normal
        End If

        ControlPaint.DrawButton(grfx, 0, 0, cxBtn, cyBtn, bs)

        Dim grfxstate As GraphicsState = grfx.Save()
        grfx.TranslateTransform((cxBtn - cxImage) \ 2, _
                                (cyBtn - cyImage) \ 2)

        If btn Is btnLarger Then
            DrawLargerButton(grfx, cxImage, cyImage)
        Else
            DrawSmallerButton(grfx, cxImage, cyImage)
        End If
        grfx.Restore(grfxstate)

        If btn.Focused Then
            ControlPaint.DrawFocusRectangle(grfx, _
                New Rectangle((cxBtn - cxImage) \ 2 + cxImage \ 16, _
                              (cyBtn - cyImage) \ 2 + cyImage \ 16, _
                              7 * cxImage \ 8, 7 * cyImage \ 8))
        End If
    End Sub

    Private Sub DrawLargerButton(ByVal grfx As Graphics, _
                            ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(btnLarger.ForeColor)
        Dim pn As New Pen(btnLarger.ForeColor)
        Dim i As Integer

        grfx.TranslateTransform(cx \ 2, cy \ 2)

        For i = 0 To 3
            grfx.DrawLine(pn, 0, 0, cx \ 4, 0)
            grfx.FillPolygon(br, New Point() { _
                                New Point(cx \ 4, -cy \ 8), _
                                New Point(cx \ 2, 0), _
                                New Point(cx \ 4, cy \ 8)})
            grfx.RotateTransform(90)
        Next i
    End Sub

    Private Sub DrawSmallerButton(ByVal grfx As Graphics, _
                            ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(btnSmaller.ForeColor)
        Dim pn As New Pen(btnSmaller.ForeColor)
        Dim i As Integer

        grfx.TranslateTransform(cx \ 2, cy \ 2)

        For i = 0 To 3
            grfx.DrawLine(pn, 3 * cx \ 8, 0, cx \ 2, 0)
            grfx.FillPolygon(br, New Point() { _
                                New Point(3 * cx \ 8, -cy \ 8), _
                                New Point(cx \ 8, 0), _
                                New Point(3 * cx \ 8, cy \ 8)})
            grfx.RotateTransform(90)
        Next i
    End Sub
End Class
