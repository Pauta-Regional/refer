'------------------------------------------------
' TwoTriangleTile.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TwoTriangleTile
    Inherits PrintableForm
    Const iSide As Integer = 50   ' Side of square for triangle

    Shared Shadows Sub Main()
        Application.Run(New TwoTriangleTile())
    End Sub

    Sub New()
        Text = "Two-Triangle Tile"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        ' Define the triangle and create the first brush.
        Dim apt() As Point = _
            {New Point(0, 0), New Point(iSide, 0), New Point(0, iSide)}
        Dim pgbr1 As New PathGradientBrush(apt, WrapMode.TileFlipXY)

        ' Define another triangle and create the second brush.
        apt = New Point() {New Point(iSide, 0), New Point(iSide, iSide), _
                           New Point(0, iSide)}
        Dim pgbr2 As New PathGradientBrush(apt, WrapMode.TileFlipXY)

        grfx.FillRectangle(pgbr1, 0, 0, cx, cy)
        grfx.FillRectangle(pgbr2, 0, 0, cx, cy)
    End Sub
End Class
