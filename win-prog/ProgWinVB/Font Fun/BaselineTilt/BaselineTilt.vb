'---------------------------------------------
' BaselineTilt.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class BaselineTilt
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New BaselineTilt())
    End Sub

    Sub New()
        Text = "Baseline Tilt"
        strText = "Baseline"
        fnt = New Font("Times New Roman", 144)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim yBaseline As Single = 3 * cy \ 4
        Dim cyAscent As Single = GetAscent(grfx, fnt)

        grfx.DrawLine(New Pen(clr), 0, yBaseline, cx, yBaseline)
        grfx.TranslateTransform(0, yBaseline)

        Dim matx As Matrix = grfx.Transform
        matx.Shear(-0.5, 0)
        grfx.Transform = matx

        grfx.DrawString(strText, fnt, New SolidBrush(clr), 0, -cyAscent)
    End Sub
End Class
