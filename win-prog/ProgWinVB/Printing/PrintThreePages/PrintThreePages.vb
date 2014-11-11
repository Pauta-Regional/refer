'------------------------------------------------
' PrintThreePages.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class PrintThreePages
    Inherits Form

    Const iNumberPages As Integer = 3
    Private iPageNumber As Integer

    Shared Sub Main()
        Application.Run(New PrintThreePages())
    End Sub

    Sub New()
        Text = "Print Three Pages"

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

        ' Set printer resolution to "draft".
        Dim prnres As PrinterResolution
        For Each prnres In prndoc.PrinterSettings.PrinterResolutions
            If prnres.Kind = PrinterResolutionKind.Draft Then
                prndoc.DefaultPageSettings.PrinterResolution = prnres
                Exit For
            End If
        Next

        ' Set remainder of PrintDocument properties.
        prndoc.DocumentName = Text
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        AddHandler prndoc.QueryPageSettings, AddressOf OnQueryPageSettings

        ' Commence printing.
        iPageNumber = 1
        prndoc.Print()
    End Sub

    Private Sub OnQueryPageSettings(ByVal obj As Object, _
                                ByVal qpsea As QueryPageSettingsEventArgs)
        If qpsea.PageSettings.PrinterSettings.LandscapeAngle <> 0 Then
            qpsea.PageSettings.Landscape = Not qpsea.PageSettings.Landscape
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
        ppea.HasMorePages = iPageNumber < iNumberPages
        iPageNumber += 1
    End Sub
End Class
