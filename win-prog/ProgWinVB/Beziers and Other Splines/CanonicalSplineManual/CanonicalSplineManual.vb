'------------------------------------------------------
' CanonicalSplineManual.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CanonicalSplineManual
    Inherits CanonicalSpline

    Shared Shadows Sub Main()
        Application.Run(new CanonicalSplineManual())
    End Sub

    Sub New()
        Text = "Canonical Spline ""Manually"" Drawn"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        MyBase.OnPaint(pea)
        CanonicalSpline(pea.Graphics, Pens.Red, apt, fTension)
    End Sub

    Private Sub CanonicalSpline(ByVal grfx As Graphics, ByVal pn As Pen, _
            ByVal apt() As Point, ByVal fTension As Single)
        CanonicalSegment(grfx, pn, apt(0), apt(0), apt(1), apt(2), fTension)
        CanonicalSegment(grfx, pn, apt(0), apt(1), apt(2), apt(3), fTension)
        CanonicalSegment(grfx, pn, apt(1), apt(2), apt(3), apt(3), fTension)
    End Sub

    Private Sub CanonicalSegment(ByVal grfx As Graphics, ByVal pn As Pen, _
                                 ByVal pt0 As Point, ByVal pt1 As Point, _
                                 ByVal pt2 As Point, ByVal pt3 As Point, _
                                 ByVal fTension As Single)
        Dim apt(9) As Point

        Dim SX1 As Single = fTension * (pt2.X - pt0.X)
        Dim SY1 As Single = fTension * (pt2.Y - pt0.Y)
        Dim SX2 As Single = fTension * (pt3.X - pt1.X)
        Dim SY2 As Single = fTension * (pt3.Y - pt1.Y)
        Dim AX As Single = SX1 + SX2 + 2 * pt1.X - 2 * pt2.X
        Dim AY As Single = SY1 + SY2 + 2 * pt1.Y - 2 * pt2.Y
        Dim BX As Single = -2 * SX1 - SX2 - 3 * pt1.X + 3 * pt2.X
        Dim BY As Single = -2 * SY1 - SY2 - 3 * pt1.Y + 3 * pt2.Y
        Dim CX As Single = SX1
        Dim CY As Single = SY1
        Dim DX As Single = pt1.X
        Dim DY As Single = pt1.Y
        Dim i As Integer

        For i = 0 To apt.GetUpperBound(0)
            Dim t As Single = CSng(i / apt.GetUpperBound(0))
            apt(i).X = CInt(AX * t * t * t + BX * t * t + CX * t + DX)
            apt(i).Y = CInt(AY * t * t * t + BY * t * t + CY * t + DY)
        Next i

        grfx.DrawLines(pn, apt)
    End Sub
End Class
