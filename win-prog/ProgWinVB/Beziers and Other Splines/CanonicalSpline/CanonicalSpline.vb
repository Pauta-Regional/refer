'------------------------------------------------
' CanonicalSpline.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CanonicalSpline
    Inherits Form

    Protected apt(3) As Point
    Protected fTension As Single = 0.5

    Shared Sub Main()
        Application.Run(New CanonicalSpline())
    End Sub

    Sub New()
        Text = "Canonical Spline"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True

        Dim scrbar As New VScrollBar()
        scrbar.Parent = Me
        scrbar.Dock = DockStyle.Right
        scrbar.Minimum = -100
        scrbar.Maximum = 109
        scrbar.SmallChange = 1
        scrbar.LargeChange = 10
        scrbar.Value = CInt(10 * fTension)
        AddHandler scrbar.ValueChanged, AddressOf ScrollOnValueChanged

        OnResize(EventArgs.Empty)
    End Sub

    Private Sub ScrollOnValueChanged(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim scrbar As ScrollBar = DirectCast(obj, ScrollBar)
        fTension = scrbar.Value / 10.0F
        Invalidate(False)
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
            If ModifierKeys = Keys.Shift Then
                pt = apt(0)
            ElseIf ModifierKeys = Keys.None Then
                pt = apt(1)
            Else
                Return
            End If
        ElseIf mea.Button = MouseButtons.Right Then
            If ModifierKeys = Keys.None Then
                pt = apt(2)
            ElseIf ModifierKeys = Keys.Shift Then
                pt = apt(3)
            Else
                Return
            End If
        Else
            Return
        End If

        Cursor.Position = PointToScreen(pt)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        Dim pt As New Point(mea.X, mea.Y)

        If mea.Button = MouseButtons.Left Then
            If ModifierKeys = Keys.Shift Then
                apt(0) = pt
            ElseIf ModifierKeys = Keys.None Then
                apt(1) = pt
            Else
                Return
            End If
        ElseIf mea.Button = MouseButtons.Right Then
            If ModifierKeys = Keys.None Then
                apt(2) = pt
            ElseIf ModifierKeys = Keys.Shift Then
                apt(3) = pt
            Else
                Return
            End If
        Else
            Return
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim i As Integer

        grfx.DrawCurve(New Pen(ForeColor), apt, fTension)
        grfx.DrawString("Tension = " & fTension.ToString(), Font, br, 0, 0)

        For i = 0 To 3
            grfx.FillEllipse(br, apt(i).X - 3, apt(i).Y - 3, 7, 7)
        Next i
    End Sub
End Class
