'---------------------------------------------------
' BezierClockControl.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class BezierClockControl
    Inherits ClockControl

    Protected Overrides Sub DrawHourHand(ByVal grfx As Graphics, _
                                         ByVal pn As Pen)
        Dim gs As GraphicsState = grfx.Save()
        grfx.RotateTransform(360.0F * Time.Hour / 12 + _
                             30.0F * Time.Minute / 60)
        grfx.DrawBeziers(pn, New Point() _
            { _
            New Point(0, -600), New Point(0, -300), _
            New Point(200, -300), New Point(50, -200), _
            New Point(50, -200), New Point(50, 0), _
            New Point(50, 0), New Point(50, 75), _
            New Point(-50, 75), New Point(-50, 0), _
            New Point(-50, 0), New Point(-50, -200), _
            New Point(-50, -200), New Point(-200, -300), _
            New Point(0, -300), New Point(0, -600) _
            })
        grfx.Restore(gs)
    End Sub

    Protected Overrides Sub DrawMinuteHand(ByVal grfx As Graphics, _
                                           ByVal pn As Pen)
        Dim gs As GraphicsState = grfx.Save()
        grfx.RotateTransform(360.0F * Time.Minute / 60 + _
                             6.0F * Time.Second / 60)
        grfx.DrawBeziers(pn, New Point() _
            { _
            New Point(0, -800), New Point(0, -750), _
            New Point(0, -700), New Point(25, -600), _
            New Point(25, -600), New Point(25, 0), _
            New Point(25, 0), New Point(25, 50), _
            New Point(-25, 50), New Point(-25, 0), _
            New Point(-25, 0), New Point(-25, -600), _
            New Point(-25, -600), New Point(0, -700), _
            New Point(0, -750), New Point(0, -800) _
            })
        grfx.Restore(gs)
    End Sub
End Class
