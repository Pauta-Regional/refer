'--------------------------------------------
' JeuDeTaquin.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class JeuDeTaquin
    Inherits Form

    Const nRows As Integer = 4
    Const nCols As Integer = 4

    Private szTile As Size
    Private atile(nRows, nCols) As JeuDeTaquinTile
    Private rand As Random
    Private ptBlank As Point
    Private iTimerCountdown As Integer

    Shared Sub Main()
        Application.Run(new JeuDeTaquin())
    End Sub

    Sub New()
        Text = "Jeu de Taquin"
        FormBorderStyle = FormBorderStyle.Fixed3D
    End Sub

    Protected Overrides Sub OnLoad(ByVal ea As EventArgs)
        ' Calculate the size of the tiles and the form.
        Dim grfx As Graphics = CreateGraphics()
        szTile = New Size(CInt(2 * grfx.DpiX / 3), _
                          CInt(2 * grfx.DpiY / 3))
        ClientSize = New Size(nCols * szTile.Width, nRows * szTile.Height)
        grfx.Dispose()

        ' Create the tiles.
        Dim iRow, iCol, iNum As Integer

        For iRow = 0 To nRows - 1
            For iCol = 0 To nCols - 1
                iNum = iRow * nCols + iCol + 1
                If iNum <> nRows * nCols Then
                    Dim tile As New JeuDeTaquinTile(iNum)
                    tile.Parent = Me
                    tile.Location = New Point(iCol * szTile.Width, _
                                              iRow * szTile.Height)
                    tile.Size = szTile
                    atile(iRow, iCol) = tile
                End If
            Next iCol
        Next iRow
        ptBlank = New Point(nCols - 1, nRows - 1)
        Randomize()
    End Sub

    Protected Sub Randomize()
        rand = new Random()
        iTimerCountdown = 64 * nRows * nCols

        Dim tmr As New Timer()
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Interval = 1
        tmr.Enabled = True
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim x As Integer = ptBlank.X
        Dim y As Integer = ptBlank.Y

        Select Case rand.Next(4)
            Case 0 : x += 1
            Case 1 : x -= 1
            Case 2 : y += 1
            Case 3 : y -= 1
        End Select

        If x >= 0 AndAlso x < nCols AndAlso y >= 0 AndAlso y < nRows Then
            MoveTile(x, y)
        End If

        iTimerCountdown -= 1

        If iTimerCountdown = 0 Then
            Dim tmr As Timer = DirectCast(obj, Timer)
            tmr.Stop()
            RemoveHandler tmr.Tick, AddressOf TimerOnTick
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        If kea.KeyCode = Keys.Left AndAlso ptBlank.X < nCols - 1 Then
            MoveTile(ptBlank.X + 1, ptBlank.Y)

        ElseIf kea.KeyCode = Keys.Right AndAlso ptBlank.X > 0 Then
            MoveTile(ptBlank.X - 1, ptBlank.Y)

        ElseIf kea.KeyCode = Keys.Up AndAlso ptBlank.Y < nRows - 1 Then
            MoveTile(ptBlank.X, ptBlank.Y + 1)

        ElseIf kea.KeyCode = Keys.Down AndAlso ptBlank.Y > 0 Then
            MoveTile(ptBlank.X, ptBlank.Y - 1)
        End If
        kea.Handled = True
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        Dim x As Integer = mea.X \ szTile.Width
        Dim y As Integer = mea.Y \ szTile.Height
        Dim x2, y2 As Integer

        If x = ptBlank.X Then
            If y < ptBlank.Y Then
                For y2 = ptBlank.Y - 1 To y Step -1
                    MoveTile(x, y2)
                Next y2
            ElseIf y > ptBlank.Y Then
                For y2 = ptBlank.Y + 1 To y
                    MoveTile(x, y2)
                Next y2
            End If
        ElseIf y = ptBlank.Y Then
            If x < ptBlank.X Then
                For x2 = ptBlank.X - 1 To x Step -1
                    MoveTile(x2, y)
                Next x2
            ElseIf x > ptBlank.X Then
                For x2 = ptBlank.X + 1 To x
                    MoveTile(x2, y)
                Next x2
            End If
        End If
    End Sub

    Private Sub MoveTile(ByVal x As Integer, ByVal y As Integer)
        atile(y, x).Location = New Point(ptBlank.X * szTile.Width, _
                                         ptBlank.Y * szTile.Height)
        atile(ptBlank.Y, ptBlank.X) = atile(y, x)
        atile(y, x) = Nothing
        ptBlank = New Point(x, y)
    End Sub
End Class
