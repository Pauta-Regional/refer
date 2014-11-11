'--------------------------------------------
' FourByFours.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FourByFours
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New FourByFours())
    End Sub

    Sub New()
        Text = "Four by Fours"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr)
        Dim br As New SolidBrush(clr)

        grfx.DrawRectangle(pn, New Rectangle(2, 2, 4, 4))
        grfx.DrawRectangles(pn, New Rectangle() _
                                    {New Rectangle(8, 2, 4, 4)})
        grfx.DrawEllipse(pn, New Rectangle(14, 2, 4, 4))
        grfx.FillRectangle(br, New Rectangle(2, 8, 4, 4))
        grfx.FillRectangles(br, New Rectangle() _
                                    {New Rectangle(8, 8, 4, 4)})
        grfx.FillEllipse(br, New Rectangle(14, 8, 4, 4))
    End Sub
End Class
