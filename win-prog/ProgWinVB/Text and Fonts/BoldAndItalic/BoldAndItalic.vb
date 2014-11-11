'----------------------------------------------
' BoldAndItalic.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BoldAndItalic
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New BoldAndItalic())
    End Sub

    Sub New()
        Text = "Bold and Italic Text"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const str1 As String = "This is some "
        Const str2 As String = "bold"
        Const str3 As String = " text and this is some "
        Const str4 As String = "italic"
        Const str5 As String = " text."
        Dim br As New SolidBrush(clr)
        Dim fntRegular As Font = Font
        Dim fntBold As New Font(fntRegular, FontStyle.Bold)
        Dim fntItalic As New Font(fntRegular, FontStyle.Italic)
        Dim x As Single = 0
        Dim y As Single = 0

        grfx.DrawString(str1, fntRegular, br, x, y)
        x += grfx.MeasureString(str1, fntRegular).Width

        grfx.DrawString(str2, fntBold, br, x, y)
        x += grfx.MeasureString(str2, fntBold).Width

        grfx.DrawString(str3, fntRegular, br, x, y)
        x += grfx.MeasureString(str3, fntRegular).Width

        grfx.DrawString(str4, fntItalic, br, x, y)
        x += grfx.MeasureString(str4, fntItalic).Width

        grfx.DrawString(str5, fntRegular, br, x, y)
    End Sub
End Class
