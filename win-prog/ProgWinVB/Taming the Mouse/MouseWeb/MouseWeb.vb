'-----------------------------------------
' MouseWeb.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MouseWeb
    Inherits Form

    Private ptMouse As Point = Point.Empty

    Shared Sub Main()
        Application.Run(New MouseWeb())
    End Sub

    Sub New()
        Text = "Mouse Web"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        Dim grfx As Graphics = CreateGraphics()
        DrawWeb(grfx, BackColor, ptMouse)
        ptMouse = New Point(mea.X, mea.Y)
        DrawWeb(grfx, ForeColor, ptMouse)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        DrawWeb(pea.Graphics, ForeColor, ptMouse)
    End Sub

    Private Sub DrawWeb(ByVal grfx As Graphics, ByVal clr As Color, _
                                                ByVal pt As Point)
        Dim cx As Integer = ClientSize.Width
        Dim cy As Integer = ClientSize.Height
        Dim pn As New Pen(clr)

        grfx.DrawLine(pn, pt, New Point(0, 0))
        grfx.DrawLine(pn, pt, New Point(cx \ 4, 0))
        grfx.DrawLine(pn, pt, New Point(cx \ 2, 0))
        grfx.DrawLine(pn, pt, New Point(3 * cx \ 4, 0))
        grfx.DrawLine(pn, pt, New Point(cx, 0))
        grfx.DrawLine(pn, pt, New Point(cx, cy \ 4))
        grfx.DrawLine(pn, pt, New Point(cx, cy \ 2))
        grfx.DrawLine(pn, pt, New Point(cx, 3 * cy \ 4))
        grfx.DrawLine(pn, pt, New Point(cx, cy))
        grfx.DrawLine(pn, pt, New Point(3 * cx \ 4, cy))
        grfx.DrawLine(pn, pt, New Point(cx \ 2, cy))
        grfx.DrawLine(pn, pt, New Point(cx \ 4, cy))
        grfx.DrawLine(pn, pt, New Point(0, cy))
        grfx.DrawLine(pn, pt, New Point(0, cy \ 4))
        grfx.DrawLine(pn, pt, New Point(0, cy \ 2))
        grfx.DrawLine(pn, pt, New Point(0, 3 * cy \ 4))
    End Sub
End Class
