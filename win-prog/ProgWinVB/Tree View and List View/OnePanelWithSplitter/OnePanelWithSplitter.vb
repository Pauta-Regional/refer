'-----------------------------------------------------
' OnePanelWithSplitter.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class OnePanelWithSplitter
    Inherits Form

    Shared Sub Main()
        Application.Run(New OnePanelWithSplitter())
    End Sub

    Sub New()
        Text = "One Panel with Splitter"

        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left

        Dim pnl As New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Left
        pnl.BackColor = Color.Lime
        AddHandler pnl.Resize, AddressOf PanelOnResize
        AddHandler pnl.Paint, AddressOf PanelOnPaint
    End Sub

    Private Sub PanelOnResize(ByVal obj As Object, ByVal ea As EventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        pnl.Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics

        grfx.DrawEllipse(Pens.Black, 0, 0, _
                         pnl.Width - 1, pnl.Height - 1)
    End Sub
End Class
