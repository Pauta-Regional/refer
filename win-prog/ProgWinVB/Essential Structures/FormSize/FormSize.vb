'-----------------------------------------
' FormSize.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FormSize
    Inherits Form

    Shared Sub Main()
        Application.Run(New FormSize())
    End Sub

    Sub New()
        Text = "Form Size"
        BackColor = Color.White
    End Sub

    Protected Overrides Sub OnMove(ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim str As String = _
            "Location: " & Location.ToString() & vbLf & _
            "Size: " & Size.ToString() & vbLf & _
            "Bounds: " & Bounds.ToString() & vbLf & _
            "Width: " & Width.ToString() & vbLf & _
            "Height: " & Height.ToString() & vbLf & _
            "Left: " & Left.ToString() & vbLf & _
            "Top: " & Top.ToString() & vbLf & _
            "Right: " & Right.ToString() & vbLf & _
            "Bottom: " & Bottom.ToString() & vbLf & vbLf & _
            "DesktopLocation: " & DesktopLocation.ToString() & vbLf & _
            "DesktopBounds: " & DesktopBounds.ToString() & vbLf & vbLf & _
            "ClientSize: " & ClientSize.ToString() & vbLf & _
            "ClientRectangle: " & ClientRectangle.ToString()

        grfx.DrawString(str, Font, Brushes.Black, 0, 0)
    End Sub
End Class
