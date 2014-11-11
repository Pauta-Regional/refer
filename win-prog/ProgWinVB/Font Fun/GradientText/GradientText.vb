'---------------------------------------------
' GradientText.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class GradientText
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New GradientText())
    End Sub

    Sub New()
        Text = "Gradient Text"
        Width *= 3
        strText = "Gradient"
        fnt = New Font("Times New Roman", 144, FontStyle.Italic)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim ptf As New PointF((cx - szf.Width) / 2, _
                              (cy - szf.Height) / 2)

        Dim rectf As New RectangleF(ptf, szf)
        Dim lgbr As New LinearGradientBrush(rectf, _
                                Color.White, Color.Black, _
                                LinearGradientMode.ForwardDiagonal)
        grfx.Clear(Color.Gray)
        grfx.DrawString(strText, fnt, lgbr, ptf)
    End Sub
End Class
