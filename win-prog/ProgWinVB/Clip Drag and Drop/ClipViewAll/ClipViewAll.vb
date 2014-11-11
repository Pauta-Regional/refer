'--------------------------------------------
' ClipViewAll.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Class ClipViewAll
    Inherits Form

    Private pnlDisplay, pnlButtons As Panel
    Private radioChecked As RadioButton
    Private astrFormatsSave(0) As String

    Shared Sub Main()
        Application.Run(New ClipViewAll())
    End Sub

    Sub New()
        Text = "Clipboard Viewer (All Formats)"

        ' Create variable-width panel for clipboard display.
        pnlDisplay = New Panel()
        pnlDisplay.Parent = Me
        pnlDisplay.Dock = DockStyle.Fill
        AddHandler pnlDisplay.Paint, AddressOf PanelOnPaint
        pnlDisplay.BorderStyle = BorderStyle.Fixed3D

        ' Create splitter.
        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left

        ' Create panel for radio buttons.
        pnlButtons = New Panel()
        pnlButtons.Parent = Me
        pnlButtons.Dock = DockStyle.Left
        pnlButtons.AutoScroll = True
        pnlButtons.Width = Width \ 2

        ' Set timer for 1 second.
        Dim tmr As New Timer()
        tmr.Interval = 1000
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Enabled = True
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        Dim astrFormats() As String = data.GetFormats()
        Dim bUpdate As Boolean = False
        Dim i As Integer

        ' Determine whether clipboard formats have changed.
        If astrFormats.Length <> astrFormatsSave.Length Then
            bUpdate = True
        Else
            For i = 0 To astrFormats.GetUpperBound(0)
                If astrFormats(i) <> astrFormatsSave(i) Then
                    bUpdate = True
                    Exit For
                End If
            Next i
        End If

        ' Invalidate display regardless.
        pnlDisplay.Invalidate()

        ' Don't update buttons if formats haven't changed.
        If Not bUpdate Then
            Return
        End If

        ' Formats have changed, so re-create radio buttons.
        astrFormatsSave = astrFormats
        pnlButtons.Controls.Clear()

        Dim grfx As Graphics = CreateGraphics()
        Dim eh As EventHandler = AddressOf RadioButtonOnClick
        Dim cxText As Integer = AutoScaleBaseSize.Width
        Dim cyText As Integer = AutoScaleBaseSize.Height

        For i = 0 To astrFormats.GetUpperBound(0)
            Dim radio As New RadioButton()
            radio.Parent = pnlButtons
            radio.Text = astrFormats(i)

            If Not data.GetDataPresent(astrFormats(i), False) Then
                radio.Text &= "*"
            End If

            Try
                Dim objClip As Object = data.GetData(astrFormats(i))
                radio.Text &= " (" & objClip.GetType().ToString() & ")"
            Catch
                radio.Text &= " (Exception on GetData or GetType!)"
            End Try

            radio.Tag = astrFormats(i)
            radio.Location = New Point(cxText, i * 3 * cyText \ 2)
            radio.Size = New Size((radio.Text.Length + 20) * cxText, _
                                  3 * cyText \ 2)
            AddHandler radio.Click, eh
        Next i
        grfx.Dispose()
        radioChecked = Nothing
    End Sub

    Private Sub RadioButtonOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        radioChecked = DirectCast(obj, RadioButton)
        pnlDisplay.Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(pnl.ForeColor)

        If radioChecked Is Nothing Then Return

        Dim data As IDataObject = Clipboard.GetDataObject()
        Dim objClip As Object = _
                        data.GetData(DirectCast(radioChecked.Tag, String))

        If objClip Is Nothing Then
            Return
        ElseIf objClip.GetType() Is GetType(String) Then
            grfx.DrawString(DirectCast(objClip, String), Font, br, _
                            RectangleF.op_Implicit(pnl.ClientRectangle))

        ElseIf objClip.GetType() Is GetType(String()) Then
            Dim str As String = _
                            String.Join(vbLf, DirectCast(objClip, String()))
            grfx.DrawString(str, Font, br, _
                            RectangleF.op_Implicit(pnl.ClientRectangle))

        ElseIf objClip.GetType() Is GetType(Bitmap) OrElse _
                objClip.GetType() Is GetType(Metafile) OrElse _
                objClip.GetType() Is GetType(Image) Then
            grfx.DrawImage(DirectCast(objClip, Image), 0, 0)

        ElseIf objClip.GetType() Is GetType(MemoryStream) Then
            Dim strm As Stream = DirectCast(objClip, Stream)
            Dim abyBuffer(16) As Byte
            Dim lAddress As Long = 0
            Dim fnt As New Font(FontFamily.GenericMonospace, _
                                Font.SizeInPoints)
            Dim y As Single = 0
            Dim iCount As Integer = strm.Read(abyBuffer, 0, 16)

            While iCount > 0
                Dim str As String = _
                            HexDump.ComposeLine(lAddress, abyBuffer, iCount)
                grfx.DrawString(str, fnt, br, 0, y)
                lAddress += 16
                y += fnt.GetHeight(grfx)

                If y > pnl.Bottom Then Exit While

                iCount = strm.Read(abyBuffer, 0, 16)
            End While
        End If
    End Sub
End Class
