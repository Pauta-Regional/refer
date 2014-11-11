'-----------------------------------------
' LineCaps.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class LineCaps
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New LineCaps())
    End Sub

    Sub New()
        Text = "Line Caps"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pnWide As New Pen(Color.Gray, Font.Height)
        Dim pnNarrow As New Pen(clr)
        Dim br As New SolidBrush(clr)
        Dim lc As LineCap

        For Each lc In System.Enum.GetValues(GetType(LineCap))
            grfx.DrawString(lc.ToString(), Font, br, _
                            Font.Height, Font.Height \ 2)
            pnWide.StartCap = lc
            pnWide.EndCap = lc
            grfx.DrawLine(pnWide, 2 * cx \ 4, Font.Height, _
                                  3 * cx \ 4, Font.Height)
            grfx.DrawLine(pnNarrow, 2 * cx \ 4, Font.Height, _
                                    3 * cx \ 4, Font.Height)
            grfx.TranslateTransform(0, 2 * Font.Height)
        Next lc
    End Sub
End Class
