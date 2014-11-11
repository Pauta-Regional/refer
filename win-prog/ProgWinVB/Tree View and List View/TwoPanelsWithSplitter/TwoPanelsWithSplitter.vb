'------------------------------------------------------
' TwoPanelsWithSplitter.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwoPanelsWithSplitter
    Inherits Form

    Shared Sub Main()
        Application.Run(new TwoPanelsWithSplitter())
    End Sub

    Sub New()
        Text = "Two Panels with Splitter"

        Dim pnl1 As New Panel()
        pnl1.Parent = Me
        pnl1.Dock = DockStyle.Fill
        pnl1.BackColor = Color.Lime
        AddHandler pnl1.Resize, AddressOf PanelOnResize
        AddHandler pnl1.Paint, AddressOf PanelOnPaint

        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Right

        Dim pnl2 As New Panel()
        pnl2.Parent = Me
        pnl2.Dock = DockStyle.Right
        pnl2.BackColor = Color.Red
        AddHandler pnl2.Resize, AddressOf PanelOnResize
        AddHandler pnl2.Paint, AddressOf PanelOnPaint
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
