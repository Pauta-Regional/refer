'---------------------------------------------
' FontMenuForm.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FontMenuForm
    Inherits PrintableForm

    Protected strText As String = "Sample Text"
    Protected fnt As New Font("Times New Roman", 24, FontStyle.Italic)

    Shared Shadows Sub Main()
        Application.Run(New FontMenuForm())
    End Sub

    Sub New()
        Text = "Font Menu Form"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Font!", AddressOf MenuFontOnClick)
    End Sub

    Private Sub MenuFontOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New FontDialog()
        dlg.Font = fnt

        If dlg.ShowDialog() = DialogResult.OK Then
            fnt = dlg.Font
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim br As New SolidBrush(clr)

        grfx.DrawString(strText, fnt, br, (cx - szf.Width) / 2, _
                                          (cy - szf.Height) / 2)
    End Sub

    Function GetAscent(ByVal grfx As Graphics, ByVal fnt As Font) As Single
        Return fnt.GetHeight(grfx) * _
                    fnt.FontFamily.GetCellAscent(fnt.Style) / _
                        fnt.FontFamily.GetLineSpacing(fnt.Style)
    End Function

    Function GetDescent(ByVal grfx As Graphics, ByVal fnt As Font) As Single
        Return fnt.GetHeight(grfx) * _
                    fnt.FontFamily.GetCellDescent(fnt.Style) / _
                        fnt.FontFamily.GetLineSpacing(fnt.Style)
    End Function

    Function PointsToPageUnits(ByVal grfx As Graphics, _
                               ByVal fnt As Font) As Single
        Dim fFontSize As Single

        If grfx.PageUnit = GraphicsUnit.Display Then
            fFontSize = 100 * fnt.SizeInPoints / 72
        Else
            fFontSize = grfx.DpiX * fnt.SizeInPoints / 72
        End If

        Return fFontSize
    End Function
End Class
