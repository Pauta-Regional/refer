'--------------------------------------------
' LineArcPath.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class LineArcPath
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New LineArcPath())
    End Sub

    Sub New()
        Text = "Line & Arc in Path"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        Dim pn As New Pen(clr, 25)

        path.AddLine(25, 100, 125, 100)
        path.AddArc(125, 50, 100, 100, -180, 180)
        path.AddLine(225, 100, 325, 100)

        grfx.DrawPath(pn, path)
    End Sub
End Class
