'------------------------------------------
' LineJoins.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class LineJoins
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New LineJoins())
    End Sub

    Sub New()
        Text = "Line Joins: Miter, Bevel, Round, MiterClipped"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pnNarrow As New Pen(clr)
        Dim pnWide As New Pen(Color.Gray, cx \ 16)
        Dim apt() As Point = {New Point(1 * cx \ 32, 1 * cy \ 8), _
                              New Point(4 * cx \ 32, 6 * cy \ 8), _
                              New Point(7 * cx \ 32, 1 * cy \ 8)}
        Dim i As Integer

        For i = 0 To 3
            pnWide.LineJoin = CType(i, LineJoin)
            grfx.DrawLines(pnWide, apt)
            grfx.DrawLines(pnNarrow, apt)
            grfx.TranslateTransform(cx \ 4, 0)
        Next i
    End Sub
End Class
