'---------------------------------------------
' SixInchRuler.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SixInchRuler
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New SixInchRuler())
    End Sub

    Sub New()
        Text = "Six-Inch Ruler"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Const xOffset As Integer = 16
        Const yOffset As Integer = 16
        Dim i As Integer
        Dim pn As New Pen(clr, 0.5)
        Dim br As New SolidBrush(clr)
        Dim strfmt As New StringFormat()

        grfx.PageUnit = GraphicsUnit.Inch
        grfx.PageScale = 1 / 64
        grfx.DrawRectangle(pn, xOffset, yOffset, 6 * 64, 64)
        strfmt.Alignment = StringAlignment.Center

        For i = 1 To 95
            Dim x As Integer = xOffset + i * 4
            Dim y As Integer = yOffset
            Dim dy As Integer

            If i Mod 16 = 0 Then
                dy = 32
                grfx.DrawString((i / 16).ToString(), Font, br, _
                                        x, y + dy, strfmt)
            ElseIf i Mod 8 = 0 Then
                dy = 24
            ElseIf i Mod 4 = 0 Then
                dy = 20
            ElseIf i Mod 2 = 0 Then
                dy = 16
            Else
                dy = 12
            End If
            grfx.DrawLine(pn, x, y, x, y + dy)
        Next i
    End Sub
End Class
