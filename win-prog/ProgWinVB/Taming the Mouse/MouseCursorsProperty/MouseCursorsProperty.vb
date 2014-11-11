'-----------------------------------------------------
' MouseCursorsProperty.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MouseCursorsProperty
    Inherits Form

    Private actrl(28) As Label

    Shared Sub Main()
        Application.Run(New MouseCursorsProperty())
    End Sub

    Sub New()
        Dim i As Integer
        Dim acursor() As Cursor = _
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
        Dim astrCursor() As String = _
        { _
            "AppStarting", "Arrow", "Cross", "Default", "Hand", _
            "Help", "HSplit", "IBeam", "No", "NoMove2D", _
            "NoMoveHoriz", "NoMoveVert", "PanEast", "PanNE", "PanNorth", _
            "PanNW", "PanSE", "PanSouth", "PanSW", "PanWest", _
            "SizeAll", "SizeNESW", "SizeNS", "SizeNWSE", "SizeWE", _
            "UpArrow", "VSplit", "WaitCursor" _
        }
        Text = "Mouse Cursors Using Cursor Property"

        For i = 0 To 27
            actrl(i) = New Label()
            actrl(i).Parent = Me
            actrl(i).Text = astrCursor(i)
            actrl(i).Cursor = acursor(i)
            actrl(i).BorderStyle = BorderStyle.FixedSingle
        Next i
        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        Dim i As Integer
        For i = 0 To 27
            actrl(i).Bounds = Rectangle.FromLTRB( _
                                (i Mod 4) * ClientSize.Width \ 4, _
                                (i \ 4) * ClientSize.Height \ 7, _
                                (i Mod 4 + 1) * ClientSize.Width \ 4, _
                                (i \ 4 + 1) * ClientSize.Height \ 7)
        Next i
    End Sub
End Class
