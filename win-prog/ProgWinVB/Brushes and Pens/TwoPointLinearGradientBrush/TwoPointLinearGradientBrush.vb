'------------------------------------------------------------
' TwoPointLinearGradientBrush.vb (c) 2002 by Charles Petzold
'------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TwoPointLinearGradientBrush
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TwoPointLinearGradientBrush())
    End Sub

    Sub New()
        Text = "Two-Point Linear Gradient Brush"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim lgbr As New LinearGradientBrush( _
                                New Point(cx \ 4, cy \ 4), _
                                New Point(3 * cx \ 4, 3 * cy \ 4), _
                                Color.White, Color.Black)
        grfx.FillRectangle(lgbr, 0, 0, cx, cy)
    End Sub
End Class
