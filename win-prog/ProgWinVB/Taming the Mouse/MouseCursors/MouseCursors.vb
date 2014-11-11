'---------------------------------------------
' MouseCursors.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MouseCursors
    Inherits Form

    Private acursor() As Cursor = _
    { _
        Cursors.AppStarting, Cursors.Arrow, Cursors.Cross, _
        Cursors.Default, Cursors.Hand, Cursors.Help, _
        Cursors.HSplit, Cursors.IBeam, Cursors.No, _
        Cursors.NoMove2D, Cursors.NoMoveHoriz, Cursors.NoMoveVert, _
        Cursors.PanEast, Cursors.PanNE, Cursors.PanNorth, _
        Cursors.PanNW, Cursors.PanSE, Cursors.PanSouth, _
        Cursors.PanSW, Cursors.PanWest, Cursors.SizeAll, _
        Cursors.SizeNESW, Cursors.SizeNS, Cursors.SizeNWSE, _
        Cursors.SizeWE, Cursors.UpArrow, Cursors.VSplit, _
        Cursors.WaitCursor _
    }
    Private astrCursor() As String = _
    { _
        "AppStarting", "Arrow", "Cross", "Default", "Hand", _
        "Help", "HSplit", "IBeam", "No", "NoMove2D", _
        "NoMoveHoriz", "NoMoveVert", "PanEast", "PanNE", "PanNorth", _
        "PanNW", "PanSE", "PanSouth", "PanSW", "PanWest", _
        "SizeAll", "SizeNESW", "SizeNS", "SizeNWSE", "SizeWE", _
        "UpArrow", "VSplit", "WaitCursor" _
    }

    Shared Sub Main()
        Application.Run(New MouseCursors())
    End Sub

    Sub New()
        Text = "Mouse Cursors"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        Dim x As Integer = Math.Max(0, _
                           Math.Min(3, mea.X \ (ClientSize.Width \ 4)))
        Dim y As Integer = Math.Max(0, _
                           Math.Min(6, mea.Y \ (ClientSize.Height \ 7)))
        Cursor.Current = acursor(4 * y + x)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim x, y As Integer
        Dim br As New SolidBrush(ForeColor)
        Dim pn As New Pen(ForeColor)
        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        For y = 0 To 6
            For x = 0 To 3
                Dim rect As Rectangle = Rectangle.FromLTRB( _
                                 x * ClientSize.Width \ 4, _
                                 y * ClientSize.Height \ 7, _
                                (x + 1) * ClientSize.Width \ 4, _
                                (y + 1) * ClientSize.Height \ 7)
                grfx.DrawRectangle(pn, rect)
                grfx.DrawString(astrCursor(4 * y + x), Font, _
                                br, RectangleF.op_Implicit(rect), strfmt)
            Next x
        Next y
    End Sub
End Class
