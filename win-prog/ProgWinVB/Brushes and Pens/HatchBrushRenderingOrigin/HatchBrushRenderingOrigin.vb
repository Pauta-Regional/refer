'----------------------------------------------------------
' HatchBrushRenderingOrigin.vb (c) 2002 by Charles Petzold
'----------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HatchBrushRenderingOrigin
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new HatchBrushRenderingOrigin())
    End Sub

    Sub New()
        Text = "Hatch Brush Rendering Origin"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim hbr As New HatchBrush(HatchStyle.HorizontalBrick, Color.White)
        Dim i As Integer

        For i = 0 To 9
            grfx.RenderingOrigin = New Point(i * cx \ 10, i * cy \ 10)
            grfx.FillRectangle(hbr, i * cx \ 10, i * cy \ 10, cx \ 8, cy \ 8)
        Next i
    End Sub
End Class
