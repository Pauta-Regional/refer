'---------------------------------------------
' EmbossedText.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class EmbossedText
    Inherits FontMenuForm

    Private iOffset As Integer = 2

    Shared Shadows Sub Main()
        Application.Run(New EmbossedText())
    End Sub

    Sub New()
        Text = "Embossed Text"
        Width *= 2
        Menu.MenuItems.Add("&Toggle!", AddressOf MenuToggleOnClick)
        strText = "Emboss"
        fnt = New Font("Times New Roman", 108)
    End Sub

    Private Sub MenuToggleOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        iOffset = -iOffset

        If (iOffset > 0) Then
            Text = "Embossed Text"
            strText = "Emboss"
        Else
            Text = "Engraved Text"
            strText = "Engrave"
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim x As Single = (cx - szf.Width) / 2
        Dim y As Single = (cy - szf.Height) / 2

        grfx.Clear(Color.White)
        grfx.DrawString(strText, fnt, Brushes.Gray, x, y)
        grfx.DrawString(strText, fnt, Brushes.White, x - iOffset, _
                                                     y - iOffset)
    End Sub
End Class
