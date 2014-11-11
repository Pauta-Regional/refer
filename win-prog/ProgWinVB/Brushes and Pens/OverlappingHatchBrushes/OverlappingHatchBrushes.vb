'--------------------------------------------------------
' OverlappingHatchBrushes.vb (c) 2002 by Charles Petzold
'--------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class OverlappingHatchBrushes
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New OverlappingHatchBrushes())
    End Sub

    Sub New()
        Text = "Overlapping Hatch Brushes"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim hbr As New HatchBrush(HatchStyle.HorizontalBrick, Color.White)
        Dim i As Integer

        For i = 0 To 9
            grfx.FillRectangle(hbr, i * cx \ 10, i * cy \ 10, cx \ 8, cy \ 8)
        Next i
    End Sub
End Class
