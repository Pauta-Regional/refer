'---------------------------------------------
' TiltedShadow.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TiltedShadow
    Inherits FontMenuForm

    Shared Shadows Sub Main()
        Application.Run(New TiltedShadow())
    End Sub

    Sub New()
        Text = "Tilted Shadow"
        strText = "Shadow"
        fnt = New Font("Times New Roman", 54)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim fAscent As Single = GetAscent(grfx, fnt)

        ' Set baseline 3/4 down client area.
        grfx.TranslateTransform(0, 3 * cy \ 4)

        ' Save the graphics state.
        Dim grfxstate As GraphicsState = grfx.Save()

        ' Set scaling and shear, and draw shadow.
        grfx.MultiplyTransform(New Matrix(1, 0, -3, 3, 0, 0))
        grfx.DrawString(strText, fnt, Brushes.DarkGray, 0, -fAscent)

        ' Draw text without scaling or shear.
        grfx.Restore(grfxstate)
        grfx.DrawString(strText, fnt, Brushes.Black, 0, -fAscent)
    End Sub
End Class
