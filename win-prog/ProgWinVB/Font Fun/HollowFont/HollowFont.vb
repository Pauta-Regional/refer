'-------------------------------------------
' HollowFont.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HollowFont
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New HollowFont())
    End Sub

    Sub New()
        Text = "Hollow Font"
        Width *= 2
        strText = "Hollow"
        fnt = New Font("Times New Roman", 108)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim fFontSize As Single = PointsToPageUnits(grfx, fnt)

        ' Get coordinates for a centered string.
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)
        Dim ptf As New PointF((cx - szf.Width) / 2, _
                              (cy - szf.Height) / 2)

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, ptf, New StringFormat())

        ' Draw the path.
        grfx.DrawPath(New Pen(clr), path)
    End Sub
End Class
