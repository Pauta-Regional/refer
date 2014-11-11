'-------------------------------------------------
' PrintWithMargins.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class PrintWithMargins
    Inherits Form

    Shared Sub Main()
        Application.Run(New PrintWithMargins())
    End Sub

    Sub New()
        Text = "Print with Margins"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&File")
        Menu.MenuItems(0).MenuItems.Add("&Print...", _
                                        AddressOf MenuFilePrintOnClick)
    End Sub

    Private Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        ' Create PrintDocument.
        Dim prndoc As New PrintDocument()

        ' Create dialog box and set PrinterName property.
        Dim dlg As New PrinterSelectionDialog()
        dlg.PrinterName = prndoc.PrinterSettings.PrinterName

        ' Show dialog box and bail out if not OK.
        If dlg.ShowDialog() <> DialogResult.OK Then Return

        ' Set PrintDocument to selected printer.
        prndoc.PrinterSettings.PrinterName = dlg.PrinterName

        ' Set remainder of PrintDocument properties and commence.
        prndoc.DocumentName = Text
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        prndoc.Print()
    End Sub

    Private Sub OnPrintPage(ByVal obj As Object, _
                            ByVal ppea As PrintPageEventArgs)
        Dim grfx As Graphics = ppea.Graphics
        Dim rectf As New RectangleF( _
            ppea.MarginBounds.Left - _
            (ppea.PageBounds.Width - grfx.VisibleClipBounds.Width) / 2, _
            ppea.MarginBounds.Top - _
            (ppea.PageBounds.Height - grfx.VisibleClipBounds.Height) / 2, _
            ppea.MarginBounds.Width, ppea.MarginBounds.Height)

        grfx.DrawRectangle(Pens.Black, rectf.X, rectf.Y, _
                                       rectf.Width, rectf.Height)
        grfx.DrawLine(Pens.Black, rectf.Left, rectf.Top, _
                                  rectf.Right, rectf.Bottom)
        grfx.DrawLine(Pens.Black, rectf.Right, rectf.Top, _
                                  rectf.Left, rectf.Bottom)
    End Sub
End Class
