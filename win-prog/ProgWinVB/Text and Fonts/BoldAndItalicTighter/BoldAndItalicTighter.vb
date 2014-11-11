'-----------------------------------------------------
' BoldAndItalicTighter.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Class BoldAndItalicTighter
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new BoldAndItalicTighter())
    End Sub

    Sub New()
        Text = "Bold and Italic (Tighter)"
        Font = new Font("Times New Roman", 24)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const str1 As String = "This is some "
        Const str2 As String = "bold"
        Const str3 As String = " text, and Me is some "
        Const str4 As String = "italic"
        Const str5 As String = " text."
        Dim br As New SolidBrush(clr)
        Dim fntRegular As Font = Font
        Dim fntBold As New Font(fntRegular, FontStyle.Bold)
        Dim fntItalic As New Font(fntRegular, FontStyle.Italic)
        Dim ptf As New PointF(0, 0)
        Dim strfmt As StringFormat = StringFormat.GenericTypographic
        strfmt.FormatFlags = strfmt.FormatFlags Or _
                            StringFormatFlags.MeasureTrailingSpaces

        grfx.DrawString(str1, fntRegular, br, ptf, strfmt)
        ptf.X += grfx.MeasureString(str1, fntRegular, ptf, strfmt).Width

        grfx.DrawString(str2, fntBold, br, ptf, strfmt)
        ptf.X += grfx.MeasureString(str2, fntBold, ptf, strfmt).Width

        grfx.DrawString(str3, fntRegular, br, ptf, strfmt)
        ptf.X += grfx.MeasureString(str3, fntRegular, ptf, strfmt).Width

        grfx.DrawString(str4, fntItalic, br, ptf, strfmt)
        ptf.X += grfx.MeasureString(str4, fntItalic, ptf, strfmt).Width

        grfx.DrawString(str5, fntRegular, br, ptf, strfmt)
    End Sub
End Class
