'---------------------------------------------------------
' NotepadCloneWithPrinting.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Windows.Forms

Class NotepadCloneWithPrinting
    Inherits NotepadCloneWithFormat

    Private prndoc As New PrintDocument()
    Private setdlg As New PageSetupDialog()
    Private predlg As New PrintPreviewDialog()
    Private prndlg As New PrintDialog()
    Private strPrintText As String
    Private iStartPage, iNumPages, iPageNumber As Integer

    Shared Shadows Sub Main()
        Application.Run(New NotepadCloneWithPrinting())
    End Sub

    Sub New()
        strProgName = "Notepad Clone with Printing"
        MakeCaption()

        ' Initialize printer-related objects and dialogs
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        setdlg.Document = prndoc
        predlg.Document = prndoc
        prndlg.Document = prndoc
        prndlg.AllowSomePages = True
        prndlg.PrinterSettings.FromPage = 1
        prndlg.PrinterSettings.ToPage = prndlg.PrinterSettings.MaximumPage
    End Sub

    Protected Overrides Sub MenuFileSetupOnClick(ByVal obj As Object, _
                                                 ByVal ea As EventArgs)
        setdlg.ShowDialog()
    End Sub

    Protected Overrides Sub MenuFilePreviewOnClick(ByVal obj As Object, _
                                                   ByVal ea As EventArgs)
        prndoc.DocumentName = Text    ' Just in case it's printed
        strPrintText = txtbox.Text
        iStartPage = 1
        iNumPages = prndlg.PrinterSettings.MaximumPage
        iPageNumber = 1
        predlg.ShowDialog()
    End Sub

    Protected Overrides Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                                 ByVal ea As EventArgs)
        prndlg.AllowSelection = txtbox.SelectionLength > 0
        If prndlg.ShowDialog() = DialogResult.OK Then
            prndoc.DocumentName = Text

            ' Initialize some important fields.
            Select Case prndlg.PrinterSettings.PrintRange
                Case PrintRange.AllPages
                    strPrintText = txtbox.Text
                    iStartPage = 1
                    iNumPages = prndlg.PrinterSettings.MaximumPage

                Case PrintRange.Selection
                    strPrintText = txtbox.SelectedText
                    iStartPage = 1
                    iNumPages = prndlg.PrinterSettings.MaximumPage

                Case PrintRange.SomePages
                    strPrintText = txtbox.Text
                    iStartPage = prndlg.PrinterSettings.FromPage
                    iNumPages = prndlg.PrinterSettings.ToPage - _
                                                        iStartPage + 1

            End Select

            ' And commence printing.
            iPageNumber = 1
            prndoc.Print()
        End If
    End Sub

    Private Sub OnPrintPage(ByVal obj As Object, _
                            ByVal ppea As PrintPageEventArgs)
        Dim grfx As Graphics = ppea.Graphics
        Dim fnt As Font = txtbox.Font
        Dim cyFont As Single = fnt.GetHeight(grfx)
        Dim strfmt As New StringFormat()
        Dim rectfFull, rectfText As RectangleF
        Dim iChars, iLines As Integer

        ' Calculate RectangleF for header and footer.
        If grfx.VisibleClipBounds.X < 0 Then
            rectfFull = RectangleF.op_Implicit(ppea.MarginBounds)
        Else
            rectfFull = New RectangleF( _
                ppea.MarginBounds.Left - (ppea.PageBounds.Width - _
                        grfx.VisibleClipBounds.Width) / 2, _
                ppea.MarginBounds.Top - (ppea.PageBounds.Height - _
                        grfx.VisibleClipBounds.Height) / 2, _
                ppea.MarginBounds.Width, ppea.MarginBounds.Height)
        End If

        ' Calculate RectangleF for text.
        rectfText = RectangleF.Inflate(rectfFull, 0, -2 * cyFont)
        Dim iDisplayLines As Integer = _
                                CInt(Math.Floor(rectfText.Height / cyFont))
        rectfText.Height = iDisplayLines * cyFont

        ' Set up StringFormat Object for rectanglar display of text.
        If txtbox.WordWrap Then
            strfmt.Trimming = StringTrimming.Word
        Else
            strfmt.Trimming = StringTrimming.EllipsisCharacter
            strfmt.FormatFlags = strfmt.FormatFlags Or _
                                                StringFormatFlags.NoWrap
        End If

        ' For "some pages" get to the first page.
        While iPageNumber < iStartPage AndAlso strPrintText.Length > 0
            If txtbox.WordWrap Then
                grfx.MeasureString(strPrintText, fnt, rectfText.Size, _
                                   strfmt, iChars, iLines)
            Else
                iChars = CharsInLines(strPrintText, iDisplayLines)
            End If
            strPrintText = strPrintText.Substring(iChars)
            iPageNumber += 1
        End While

        ' If we've prematurely run out of text, cancel the print job.
        If strPrintText.Length = 0 Then
            ppea.Cancel = True
            Return
        End If

        ' Display text for this page
        grfx.DrawString(strPrintText, fnt, Brushes.Black, rectfText, strfmt)

        ' Get text for next page.
        If txtbox.WordWrap Then
            grfx.MeasureString(strPrintText, fnt, rectfText.Size, _
                               strfmt, iChars, iLines)
        Else
            iChars = CharsInLines(strPrintText, iDisplayLines)
        End If
        strPrintText = strPrintText.Substring(iChars)

        ' Reset StringFormat to display header and footer.
        strfmt = New StringFormat()

        ' Display filename at top.
        strfmt.Alignment = StringAlignment.Center
        grfx.DrawString(FileTitle(), fnt, Brushes.Black, rectfFull, strfmt)

        ' Display page number at bottom.
        strfmt.LineAlignment = StringAlignment.Far
        grfx.DrawString("Page " & iPageNumber, fnt, Brushes.Black, _
                        rectfFull, strfmt)

        ' Decide whether to print another page.
        iPageNumber += 1
        ppea.HasMorePages = (strPrintText.Length > 0) AndAlso _
                            (iPageNumber < iStartPage + iNumPages)

        ' Reinitialize variables for printing from preview form.
        If Not ppea.HasMorePages Then
            strPrintText = txtbox.Text
            iStartPage = 1
            iNumPages = prndlg.PrinterSettings.MaximumPage
            iPageNumber = 1
        End If
    End Sub

    Private Function CharsInLines(ByVal strPrintText As String, _
                                  ByVal iNumLines As Integer) As Integer
        Dim index As Integer = 0
        Dim i As Integer
        For i = 0 To iNumLines - 1
            index = 1 + strPrintText.IndexOf(vbLf, index)
            If index = 0 Then
                Return strPrintText.Length
            End If
        Next i
        Return index
    End Function
End Class
