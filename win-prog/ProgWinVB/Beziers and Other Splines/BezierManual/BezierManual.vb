'---------------------------------------------
' BezierManual.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BezierManual
    Inherits Bezier

    Shared Shadows Sub Main()
        Application.Run(New BezierManual())
    End Sub

    Sub New()
        Text = "Bezier Curve ""Manually"" Drawn"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        MyBase.OnPaint(pea)
        BezierSpline(pea.Graphics, Pens.Red, apt)
    End Sub

    Private Sub BezierSpline(ByVal grfx As Graphics, _
                             ByVal pn As Pen, ByVal apt4Pts() As Point)
        Dim apt(99) As Point
        Dim i As Integer

        For i = 0 To apt.GetUpperBound(0)
            Dim t As Single = CSng(i / apt.GetUpperBound(0))
            Dim x As Single = (1 - t) * (1 - t) * (1 - t) * apt4Pts(0).X + _
                              3 * t * (1 - t) * (1 - t) * apt4Pts(1).X + _
                              3 * t * t * (1 - t) * apt4Pts(2).X + _
                              t * t * t * apt4Pts(3).X
            Dim y As Single = (1 - t) * (1 - t) * (1 - t) * apt4Pts(0).Y + _
                              3 * t * (1 - t) * (1 - t) * apt4Pts(1).Y + _
                              3 * t * t * (1 - t) * apt4Pts(2).Y + _
                              t * t * t * apt4Pts(3).Y
            apt(i) = New Point(CInt(x), CInt(y))
        Next i
        grfx.DrawLines(pn, apt)
    End Sub
End Class
