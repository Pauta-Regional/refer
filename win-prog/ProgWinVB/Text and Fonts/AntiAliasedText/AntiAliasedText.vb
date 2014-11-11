'------------------------------------------------
' AntiAliasedText.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Class AntiAliasedText
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new AntiAliasedText())
    End Sub

    Sub New()
        Text = "Anti-Aliased Text"
        Font = new Font("Times New Roman", 12)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim str As String = "A "
        Dim cxText As Integer = CInt(grfx.MeasureString(str, Font).Width)
        Dim i As Integer

        For i = 0 To 5
            grfx.TextRenderingHint = CType(i, TextRenderingHint)
            grfx.DrawString(str, Font, br, i * cxText, 0)
        Next i
    End Sub
End Class
