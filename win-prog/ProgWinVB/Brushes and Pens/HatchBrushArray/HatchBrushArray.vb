'------------------------------------------------
' HatchBrushArray.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HatchBrushArray
    Inherits PrintableForm

    Const iSize As Integer = 32
    Const iMargin As Integer = 8

    ' HatchStyle minimum and maximum values
    Const hsMin As HatchStyle = CType(0, HatchStyle)
    Const hsMax As HatchStyle = CType(52, HatchStyle)

    Shared Shadows Sub Main()
        Application.Run(New HatchBrushArray())
    End Sub

    Sub New()
        Text = "Hatch Brush Array"
        ClientSize = New Size(8 * iSize + 9 * iMargin, _
                              7 * iSize + 8 * iMargin)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim hbr As HatchBrush
        Dim hs As HatchStyle
        Dim x, y As Integer

        For hs = hsMin To hsMax
            hbr = New HatchBrush(hs, Color.White, Color.Black)
            y = hs \ 8
            x = hs Mod 8

            grfx.FillRectangle(hbr, iMargin + x * (iMargin + iSize), _
                                    iMargin + y * (iMargin + iSize), _
                                    iSize, iSize)
        Next hs
    End Sub
End Class
