'-------------------------------------------------------
' InheritWithConstructor.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module InheritWithConstructor
    Sub Main()
        Application.Run(New InheritAndConstruct())
    End Sub
End Module

Class InheritAndConstruct
    Inherits Form

    Sub New()
        Text = "Inherit with Constructor"
        BackColor = Color.White
    End Sub
End Class
