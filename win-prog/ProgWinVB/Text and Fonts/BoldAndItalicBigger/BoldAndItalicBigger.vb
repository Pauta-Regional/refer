'----------------------------------------------------
' BoldAndItalicBigger.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BoldAndItalicBigger
    Inherits BoldAndItalic

    Shared Shadows Sub Main()
        Application.Run(new BoldAndItalicBigger())
    End Sub

    Sub New()
        Text &= " Bigger"
        Font = New Font("Times New Roman", 24)
    End Sub
End Class
