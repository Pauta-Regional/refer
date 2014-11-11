'--------------------------------------------------
' SimplePrintDialog.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class SimplePrintDialog
    Inherits Form

    Const iNumberPages As Integer = 3
    Private iPagesToPrint, iPageNumber As Integer

    Shared Sub Main()
        Application.Run(New SimplePrintDialog())
    End Sub

    Sub New()
        Text = "Simple PrintDialog"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&File")
        Menu.MenuItems(0).MenuItems.Add("&Print...", _
                                        AddressOf MenuFilePrintOnClick)
    End Sub

    Private Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        ' Create the PrintDocument and PrintDialog.
        Dim prndoc As New PrintDocument()
        Dim prndlg As New PrintDialog()
        prndlg.Document = prndoc

        ' Allow a page range.
        prndlg.AllowSomePages = True
        prndlg.PrinterSettings.MinimumPage = 1
        prndlg.PrinterSettings.MaximumPage = iNumberPages
        prndlg.PrinterSettings.FromPage = 1
        prndlg.PrinterSettings.ToPage = iNumberPages

        ' If the dialog box returns OK, print.
        If prndlg.ShowDialog() = DialogResult.OK Then
            prndoc.DocumentName = Text
            AddHandler prndoc.PrintPage, AddressOf OnPrintPage

            ' Determine which pages to print.
            Select Case prndlg.PrinterSettings.PrintRange
                Case PrintRange.AllPages
                    iPagesToPrint = iNumberPages
                    iPageNumber = 1

                Case PrintRange.SomePages
                    iPagesToPrint = 1 + prndlg.PrinterSettings.ToPage - _
                                    prndlg.PrinterSettings.FromPage()
                    iPageNumber = prndlg.PrinterSettings.FromPage
            End Select
            prndoc.Print()
        End If
    End Sub

    Private Sub OnPrintPage(ByVal obj As Object, _
                            ByVal ppea As PrintPageEventArgs)
        Dim grfx As Graphics = ppea.Graphics
        Dim fnt As New Font("Times New Roman", 360)
        Dim str As String = iPageNumber.ToString()
        Dim szf As SizeF = grfx.MeasureString(str, fnt)

        grfx.DrawString(str, fnt, Brushes.Black, _
                        (grfx.VisibleClipBounds.Width - szf.Width) / 2, _
                        (grfx.VisibleClipBounds.Height - szf.Height) / 2)
        iPageNumber += 1
        iPagesToPrint -= 1
        ppea.HasMorePages = iPagesToPrint > 0
    End Sub
End Class
