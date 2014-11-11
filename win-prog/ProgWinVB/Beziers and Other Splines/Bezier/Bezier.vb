'---------------------------------------
' Bezier.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Bezier
    Inherits Form

    Protected apt(3) As Point

    Shared Sub Main()
        Application.Run(New Bezier())
    End Sub

    Sub New()
        Text = "Bezier (Mouse Defines Control Points)"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True

        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)

        Dim cx As Integer = ClientSize.Width
        Dim cy As Integer = ClientSize.Height

        apt(0) = New Point(cx \ 4, cy \ 2)
        apt(1) = New Point(cx \ 2, cy \ 4)
        apt(2) = New Point(cx \ 2, 3 * cy \ 4)
        apt(3) = New Point(3 * cx \ 4, cy \ 2)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        Dim pt As Point

        If mea.Button = MouseButtons.Left Then
            pt = apt(1)
        ElseIf mea.Button = MouseButtons.Right Then
            pt = apt(2)
        Else
            Return
        End If

        Cursor.Position = PointToScreen(pt)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If mea.Button = MouseButtons.Left Then
            apt(1) = New Point(mea.X, mea.Y)
            Invalidate()
        ElseIf mea.Button = MouseButtons.Right Then
            apt(2) = New Point(mea.X, mea.Y)
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawBeziers(New Pen(ForeColor), apt)

        Dim pn As New Pen(Color.FromArgb(128, ForeColor))
        grfx.DrawLine(pn, apt(0), apt(1))
        grfx.DrawLine(pn, apt(2), apt(3))
    End Sub
End Class
