'--------------------------------------------------
' InheritHelloWorld.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class InheritHelloWorld
    Inherits HelloWorld

    Shared Shadows Sub Main()
        Application.Run(New InheritHelloWorld())
    End Sub

    Sub New()
        Text = "Inherit " & Text
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("Hello from InheritHelloWorld!", _
                        Font, Brushes.Black, 0, 100)
    End Sub
End Class
