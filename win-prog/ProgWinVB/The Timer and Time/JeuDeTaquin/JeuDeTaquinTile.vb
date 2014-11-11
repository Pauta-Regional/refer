'------------------------------------------------
' JeuDeTaquinTile.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class JeuDeTaquinTile
    Inherits UserControl

    Private iNum As Integer

    Sub New(ByVal iNum As Integer)
        Me.iNum = iNum
        Enabled = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.Clear(SystemColors.Control)

        Dim cx As Integer = Size.Width
        Dim cy As Integer = Size.Height
        Dim wx As Integer = SystemInformation.FrameBorderSize.Width
        Dim wy As Integer = SystemInformation.FrameBorderSize.Height

        grfx.FillPolygon(SystemBrushes.ControlLightLight, _
            New Point() {New Point(0, cy), New Point(0, 0), _
                         New Point(cx, 0), New Point(cx - wx, wy), _
                         New Point(wx, wy), New Point(wx, cy - wy)})

        grfx.FillPolygon(SystemBrushes.ControlDark, _
            New Point() {New Point(cx, 0), New Point(cx, cy), _
                         New Point(0, cy), New Point(wx, cy - wy), _
                         New Point(cx - wx, cy - wy), _
                         New Point(cx - wx, wy)})

        Dim fnt As New Font("Arial", 24)
        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        grfx.DrawString(iNum.ToString(), fnt, SystemBrushes.ControlText, _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
