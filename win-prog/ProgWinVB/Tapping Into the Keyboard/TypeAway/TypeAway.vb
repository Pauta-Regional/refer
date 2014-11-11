'-----------------------------------------
' TypeAway.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Class TypeAway
    Inherits Form

    Shared Sub Main()
        Application.Run(new TypeAway())
    End Sub

    Protected crt As caret
    Protected strText As String = ""
    Protected iInsert As Integer = 0

    Sub New()
        Text = "Type Away"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        crt = New Caret(Me)
        crt.Size = New Size(2, Font.Height)
        crt.Position = New Point(0, 0)
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal kpea As KeyPressEventArgs)
        Dim grfx As Graphics = CreateGraphics()
        crt.Hide()
        grfx.FillRectangle(New SolidBrush(BackColor), _
                    New RectangleF(PointF.Empty, _
                    grfx.MeasureString(strText, Font, _
                    PointF.Empty, StringFormat.GenericTypographic)))

        Select Case kpea.KeyChar
            Case Chr(8)
                If (iInsert > 0) Then
                    strText = strText.Substring(0, iInsert - 1) & _
                              strText.Substring(iInsert)
                    iInsert -= 1
                End If

            Case Chr(10), Chr(13)  ' Ignore these keys

            Case Else
                If (iInsert = strText.Length) Then
                    strText &= kpea.KeyChar
                Else
                    strText = strText.Substring(0, iInsert) & _
                              kpea.KeyChar & _
                              strText.Substring(iInsert)
                End If
                iInsert += 1
        End Select

        grfx.TextRenderingHint = TextRenderingHint.AntiAlias
        grfx.DrawString(strText, Font, New SolidBrush(ForeColor), _
                        0, 0, StringFormat.GenericTypographic)
        grfx.Dispose()

        PositionCaret()
        crt.Show()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        Select Case kea.KeyData
            Case Keys.Left
                If (iInsert > 0) Then iInsert -= 1

            Case Keys.Right
                If (iInsert < strText.Length) Then iInsert += 1

            Case Keys.Home
                iInsert = 0

            Case Keys.End
                iInsert = strText.Length

            Case Keys.Delete
                If (iInsert < strText.Length) Then
                    iInsert += 1
                    OnKeyPress(New KeyPressEventArgs(Chr(8)))
                End If

            Case Else
                Return
        End Select

        PositionCaret()
    End Sub

    Protected Sub PositionCaret()
        Dim grfx As Graphics = CreateGraphics()
        Dim str As String = strText.Substring(0, iInsert)
        Dim strfmt As StringFormat = StringFormat.GenericTypographic
        strfmt.FormatFlags = strfmt.FormatFlags Or _
                             StringFormatFlags.MeasureTrailingSpaces
        Dim szf As SizeF = grfx.MeasureString(str, Font, _
                                              PointF.Empty, strfmt)
        crt.Position = New Point(CInt(szf.Width), 0)
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.TextRenderingHint = TextRenderingHint.AntiAlias
        grfx.DrawString(strText, Font, New SolidBrush(ForeColor), _
                        0, 0, StringFormat.GenericTypographic)
    End Sub
End Class
