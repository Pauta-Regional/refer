'------------------------------------------------
' TrimmingTheText.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TrimmingTheText
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new TrimmingTheText())
    End Sub

    Sub New()
        Text = "Trimming the Text"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim cyText As Single = Font.GetHeight(grfx)
        Dim cyRect As Single = cyText
        Dim rectf As New RectangleF(0, 0, cx, cyRect)
        Dim str As String = "Those who profess to favor freedom and " & _
                            "yet depreciate agitation. . .want " & _
                            "crops without plowing up the ground, " & _
                            "they want rain without thunder and " & _
                            "lightning. They want the ocean without " & _
                            "the awful roar of its many waters. " & _
                            ChrW(&H2014) & "Frederick Douglass"
        Dim strfmt As New StringFormat()

        strfmt.Trimming = StringTrimming.Character
        grfx.DrawString("Character: " & str, Font, br, rectf, strfmt)
        rectf.Offset(0, cyRect + cyText)

        strfmt.Trimming = StringTrimming.Word
        grfx.DrawString("Word: " & str, Font, br, rectf, strfmt)
        rectf.Offset(0, cyRect + cyText)

        strfmt.Trimming = StringTrimming.EllipsisCharacter
        grfx.DrawString("EllipsisCharacter: " & str, _
                        Font, br, rectf, strfmt)
        rectf.Offset(0, cyRect + cyText)

        strfmt.Trimming = StringTrimming.EllipsisWord
        grfx.DrawString("EllipsisWord: " & str, _
                        Font, br, rectf, strfmt)
        rectf.Offset(0, cyRect + cyText)

        strfmt.Trimming = StringTrimming.EllipsisPath
        grfx.DrawString("EllipsisPath: " & _
                        Environment.GetFolderPath _
                                (Environment.SpecialFolder.Personal), _
                        Font, br, rectf, strfmt)
        rectf.Offset(0, cyRect + cyText)

        strfmt.Trimming = StringTrimming.None
        grfx.DrawString("None: " & str, Font, br, rectf, strfmt)
    End Sub
End Class
