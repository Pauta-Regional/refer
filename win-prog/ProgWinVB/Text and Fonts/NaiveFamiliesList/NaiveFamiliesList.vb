'--------------------------------------------------
' NaiveFamiliesList.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NaiveFamiliesList
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New NaiveFamiliesList())
    End Sub

    Sub New()
        Text = "Naive Font Families List"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim y As Single = 0
        Dim aff() As FontFamily = FontFamily.Families
        Dim ff As FontFamily

        For Each ff In aff
            'Try
            Dim fnt As New Font(ff, 12)
            grfx.DrawString(ff.Name, fnt, br, 0, y)
            y += fnt.GetHeight(grfx)
            'Catch
            'End Try
        Next ff
    End Sub
End Class
