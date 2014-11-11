'-------------------------------------------------
' SplitThreeFrames.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SplitThreeFrames
    Inherits Form

    Shared Sub Main()
        Application.Run(new SplitThreeFrames())
    End Sub

    Sub New()
        Text = "Split Three Frames"

        Dim pnl As New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Fill

        Dim split1 As New Splitter()
        split1.Parent = Me
        split1.Dock = DockStyle.Left

        Dim pnl1 As New Panel()
        pnl1.Parent = Me
        pnl1.Dock = DockStyle.Left
        pnl1.BackColor = Color.Lime
        AddHandler pnl1.Resize, AddressOf PanelOnResize
        AddHandler pnl1.Paint, AddressOf PanelOnPaint

        Dim pnl2 As New Panel()
        pnl2.Parent = pnl
        pnl2.Dock = DockStyle.Fill
        pnl2.BackColor = Color.Blue
        AddHandler pnl2.Resize, AddressOf PanelOnResize
        AddHandler pnl2.Paint, AddressOf PanelOnPaint

        Dim split2 As New Splitter()
        split2.Parent = pnl
        split2.Dock = DockStyle.Top

        Dim pnl3 As New Panel()
        pnl3.Parent = pnl
        pnl3.Dock = DockStyle.Top
        pnl3.BackColor = Color.Tan
        AddHandler pnl3.Resize, AddressOf PanelOnResize
        AddHandler pnl3.Paint, AddressOf PanelOnPaint

        pnl1.Width = ClientSize.Width \ 3
        pnl3.Height = ClientSize.Height \ 3
    End Sub

    Private Sub PanelOnResize(ByVal obj As Object, ByVal ea As EventArgs)
        DirectCast(obj, Panel).Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics

        grfx.DrawEllipse(Pens.Black, 0, 0, _
                         pnl.Width - 1, pnl.Height - 1)
    End Sub
End Class
