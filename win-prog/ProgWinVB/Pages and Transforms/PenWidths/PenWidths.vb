'------------------------------------------
' PenWidths.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PenWidths
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New PenWidths())
    End Sub

    Sub New()
        Text = "Pen Widths"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim y As Single = 0
        Dim f As Single

        grfx.PageUnit = GraphicsUnit.Point
        grfx.PageScale = 1

        For f = 0 To 3.1 Step 0.2
            Dim pn As New Pen(clr, f)
            Dim str As String = String.Format("{0:F1}-point-wide pen: ", f)
            Dim szf As SizeF = grfx.MeasureString(str, Font)

            grfx.DrawString(str, Font, br, 0, y)
            grfx.DrawLine(pn, szf.Width, y + szf.Height / 2, _
                          szf.Width + 144, y + szf.Height / 2)
            y += szf.Height
        Next f
    End Sub
End Class
