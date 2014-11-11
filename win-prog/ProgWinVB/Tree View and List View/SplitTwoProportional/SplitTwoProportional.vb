'-----------------------------------------------------
' SplitTwoProportional.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SplitTwoProportional
    Inherits Form

    Private pnl2 As Panel
    Private fProportion As Single = 0.5F

    Shared Sub Main()
        Application.Run(New SplitTwoProportional())
    End Sub

    Sub New()
        Text = "Split Two Proportional"

        Dim pnl1 As New Panel()
        pnl1.Parent = Me
        pnl1.Dock = DockStyle.Fill
        pnl1.BackColor = Color.Red
        AddHandler pnl1.Resize, AddressOf PanelOnResize
        AddHandler pnl1.Paint, AddressOf PanelOnPaint

        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left
        AddHandler split.SplitterMoving, AddressOf SplitterOnMoving

        pnl2 = New Panel()
        pnl2.Parent = Me
        pnl2.Dock = DockStyle.Left
        pnl2.BackColor = Color.Lime
        AddHandler pnl2.Resize, AddressOf PanelOnResize
        AddHandler pnl2.Paint, AddressOf PanelOnPaint

        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)
        pnl2.Width = CInt(fProportion * ClientSize.Width)
    End Sub

    Private Sub SplitterOnMoving(ByVal obj As Object, _
                                 ByVal sea As SplitterEventArgs)
        fProportion = CSng(sea.SplitX) / ClientSize.Width
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
