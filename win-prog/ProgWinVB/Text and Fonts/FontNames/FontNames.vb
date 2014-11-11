'------------------------------------------
' FontNames.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FontNames
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new FontNames())
    End Sub

    Sub New()
        Text = "Font Names"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim astrFonts() As String = {"Courier New", "Arial", _
                                     "Times New Roman"}
        Dim afs() As FontStyle = {FontStyle.Regular, FontStyle.Bold, _
                                  FontStyle.Italic, _
                                  FontStyle.Bold Or FontStyle.Italic}
        Dim br As New SolidBrush(clr)
        Dim y As Single = 0
        Dim strFont As String
        Dim fs As FontStyle

        For Each strFont In astrFonts
            For Each fs In afs
                Dim fnt As New Font(strFont, 18, fs)
                grfx.DrawString(strFont, fnt, br, 0, y)
                y += fnt.GetHeight(grfx)
            Next fs
        Next strFont
    End Sub
End Class
