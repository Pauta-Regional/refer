'------------------------------------------
' FontSizes.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FontSizes
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new FontSizes())
    End Sub

    Sub New()
        Text = "Font Sizes"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim strFont As String = "Times New Roman"
        Dim br As New SolidBrush(clr)
        Dim y As Single = 0
        Dim fSize As Single

        For fSize = 6 To 12 Step 0.25
            Dim fnt As New Font(strFont, fSize)
            grfx.DrawString(strFont & " in " & Str(fSize) & " points", _
                            fnt, br, 0, y)
            y += fnt.GetHeight(grfx)
        Next fSize
    End Sub
End Class
