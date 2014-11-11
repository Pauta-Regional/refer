'-------------------------------------------------------
' TenCentimeterRulerAuto.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TenCentimeterRulerAuto
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TenCentimeterRulerAuto())
    End Sub

    Sub New()
        Text = "Ten-Centimeter Ruler (Auto)"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const xOffset As Integer = 10
        Const yOffset As Integer = 10
        Dim i As Integer
        Dim pn As New Pen(clr, 0.25)
        Dim br As New SolidBrush(clr)
        Dim strfmt As New StringFormat()

        grfx.PageUnit = GraphicsUnit.Millimeter
        grfx.PageScale = 1
        grfx.DrawRectangle(pn, xOffset, yOffset, 100, 10)

        strfmt.Alignment = StringAlignment.Center

        For i = 1 To 99
            If i Mod 10 = 0 Then
                grfx.DrawLine(pn, _
                            New PointF(xOffset + i, yOffset), _
                            New PointF(xOffset + i, yOffset + 5))
                grfx.DrawString((i / 10).ToString(), Font, br, _
                             New PointF(xOffset + i, yOffset + 5), _
                             strfmt)
            ElseIf i Mod 5 = 0 Then
                grfx.DrawLine(pn, _
                            New PointF(xOffset + i, yOffset), _
                            New PointF(xOffset + i, yOffset + 3))
            Else
                grfx.DrawLine(pn, _
                            New PointF(xOffset + i, yOffset), _
                            New PointF(xOffset + i, yOffset + 2.5F))
            End If
        Next i
    End Sub
End Class
