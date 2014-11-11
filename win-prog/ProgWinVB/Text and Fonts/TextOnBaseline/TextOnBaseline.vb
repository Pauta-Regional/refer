'-----------------------------------------------
' TextOnBaseline.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TextOnBaseline
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TextOnBaseline())
    End Sub

    Sub New()
        Text = "Text on Baseline"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim yBaseline As Single = cy \ 2
        Dim pn As New Pen(clr)

        ' Draw the baseline across the center of the client area.
        grfx.DrawLine(pn, 0, yBaseline, cx, yBaseline)

        ' Create a 144-point font.
        Dim fnt As New Font("Times New Roman", 144)

        ' Get and calculate some metrics.
        Dim cyLineSpace As Single = fnt.GetHeight(grfx)
        Dim iCellSpace As Integer = fnt.FontFamily.GetLineSpacing(fnt.Style)
        Dim iCellAscent As Integer = fnt.FontFamily.GetCellAscent(fnt.Style)
        Dim cyAscent As Single = cyLineSpace * iCellAscent / iCellSpace

        ' Display the text on the baseline.
        grfx.DrawString("Baseline", fnt, New SolidBrush(clr), _
                        0, yBaseline - cyAscent)
    End Sub
End Class
