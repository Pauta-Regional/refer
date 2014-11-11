'---------------------------------------------------
' DropShadowWithPath.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class DropShadowWithPath
    Inherits FontMenuForm

    Const iOffset As Integer = 10 ' About 1/10 inch (exactly on printer)

    Shared Shadows Sub Main()
        Application.Run(New DropShadowWithPath())
    End Sub

    Sub New()
        Text = "Drop Shadow with Path"
        Width *= 2
        strText = "Shadow"
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

        ' Add the text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       fFontSize, ptf, New StringFormat())

        ' Clear, fill, translate, fill, and draw.
        grfx.Clear(Color.White)
        grfx.FillPath(Brushes.Black, path)
        path.Transform(New Matrix(1, 0, 0, 1, -10, -10))
        grfx.FillPath(Brushes.White, path)
        grfx.DrawPath(Pens.Black, path)
    End Sub
End Class
