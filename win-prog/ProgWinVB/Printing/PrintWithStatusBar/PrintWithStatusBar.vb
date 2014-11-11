'---------------------------------------------------
' PrintWithStatusBar.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class PrintWithStatusBar
    Inherits Form

    Private sbar As StatusBar
    Private sbp As StatusBarPanel
    Const iNumberPages As Integer = 3
    Private iPageNumber As Integer

    Shared Sub Main()
        Application.Run(New PrintWithStatusBar())
    End Sub

    Sub New()
        Text = "Print with Status Bar"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&File")
        Menu.MenuItems(0).MenuItems.Add("&Print", _
                                        AddressOf MenuFilePrintOnClick)
        sbar = New StatusBar()
        sbar.Parent = Me
        sbar.ShowPanels = True
        sbp = New StatusBarPanel()
        sbp.Text = "Ready"
        sbp.Width = Width \ 2
        sbar.Panels.Add(sbp)
    End Sub

    Private Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim prndoc As New PrintDocument()
        prndoc.DocumentName = Text
        prndoc.PrintController = New StatusBarPrintController(sbp)
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        iPageNumber = 1
        prndoc.Print()
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
        System.Threading.Thread.Sleep(1000)
        ppea.HasMorePages = iPageNumber < iNumberPages
        iPageNumber += 1
    End Sub
End Class
