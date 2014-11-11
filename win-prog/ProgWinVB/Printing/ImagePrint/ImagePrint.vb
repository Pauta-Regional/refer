'-------------------------------------------
' ImagePrint.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class ImagePrint
    Inherits ImageIO

    Private prndoc As PrintDocument = New PrintDocument()
    Private setdlg As PageSetupDialog = New PageSetupDialog()
    Private prndlg As PrintDialog = New PrintDialog()
    Private miFileSet, miFilePrint, miFileProps As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New ImagePrint())
    End Sub

    Sub New()
        strProgName = "Image Print"
        Text = strProgName

        ' Initialize PrintDocument and common dialog boxes.
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        setdlg.Document = prndoc
        prndlg.Document = prndoc

        ' Add menu items.
        AddHandler Menu.MenuItems(0).Popup, AddressOf MenuFileOnPopup
        Menu.MenuItems(0).MenuItems.Add("-")

        ' File Page Setup item
        miFileSet = New MenuItem("Page Set&up...")
        AddHandler miFileSet.Click, AddressOf MenuFileSetupOnClick
        Menu.MenuItems(0).MenuItems.Add(miFileSet)

        ' File Print item
        miFilePrint = New MenuItem("&Print...")
        AddHandler miFilePrint.Click, AddressOf MenuFilePrintOnClick
        miFilePrint.Shortcut = Shortcut.CtrlP
        Menu.MenuItems(0).MenuItems.Add(miFilePrint)
        Menu.MenuItems(0).MenuItems.Add("-")

        ' File Properties item
        miFileProps = New MenuItem("Propert&ies...")
        AddHandler miFileProps.Click, AddressOf MenuFilePropsOnClick
        Menu.MenuItems(0).MenuItems.Add(miFileProps)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        If Not img Is Nothing Then
            ScaleImageIsotropically(pea.Graphics, img, _
                    RectangleF.op_Implicit(ClientRectangle))
        End If
    End Sub

    Private Sub MenuFileOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        Dim bEnable As Boolean = Not img Is Nothing
        miFilePrint.Enabled = bEnable
        miFileSet.Enabled = bEnable
        miFileProps.Enabled = bEnable
    End Sub

    Private Sub MenuFileSetupOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        setdlg.ShowDialog()
    End Sub

    Private Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        If prndlg.ShowDialog() = DialogResult.OK Then
            prndoc.DocumentName = Text
            prndoc.Print()
        End If
    End Sub

    Private Sub MenuFilePropsOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim str As String = _
            "Size = " & img.Size.ToString() & vbLf & _
            "Horizontal Resolution = " & img.HorizontalResolution & _
            vbLf & _
            "Vertical Resolution = " & img.VerticalResolution & vbLf & _
            "Physical Dimension = " & img.PhysicalDimension.ToString() & _
            vbLf & _
            "Pixel Format = " & img.PixelFormat
        MessageBox.Show(str, "Image Properties")
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
        ScaleImageIsotropically(grfx, img, rectf)
    End Sub

    Private Sub ScaleImageIsotropically(ByVal grfx As Graphics, _
                            ByVal img As Image, ByVal rectf As RectangleF)
        Dim szf As New SizeF(img.Width / img.HorizontalResolution, _
                             img.Height / img.VerticalResolution)
        Dim fScale As Single = Math.Min(rectf.Width / szf.Width, _
                                        rectf.Height / szf.Height)
        szf.Width *= fScale
        szf.Height *= fScale
        grfx.DrawImage(img, rectf.X + (rectf.Width - szf.Width) / 2, _
                            rectf.Y + (rectf.Height - szf.Height) / 2, _
                            szf.Width, szf.Height)
    End Sub
End Class
