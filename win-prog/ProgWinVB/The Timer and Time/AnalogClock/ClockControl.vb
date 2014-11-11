'---------------------------------------------
' ClockControl.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class ClockControl
    Inherits UserControl

    Private dt As DateTime

    Sub New()
        ResizeRedraw = True
        Enabled = False
    End Sub

    Property Time() As DateTime
        Set(ByVal Value As DateTime)
            Dim grfx As Graphics = CreateGraphics()
            Dim pn As New Pen(BackColor)

            InitializeCoordinates(grfx)

            If dt.Hour <> Value.Hour Then
                DrawHourHand(grfx, pn)
            End If

            If dt.Minute <> Value.Minute Then
                DrawHourHand(grfx, pn)
                DrawMinuteHand(grfx, pn)
            End If

            If dt.Second <> Value.Second Then
                DrawMinuteHand(grfx, pn)
                DrawSecondHand(grfx, pn)
            End If

            If dt.Millisecond <> Value.Millisecond Then
                DrawSecondHand(grfx, pn)
            End If

            dt = Value
            pn = New Pen(ForeColor)
            DrawHourHand(grfx, pn)
            DrawMinuteHand(grfx, pn)
            DrawSecondHand(grfx, pn)
            grfx.Dispose()
        End Set
        Get
            Return dt
        End Get
    End Property

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)
        Dim br As New SolidBrush(ForeColor)

        InitializeCoordinates(grfx)
        DrawDots(grfx, br)
        DrawHourHand(grfx, pn)
        DrawMinuteHand(grfx, pn)
        DrawSecondHand(grfx, pn)
    End Sub

    Private Sub InitializeCoordinates(ByVal grfx As Graphics)
        If Width = 0 OrElse Height = 0 Then Return

        grfx.TranslateTransform(Width \ 2, Height \ 2)
        Dim fInches As Single = Math.Min(Width / grfx.DpiX, _
                                         Height / grfx.DpiY)
        grfx.ScaleTransform(fInches * grfx.DpiX / 2000, _
                            fInches * grfx.DpiY / 2000)
    End Sub

    Private Sub DrawDots(ByVal grfx As Graphics, ByVal br As Brush)
        Dim i, iSize As Integer

        For i = 0 To 59
            If i Mod 5 = 0 Then iSize = 100 Else iSize = 30

            grfx.FillEllipse(br, 0 - iSize \ 2, -900 - iSize \ 2, _
                                 iSize, iSize)
            grfx.RotateTransform(6)
        Next i
    End Sub

    Protected Overridable Sub DrawHourHand(ByVal grfx As Graphics, _
                                           ByVal pn As Pen)
        Dim gs As GraphicsState = grfx.Save()
        grfx.RotateTransform(360.0F * Time.Hour / 12 + _
                             30.0F * Time.Minute / 60)
        grfx.DrawPolygon(pn, New Point() { _
                         New Point(0, 150), New Point(100, 0), _
                         New Point(0, -600), New Point(-100, 0)})
        grfx.Restore(gs)
    End Sub

    Protected Overridable Sub DrawMinuteHand(ByVal grfx As Graphics, _
                                             ByVal pn As Pen)
        Dim gs As GraphicsState = grfx.Save()
        grfx.RotateTransform(360.0F * Time.Minute / 60 + _
                             6.0F * Time.Second / 60)
        grfx.DrawPolygon(pn, New Point() { _
                         New Point(0, 200), New Point(50, 0), _
                         New Point(0, -800), New Point(-50, 0)})
        grfx.Restore(gs)
    End Sub

    Protected Overridable Sub DrawSecondHand(ByVal grfx As Graphics, _
                                             ByVal pn As Pen)
        Dim gs As GraphicsState = grfx.Save()
        grfx.RotateTransform(360.0F * Time.Second / 60 + _
                             6.0F * Time.Millisecond / 1000)
        grfx.DrawLine(pn, 0, 0, 0, -800)
        grfx.Restore(gs)
    End Sub
End Class
