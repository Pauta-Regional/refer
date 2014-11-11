'------------------------------------------
' Transform.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging     ' For bitmap 
Imports System.Windows.Forms

Class Transform
    Inherits Form

    Private matx As New Matrix()

    Shared Sub Main()
        Application.Run(New Transform())
    End Sub

    Sub New()
        Text = "Transform"
        ResizeRedraw = True
        BackColor = Color.White
        Size = Size.op_Addition(Size, Size)

        ' Create modal dialog box.
        Dim dlg As New MatrixElements()
        dlg.Owner = Me
        dlg.Matrix = matx
        AddHandler dlg.Changed, AddressOf MatrixDialogOnChanged
        dlg.Show()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        DrawAxes(grfx)
        grfx.Transform = matx
        DrawHouse(grfx)
    End Sub

    Private Sub DrawAxes(ByVal grfx As Graphics)
        Dim br As Brush = Brushes.Black
        Dim pn As Pen = Pens.Black
        Dim strfmt As New StringFormat()
        Dim i As Integer

        ' Horizontal axis
        strfmt.Alignment = StringAlignment.Center
        For i = 1 To 10
            grfx.DrawLine(pn, 100 * i, 0, 100 * i, 10)
            grfx.DrawString((i * 100).ToString(), Font, br, _
                            100 * i, 10, strfmt)
            grfx.DrawLine(pn, 100 * i, 10 + Font.Height, _
                              100 * i, ClientSize.Height)
        Next i

        ' Vertical axis
        strfmt.Alignment = StringAlignment.Near
        strfmt.LineAlignment = StringAlignment.Center

        For i = 1 To 10
            grfx.DrawLine(pn, 0, 100 * i, 10, 100 * i)
            grfx.DrawString((i * 100).ToString(), Font, br, _
                            10, 100 * i, strfmt)
            Dim cxText As Single = grfx.MeasureString( _
                                        (i * 100).ToString(), Font).Width
            grfx.DrawLine(pn, 10 + cxText, 100 * i, _
                              ClientSize.Width, 100 * i)
        Next i
    End Sub

    Private Sub DrawHouse(ByVal grfx As Graphics)
        Dim rectFacade As New Rectangle(0, 40, 100, 60)
        Dim rectDoor As New Rectangle(10, 50, 25, 50)
        Dim rectWindows() As Rectangle = {New Rectangle(50, 50, 10, 10), _
                                          New Rectangle(60, 50, 10, 10), _
                                          New Rectangle(70, 50, 10, 10), _
                                          New Rectangle(50, 60, 10, 10), _
                                          New Rectangle(60, 60, 10, 10), _
                                          New Rectangle(70, 60, 10, 10), _
                                          New Rectangle(15, 60, 5, 7), _
                                          New Rectangle(20, 60, 5, 7), _
                                          New Rectangle(25, 60, 5, 7)}
        Dim rectChimney As New Rectangle(80, 5, 10, 35)
        Dim ptRoof() As Point = {New Point(50, 0), _
                                 New Point(0, 40), _
                                 New Point(100, 40)}

        ' Create bitmap and br for chimney.
        Dim bm As New Bitmap(8, 6)
        Dim bits() As Byte = {0, 0, 0, 0, 0, 0, 0, 0, _
                              1, 1, 1, 0, 1, 1, 1, 0, _
                              1, 1, 1, 0, 1, 1, 1, 0, _
                              0, 0, 0, 0, 0, 0, 0, 0, _
                              1, 0, 1, 1, 1, 0, 1, 1, _
                              1, 0, 1, 1, 1, 0, 1, 1}
        Dim i As Integer

        For i = 0 To 47
            If (bits(i) = 1) Then
                bm.SetPixel(i Mod 8, i \ 8, Color.DarkGray)
            Else
                bm.SetPixel(i Mod 8, i \ 8, Color.LightGray)
            End If
        Next i
        Dim br As New TextureBrush(bm)

        ' Draw entire house.
        grfx.FillRectangle(Brushes.LightGray, rectFacade)
        grfx.DrawRectangle(Pens.Black, rectFacade)
        grfx.FillRectangle(Brushes.DarkGray, rectDoor)
        grfx.DrawRectangle(Pens.Black, rectDoor)
        grfx.FillRectangles(Brushes.White, rectWindows)
        grfx.DrawRectangles(Pens.Black, rectWindows)
        grfx.FillRectangle(br, rectChimney)
        grfx.DrawRectangle(Pens.Black, rectChimney)
        grfx.FillPolygon(Brushes.DarkGray, ptRoof)
        grfx.DrawPolygon(Pens.Black, ptRoof)
    End Sub

    Private Sub MatrixDialogOnChanged(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        Dim dlg As MatrixElements = DirectCast(obj, MatrixElements)
        matx = dlg.Matrix
        Invalidate()
    End Sub
End Class
