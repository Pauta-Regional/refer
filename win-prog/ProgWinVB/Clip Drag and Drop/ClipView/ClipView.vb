'-----------------------------------------
' ClipView.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Class ClipView
    Inherits Form

    Private astrFormats() As String = _
        {DataFormats.Bitmap, DataFormats.CommaSeparatedValue, _
        DataFormats.Dib, DataFormats.Dif, DataFormats.EnhancedMetafile, _
        DataFormats.FileDrop, DataFormats.Html, DataFormats.Locale, _
        DataFormats.MetafilePict, DataFormats.OemText, _
        DataFormats.Palette, DataFormats.PenData, DataFormats.Riff, _
        DataFormats.Rtf, DataFormats.Serializable, _
        DataFormats.StringFormat, DataFormats.SymbolicLink, _
        DataFormats.Text, DataFormats.Tiff, DataFormats.UnicodeText, _
        DataFormats.WaveAudio}

    Private pnlDisplay As Panel
    Private aradio() As RadioButton
    Private radioChecked As RadioButton

    Shared Sub Main()
        Application.Run(New ClipView())
    End Sub

    Sub New()
        Text = "Clipboard Viewer"

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
        Dim pnl As New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Left
        pnl.Width = 200

        ' Create radio buttons.
        ReDim aradio(astrFormats.Length - 1)
        Dim eh As EventHandler = AddressOf RadioButtonOnClick
        Dim i As Integer
        For i = 0 To astrFormats.GetUpperBound(0)
            aradio(i) = New RadioButton()
            aradio(i).Parent = pnl
            aradio(i).Location = New Point(4, 12 * i)
            aradio(i).Size = New Size(300, 12)
            AddHandler aradio(i).Click, eh
            aradio(i).Tag = astrFormats(i)
        Next i

        ' Set autoscale base size.
        AutoScaleBaseSize = New Size(4, 8)

        ' Set timer for 1 second.
        Dim tmr As New Timer()
        tmr.Interval = 1000
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Enabled = True
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        Dim i As Integer

        For i = 0 To astrFormats.Length - 1
            aradio(i).Text = astrFormats(i)
            aradio(i).Enabled = data.GetDataPresent(astrFormats(i))
            If aradio(i).Enabled Then
                If Not data.GetDataPresent(astrFormats(i), False) Then
                    aradio(i).Text &= "*"
                End If
                Dim objClip As Object = data.GetData(astrFormats(i))
                Try
                    aradio(i).Text &= " (" & _
                                      objClip.GetType().ToString() & ")"
                Catch
                    aradio(i).Text &= " (Exception on GetType!)"
                End Try
            End If
        Next i
        pnlDisplay.Invalidate()
    End Sub

    Private Sub RadioButtonOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        radioChecked = DirectCast(obj, RadioButton)
        pnlDisplay.Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim panel As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(panel.ForeColor)

        If radioChecked Is Nothing OrElse Not radioChecked.Enabled Then
            Return
        End If

        Dim data As IDataObject = Clipboard.GetDataObject()
        Dim objClip As Object = _
                        data.GetData(DirectCast(radioChecked.Tag, String))

        If objClip Is Nothing Then
            Return
        ElseIf objClip.GetType() Is GetType(String) Then
            grfx.DrawString(DirectCast(objClip, String), Font, br, _
                            RectangleF.op_Implicit(panel.ClientRectangle))

        ElseIf objClip.GetType() Is GetType(String()) Then
            Dim str As String = String.Join(vbLf, _
                                            DirectCast(objClip, String()))
            grfx.DrawString(str, Font, br, _
                            RectangleF.op_Implicit(panel.ClientRectangle))

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

                If y > panel.Bottom Then Exit While

                iCount = strm.Read(abyBuffer, 0, 16)
            End While
        End If
    End Sub
End Class
