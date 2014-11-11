'-----------------------------------------------
' UnderlinedText.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Class UnderlinedText
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New UnderlinedText())
    End Sub

    Sub New()
        Text = "Underlined Text Using HotkeyPrefix"
        Font = New Font("Times New Roman", 14)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim str As String = "This is some &u&n&d&e&r&l&i&n&e&d text!"
        Dim strfmt As New StringFormat()
        strfmt.HotkeyPrefix = HotkeyPrefix.Show

        grfx.DrawString(str, Font, New SolidBrush(clr), 0, 0, strfmt)
    End Sub
End Class
