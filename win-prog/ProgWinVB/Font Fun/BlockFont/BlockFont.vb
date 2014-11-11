'------------------------------------------
' BlockFont.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BlockFont
    Inherits FontMenuForm

    Const iReps As Integer = 50 ' About 1/2 inch (exactly on printer)

    Shared Shadows Sub Main()
        Application.Run(New BlockFont())
    End Sub

    Sub New()
        Text = "Block Font"
        Width *= 2
        strText = "Block"
        fnt = New Font("Times New Roman", 108)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim x As Single = (cx - szf.Width - iReps) / 2
        Dim y As Single = (cy - szf.Height + iReps) / 2
        Dim i As Integer

        grfx.Clear(Color.LightGray)

        For i = 0 To iReps
            grfx.DrawString(strText, fnt, Brushes.Black, x + i, y - i)
        Next i
        grfx.DrawString(strText, fnt, Brushes.White, x + iReps, y - iReps)
    End Sub
End Class
