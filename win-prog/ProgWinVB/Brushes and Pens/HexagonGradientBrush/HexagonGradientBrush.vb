'-----------------------------------------------------
' HexagonGradientBrush.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HexagonGradientBrush
    Inherits PrintableForm

    Const fSide As Single = 50    ' Side (also radius) of hexagon

    Shared Shadows Sub Main()
        Application.Run(New HexagonGradientBrush())
    End Sub

    Sub New()
        Text = "Hexagon Gradient Brush"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        ' Calculate half the hexagon height.
        Dim fHalf As Single = CSng(fSide * Math.Sin(Math.PI / 3))

        ' Define a hexagon including some extra width.
        Dim aptf() As PointF = {New PointF(fSide, 0), _
                                New PointF(fSide * 1.5, 0), _
                                New PointF(fSide, 0), _
                                New PointF(fSide / 2, -fHalf), _
                                New PointF(-fSide / 2, -fHalf), _
                                New PointF(-fSide, 0), _
                                New PointF(-fSide * 1.5, 0), _
                                New PointF(-fSide, 0), _
                                New PointF(-fSide / 2, fHalf), _
                                New PointF(fSide / 2, fHalf)}

        ' Create the first brush.
        Dim pgbr1 As PathGradientBrush = _
                            New PathGradientBrush(aptf, WrapMode.Tile)

        ' Offset the hexagon and define the second brush.
        Dim i As Integer
        For i = 0 To aptf.GetUpperBound(0)
            aptf(i).X += fSide * 1.5F
            aptf(i).Y += fHalf
        Next i
        Dim pgbr2 As PathGradientBrush = _
                            New PathGradientBrush(aptf, WrapMode.Tile)

        grfx.FillRectangle(pgbr1, 0, 0, cx, cy)
        grfx.FillRectangle(pgbr2, 0, 0, cx, cy)
    End Sub
End Class
