'---------------------------------------
' Bricks.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class Bricks
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New Bricks())
    End Sub

    Sub New()
        Text = "Bricks"

        strText = "Bricks"
        fnt = New Font("Times New Roman", 144)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim hbr As New HatchBrush(HatchStyle.HorizontalBrick, _
                                  Color.White, Color.Black)

        grfx.DrawString(strText, fnt, hbr, (cx - szf.Width) / 2, _
                                           (cy - szf.Height) / 2)
    End Sub
End Class
