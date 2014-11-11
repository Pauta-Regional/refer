'--------------------------------------------
' GradientPen.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class GradientPen
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New GradientPen())
    End Sub

    Sub New()
        Text = "Gradient Pen"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim lgbr As New LinearGradientBrush( _
                                New Rectangle(0, 0, cx, cy), _
                                Color.White, Color.Black, _
                                LinearGradientMode.BackwardDiagonal)
        Dim pn As New Pen(lgbr, Math.Min(cx, cy) \ 25)
        pn.Alignment = PenAlignment.Inset

        grfx.DrawRectangle(pn, 0, 0, cx, cy)
        grfx.DrawLine(pn, 0, 0, cx, cy)
        grfx.DrawLine(pn, 0, cy, cx, 0)
    End Sub
End Class
