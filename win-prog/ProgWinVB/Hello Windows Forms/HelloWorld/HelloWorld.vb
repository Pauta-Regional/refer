'-------------------------------------------
' HelloWorld.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HelloWorld
    Inherits Form

    Shared Sub Main()
        Application.Run(New HelloWorld())
    End Sub

    Sub New()
        Text = "Hello World"
        BackColor = Color.White
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("Hello, Windows Forms!", Font, Brushes.Black, 0, 0)
    End Sub
End Class
