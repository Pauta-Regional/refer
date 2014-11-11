'-------------------------------------------
' DropShadow.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DropShadow
    Inherits FontMenuForm

    Const iOffset As Integer = 10 ' About 1/10 inch (exactly on printer)

    Shared Shadows Sub Main()
        Application.Run(new DropShadow())
    End Sub

    Sub New()
        Text = "Drop Shadow"
        Width *= 2
        strText = "Shadow"
        fnt = New Font("Times New Roman", 108)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim x As Single = (cx - szf.Width) / 2
        Dim y As Single = (cy - szf.Height) / 2

        grfx.Clear(Color.White)
        grfx.DrawString(strText, fnt, Brushes.Gray, x, y)
        grfx.DrawString(strText, fnt, Brushes.Black, x - iOffset, _
                                                     y - iOffset)
    End Sub
End Class
