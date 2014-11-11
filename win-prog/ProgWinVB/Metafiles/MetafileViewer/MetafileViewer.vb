'-----------------------------------------------
' MetafileViewer.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO                  ' For Path class
Imports System.Windows.Forms

Class MetafileViewer
    Inherits Form

    Protected mf As Metafile
    Protected strProgName As String
    Protected strFileName As String
    Private miFileSaveAs, miFilePrint, miFileProps, miViewChecked As MenuItem

    Shared Sub Main()
        Application.Run(New MetafileViewer())
    End Sub

    Sub New()
        strProgName = "Metafile Viewer"
        Text = strProgName
        ResizeRedraw = True

        Menu = New MainMenu()

        ' File menu
        Dim mi As New MenuItem("&File")
        AddHandler mi.Popup, AddressOf MenuFileOnPopup
        Menu.MenuItems.Add(mi)

        ' File Open menu item
        mi = New MenuItem("&Open...")
        AddHandler mi.Click, AddressOf MenuFileOpenOnClick
        mi.Shortcut = Shortcut.CtrlO
        Menu.MenuItems(0).MenuItems.Add(mi)

        ' File Save As Bitmap menu item
        miFileSaveAs = New MenuItem("Save &As Bitmap...")
        AddHandler miFileSaveAs.Click, AddressOf MenuFileSaveAsOnClick
        Menu.MenuItems(0).MenuItems.Add(miFileSaveAs)
        Menu.MenuItems(0).MenuItems.Add("-")

        ' File Print menu item
        miFilePrint = New MenuItem("&Print...")
        AddHandler miFilePrint.Click, AddressOf MenuFilePrintOnClick
        Menu.MenuItems(0).MenuItems.Add(miFilePrint)
        Menu.MenuItems(0).MenuItems.Add("-")

        ' File Properties menu item
        miFileProps = New MenuItem("Propert&ies...")
        AddHandler miFileProps.Click, AddressOf MenuFilePropsOnClick
        Menu.MenuItems(0).MenuItems.Add(miFileProps)

        ' Edit menu (temporary until Chapter 24)
        Menu.MenuItems.Add("&Edit")

        ' View menu
        Menu.MenuItems.Add("&View")
        Dim astr As String() = {"&Stretched to Window", _
                                "&Metrical Size", "&Pixel Size"}
        Dim eh As EventHandler = AddressOf MenuViewOnClick
        Dim str As String
        For Each str In astr
            Menu.MenuItems(2).MenuItems.Add(str, eh)
        Next str
        miViewChecked = Menu.MenuItems(2).MenuItems(0)
        miViewChecked.Checked = True
    End Sub

    Private Sub MenuFileOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        Dim bEnabled As Boolean = Not mf Is Nothing

        miFilePrint.Enabled = bEnabled
        miFileSaveAs.Enabled = bEnabled
        miFileProps.Enabled = bEnabled
    End Sub

    Private Sub MenuFileOpenOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Dim dlg As New OpenFileDialog()
        dlg.Filter = "All Metafiles|*.wmf;.emf|" & _
                     "Windows Metafile (*.wmf)|*.wmf|" & _
                     "Enhanced Metafile (*.emf)|*.emf|" & _
                     "All files|*.*"
        If dlg.ShowDialog() = DialogResult.OK Then
            Try
                mf = New Metafile(dlg.FileName)
            Catch exc As Exception
                MessageBox.Show(exc.Message, strProgName)
                Return
            End Try
            strFileName = dlg.FileName
            Text = strProgName & " - " & Path.GetFileName(strFileName)
            Invalidate()
        End If
    End Sub

    Protected Overridable Sub MenuFileSaveAsOnClick(ByVal obj As Object, _
                                                    ByVal ea As EventArgs)
        MessageBox.Show("Not yet implemented!", strProgName)
    End Sub

    Private Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim prndoc As New PrintDocument()
        prndoc.DocumentName = Text
        AddHandler prndoc.PrintPage, AddressOf OnPrintPage
        prndoc.Print()
    End Sub

    Private Sub MenuFilePropsOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim mh As MetafileHeader = mf.GetMetafileHeader()
        Dim str As String = _
            "Image Properties" & _
            vbLf & vbTab & "Size = " & mf.Size.ToString() & _
            vbLf & vbTab & "Horizontal Resolution = " & _
            mf.HorizontalResolution & _
            vbLf & vbTab & "Vertical Resolution = " & _
            mf.VerticalResolution & _
            vbLf & vbTab & "Physical Dimension = " & _
            mf.PhysicalDimension.ToString() & _
            vbLf & vbLf & "Metafile Header Properties" & _
            vbLf & vbTab & "Bounds = " & mh.Bounds.ToString() & _
            vbLf & vbTab & "DpiX = " & mh.DpiX & _
            vbLf & vbTab & "DpiY = " & mh.DpiY & _
            vbLf & vbTab & "LogicalDpiX = " & mh.LogicalDpiX & _
            vbLf & vbTab & "LogicalDpiY = " & mh.LogicalDpiY & _
            vbLf & vbTab & "Type = " & mh.Type & _
            vbLf & vbTab & "Version = " & mh.Version & _
            vbLf & vbTab & "MetafileSize = " & mh.MetafileSize
        MessageBox.Show(str, Text)
    End Sub

    Private Sub MenuViewOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        miViewChecked.Checked = False
        miViewChecked = DirectCast(obj, MenuItem)
        miViewChecked.Checked = True
        Invalidate()
    End Sub

    Private Sub OnPrintPage(ByVal obj As Object, _
                            ByVal ppea As PrintPageEventArgs)
        Dim grfx As Graphics = ppea.Graphics
        Dim rect As New Rectangle( _
            ppea.MarginBounds.Left - _
            (ppea.PageBounds.Width - _
                CInt(grfx.VisibleClipBounds.Width)) \ 2, _
            ppea.MarginBounds.Top - _
            (ppea.PageBounds.Height - _
                CInt(grfx.VisibleClipBounds.Height)) \ 2, _
            ppea.MarginBounds.Width, _
            ppea.MarginBounds.Height)
        DisplayMetafile(grfx, rect)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        If Not mf Is Nothing Then
            DisplayMetafile(pea.Graphics, ClientRectangle)
        End If
    End Sub

    Private Sub DisplayMetafile(ByVal grfx As Graphics, _
                                ByVal rect As Rectangle)
        Select Case miViewChecked.Index
            Case 0 : grfx.DrawImage(mf, rect)
            Case 1 : grfx.DrawImage(mf, rect.X, rect.Y)
            Case 2 : grfx.DrawImage(mf, rect.X, rect.Y, mf.Width, mf.Height)
        End Select
    End Sub
End Class
