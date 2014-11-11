'---------------------------------------------
' FamiliesList.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FamiliesList
    Inherits PrintableForm

    Const iPointSize As Integer = 12

    Shared Shadows Sub Main()
        Application.Run(New FamiliesList())
    End Sub

    Sub New()
        Text = "Font Families List"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim x As Single = 0
        Dim y As Single = 0
        Dim fMaxWidth As Single = 0
        Dim aff() As FontFamily = GetFontFamilyArray(grfx)
        Dim ff As FontFamily

        For Each ff In aff
            Dim fnt As Font = CreateSampleFont(ff, iPointSize)
            Dim szf As SizeF = grfx.MeasureString(ff.Name, fnt)
            fMaxWidth = Math.Max(fMaxWidth, szf.Width)
        Next ff

        For Each ff In aff
            Dim fnt As Font = CreateSampleFont(ff, iPointSize)
            Dim fHeight As Single = fnt.GetHeight(grfx)

            If y > 0 AndAlso y + fHeight > cy Then
                x += fMaxWidth
                y = 0
            End If
            grfx.DrawString(ff.Name, fnt, br, x, y)
            y += fHeight
        Next ff
    End Sub

    Protected Overridable Function GetFontFamilyArray _
                                (ByVal grfx As Graphics) As FontFamily()
        Return FontFamily.Families
    End Function

    Private Function CreateSampleFont(ByVal ff As FontFamily, _
                                      ByVal fPointSize As Single) As Font
        If ff.IsStyleAvailable(FontStyle.Regular) Then
            Return New Font(ff, fPointSize)
        ElseIf ff.IsStyleAvailable(FontStyle.Bold) Then
            Return New Font(ff, fPointSize, FontStyle.Bold)
        ElseIf ff.IsStyleAvailable(FontStyle.Italic) Then
            Return New Font(ff, fPointSize, FontStyle.Italic)
        Else
            Return Font
        End If
    End Function
End Class
