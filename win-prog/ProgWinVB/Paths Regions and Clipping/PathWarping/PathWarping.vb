'--------------------------------------------
' PathWarping.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class PathWarping
    Inherits Form

    Private miWarpMode As MenuItem
    Private path As GraphicsPath
    Private aptfDest(4) As PointF

    Shared Sub Main()
        Application.Run(New PathWarping())
    End Sub

    Sub New()
        Text = "Path Warping"

        ' Create menu.
        Menu = New MainMenu()
        Menu.MenuItems.Add("&Warp Mode")
        Dim ehClick As EventHandler = AddressOf MenuWarpModeOnClick
        miWarpMode = New MenuItem("&" & CType(0, WarpMode).ToString(), _
                                  ehClick)
        miWarpMode.RadioCheck = True
        miWarpMode.Checked = True
        Menu.MenuItems(0).MenuItems.Add(miWarpMode)
        Dim mi As New MenuItem("&" & CType(1, WarpMode).ToString(), ehClick)
        mi.RadioCheck = True
        Menu.MenuItems(0).MenuItems.Add(mi)

        ' Create path.
        path = New GraphicsPath()
        Dim i As Integer
        For i = 0 To 8
            path.StartFigure()
            path.AddLine(0, 100 * i, 800, 100 * i)
            path.StartFigure()
            path.AddLine(100 * i, 0, 100 * i, 800)
        Next i

        ' Initialize PointF array.
        aptfDest(0) = New PointF(50, 50)
        aptfDest(1) = New PointF(200, 50)
        aptfDest(2) = New PointF(50, 200)
        aptfDest(3) = New PointF(200, 200)
    End Sub

    Private Sub MenuWarpModeOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        miWarpMode.Checked = False
        miWarpMode = DirectCast(obj, MenuItem)
        miWarpMode.Checked = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        Dim pt As Point

        If mea.Button = MouseButtons.Left Then
            If ModifierKeys = Keys.None Then
                pt = Point.Round(aptfDest(0))
            ElseIf ModifierKeys = Keys.Shift Then
                pt = Point.Round(aptfDest(2))
            Else
                Return
            End If
        ElseIf mea.Button = MouseButtons.Right Then
            If ModifierKeys = Keys.None Then
                pt = Point.Round(aptfDest(1))
            ElseIf ModifierKeys = Keys.Shift Then
                pt = Point.Round(aptfDest(3))
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
            If ModifierKeys = Keys.None Then
                aptfDest(0) = Point.op_Implicit(pt)
            ElseIf ModifierKeys = Keys.Shift Then
                aptfDest(2) = Point.op_Implicit(pt)
            Else
                Return
            End If
        ElseIf mea.Button = MouseButtons.Right Then
            If ModifierKeys = Keys.None Then
                aptfDest(1) = Point.op_Implicit(pt)
            ElseIf ModifierKeys = Keys.Shift Then
                aptfDest(3) = Point.op_Implicit(pt)
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
        Dim pathWarped As GraphicsPath = _
                                DirectCast(path.Clone(), GraphicsPath)
        Dim wm As WarpMode = CType(miWarpMode.Index, WarpMode)

        pathWarped.Warp(aptfDest, path.GetBounds(), New Matrix(), wm)
        grfx.DrawPath(New Pen(ForeColor), pathWarped)
    End Sub
End Class
