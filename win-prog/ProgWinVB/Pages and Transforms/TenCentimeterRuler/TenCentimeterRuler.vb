'---------------------------------------------------
' TenCentimeterRuler.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TenCentimeterRuler
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New TenCentimeterRuler())
    End Sub

    Sub New()
        Text = "Ten-Centimeter Ruler"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const xOffset As Integer = 10
        Const yOffset As Integer = 10
        Dim i As Integer
        Dim pn As New Pen(clr)
        Dim br As New SolidBrush(clr)
        Dim strfmt As New StringFormat()

        grfx.DrawPolygon(pn, New PointF() _
            { _
                MMConv(grfx, New PointF(xOffset, yOffset)), _
                MMConv(grfx, New PointF(xOffset + 100, yOffset)), _
                MMConv(grfx, New PointF(xOffset + 100, yOffset + 10)), _
                MMConv(grfx, New PointF(xOffset, yOffset + 10)) _
            })
        strfmt.Alignment = StringAlignment.Center

        For i = 1 To 99
            If i Mod 10 = 0 Then
                grfx.DrawLine(pn, _
                    MMConv(grfx, New PointF(xOffset + i, yOffset)), _
                    MMConv(grfx, New PointF(xOffset + i, yOffset + 5)))
                grfx.DrawString((i / 10).ToString(), Font, br, _
                    MMConv(grfx, New PointF(xOffset + i, yOffset + 5)), _
                    strfmt)
            ElseIf i Mod 5 = 0 Then
                grfx.DrawLine(pn, _
                    MMConv(grfx, New PointF(xOffset + i, yOffset)), _
                    MMConv(grfx, New PointF(xOffset + i, yOffset + 3)))
            Else
                grfx.DrawLine(pn, _
                  MMConv(grfx, New PointF(xOffset + i, yOffset)), _
                  MMConv(grfx, New PointF(xOffset + i, yOffset + 2.5F)))
            End If
        Next i
    End Sub

    Private Function MMConv(ByVal grfx As Graphics, _
                            ByVal ptf As PointF) As PointF
        ptf.X *= grfx.DpiX / 25.4F
        ptf.Y *= grfx.DpiY / 25.4F
        Return ptf
    End Function
End Class
