'----------------------------------------------
' ToggleButtons.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ToggleButtons
    Inherits Form

    Protected pnl As Panel
    Protected tbar As ToolBar
    Protected strText As String = "Toggle"
    Protected clrText As Color = SystemColors.WindowText
    Private fntstyle As FontStyle = FontStyle.Regular

    Shared Sub Main()
        Application.Run(New ToggleButtons())
    End Sub

    Sub New()
        Text = "Toggle Buttons"

        ' Create panel to fill interior.
        pnl = New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Fill
        pnl.BackColor = SystemColors.Window
        pnl.ForeColor = SystemColors.WindowText
        AddHandler pnl.Resize, AddressOf PanelOnResize
        AddHandler pnl.Paint, AddressOf PanelOnPaint

        ' Create ImageList.
        Dim bm As New Bitmap(Me.GetType(), "FontStyleButtons.bmp")
        Dim imglst As New ImageList()
        imglst.ImageSize = New Size(bm.Width \ 4, bm.Height)
        imglst.Images.AddStrip(bm)
        imglst.TransparentColor = Color.White

        ' Create ToolBar.
        tbar = New ToolBar()
        tbar.ImageList = imglst
        tbar.Parent = Me
        tbar.ShowToolTips = True
        AddHandler tbar.ButtonClick, AddressOf ToolBarOnClick

        ' Create ToolBarButtons.
        Dim afs() As FontStyle = {FontStyle.Bold, FontStyle.Italic, _
                                  FontStyle.Underline, FontStyle.Strikeout}
        Dim i As Integer
        For i = 0 To 3
            Dim tbarbtn As New ToolBarButton()
            tbarbtn.ImageIndex = i
            tbarbtn.Style = ToolBarButtonStyle.ToggleButton
            tbarbtn.ToolTipText = afs(i).ToString()
            tbarbtn.Tag = afs(i)
            tbar.Buttons.Add(tbarbtn)
        Next i
    End Sub

    Private Sub ToolBarOnClick(ByVal obj As Object, _
                               ByVal tbbcea As ToolBarButtonClickEventArgs)
        Dim tbb As ToolBarButton = tbbcea.Button

        ' If the Tag isn't a FontStyle, don't do anything.
        If tbb.Tag Is Nothing Then Return
        If Not tbb.Tag.GetType() Is GetType(FontStyle) Then Return

        ' Set or clear the bit in the fntstyle field.
        If tbb.Pushed Then
            fntstyle = fntstyle Or CType(tbb.Tag, FontStyle)
        Else
            fntstyle = fntstyle And Not CType(tbb.Tag, FontStyle)
        End If
        pnl.Invalidate()
    End Sub

    Private Sub PanelOnResize(ByVal obj As Object, ByVal ea As EventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        pnl.Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics
        Dim fnt As New Font("Times New Roman", 72, fntstyle)
        Dim szf As SizeF = grfx.MeasureString(strText, fnt)

        grfx.DrawString(strText, fnt, New SolidBrush(clrText), _
                        (pnl.Width - szf.Width) / 2, _
                        (pnl.Height - szf.Height) / 2)
    End Sub
End Class
