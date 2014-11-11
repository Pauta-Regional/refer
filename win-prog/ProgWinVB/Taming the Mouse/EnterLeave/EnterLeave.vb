'-------------------------------------------
' EnterLeave.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class EnterLeave
    Inherits Form

    Private bInside As Boolean = False

    Shared Sub Main()
        Application.Run(New EnterLeave())
    End Sub

    Sub New()
        Text = "Enter/Leave"
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal ea As EventArgs)
        bInside = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal ea As EventArgs)
        bInside = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseHover(ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        grfx.Clear(Color.Red)
        System.Threading.Thread.Sleep(100)
        grfx.Clear(Color.Green)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics

        If bInside Then
            grfx.Clear(Color.Green)
        Else
            grfx.Clear(BackColor)
        End If
    End Sub
End Class
