'--------------------------------------------
' RotatedFont.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RotatedFont
    Inherits FontMenuForm

    Const iDegrees As Integer = 20     ' Should be divisor of 360

    Shared Shadows Sub Main()
        Application.Run(New RotatedFont())
    End Sub

    Sub New()
        Text = "Rotated Font"
        strText = "   Rotated Font"
        fnt = New Font("Arial", 18)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim strfmt As New StringFormat()
        Dim i As Integer

        strfmt.LineAlignment = StringAlignment.Center
        grfx.TranslateTransform(cx \ 2, cy \ 2)

        For i = 0 To 359 Step iDegrees
            grfx.DrawString(strText, fnt, br, 0, 0, strfmt)
            grfx.RotateTransform(iDegrees)
        Next i
    End Sub
End Class
