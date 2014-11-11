'-------------------------------------------------
' SplitThreeAcross.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SplitThreeAcross
    Inherits Form

    Shared Sub Main()
        Application.Run(new SplitThreeAcross())
    End Sub

    Sub New()
        Text = "Split Three Across"

        Dim pnl1 As New Panel()
        pnl1.Parent = Me
        pnl1.Dock = DockStyle.Fill
        pnl1.BackColor = Color.Cyan
        AddHandler pnl1.Resize, AddressOf PanelOnResize
        AddHandler pnl1.Paint, AddressOf PanelOnPaint

        Dim split1 As New Splitter()
        split1.Parent = Me
        split1.Dock = DockStyle.Left

        Dim pnl2 As New Panel()
        pnl2.Parent = Me
        pnl2.Dock = DockStyle.Left
        pnl2.BackColor = Color.Lime
        AddHandler pnl2.Resize, AddressOf PanelOnResize
        AddHandler pnl2.Paint, AddressOf PanelOnPaint

        Dim split2 As New Splitter()
        split2.Parent = Me
        split2.Dock = DockStyle.Right

        Dim pnl3 As New Panel()
        pnl3.Parent = Me
        pnl3.Dock = DockStyle.Right
        pnl3.BackColor = Color.Red
        AddHandler pnl3.Resize, AddressOf PanelOnResize
        AddHandler pnl3.Paint, AddressOf PanelOnPaint

        pnl1.Width = ClientSize.Width \ 3
        pnl2.Width = ClientSize.Width \ 3
        pnl3.Width = ClientSize.Width \ 3
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
