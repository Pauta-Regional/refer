'-------------------------------------------
' SquareTile.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class SquareTile
    Inherits PrintableForm

    Const iSide As Integer = 50   ' Side of square

    Shared Shadows Sub Main()
        Application.Run(new SquareTile())
    End Sub

    Sub New()
        Text = "Square Tile"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim apt() As Point = {New Point(0, 0), New Point(iSide, 0), _
                              New Point(iSide, iSide), New Point(0, iSide)}
        Dim pgbr As New PathGradientBrush(apt, WrapMode.TileFlipXY)
        pgbr.SurroundColors = New Color() {Color.Red, Color.Lime, _
                                           Color.Blue, Color.White}
        grfx.FillRectangle(pgbr, 0, 0, cx, cy)
    End Sub
End Class
