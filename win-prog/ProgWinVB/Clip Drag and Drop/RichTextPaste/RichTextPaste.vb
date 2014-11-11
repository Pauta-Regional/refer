'----------------------------------------------
' RichTextPaste.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class RichTextPaste
    Inherits Form

    Private strPastedText As String = ""
    Private miPastePlain, miPasteRTF, miPasteHTML, miPasteCSV As MenuItem

    Shared Sub Main()
        Application.Run(New RichTextPaste())
    End Sub

    Sub New()
        Text = "Rich-Text Paste"
        ResizeRedraw = True

        Menu = New MainMenu()

        ' Edit menu
        Dim mi As New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        Menu.MenuItems.Add(mi)

        ' Edit Paste Plain Text menu item
        miPastePlain = New MenuItem("Paste &Plain Text")
        AddHandler miPastePlain.Click, AddressOf MenuEditPastePlainOnClick
        Menu.MenuItems(0).MenuItems.Add(miPastePlain)

        ' Edit Paste RTF menu item
        miPasteRTF = New MenuItem("Paste &Rich Text Format")
        AddHandler miPasteRTF.Click, AddressOf MenuEditPasteRTFOnClick
        Menu.MenuItems(0).MenuItems.Add(miPasteRTF)

        ' Edit Paste HTML menu item
        miPasteHTML = New MenuItem("Paste &HTML")
        AddHandler miPasteHTML.Click, AddressOf MenuEditPasteHTMLOnClick
        Menu.MenuItems(0).MenuItems.Add(miPasteHTML)

        ' Edit Paste CSV menu item
        miPasteCSV = New MenuItem("Paste &Comma-Separated Values")
        AddHandler miPasteCSV.Click, AddressOf MenuEditPasteCSVOnClick
        Menu.MenuItems(0).MenuItems.Add(miPasteCSV)
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miPastePlain.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(GetType(String))
        miPasteRTF.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf)
        miPasteHTML.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(DataFormats.Html)
        miPasteCSV.Enabled = _
            Clipboard.GetDataObject().GetDataPresent _
                                        (DataFormats.CommaSeparatedValue)
    End Sub

    Private Sub MenuEditPastePlainOnClick(ByVal obj As Object, _
                                          ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(String)) Then
            strPastedText = DirectCast(data.GetData(GetType(String)), String)
            Invalidate()
        End If
    End Sub

    Private Sub MenuEditPasteRTFOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        If data.GetDataPresent(DataFormats.Rtf) Then
            strPastedText = DirectCast(data.GetData(DataFormats.Rtf), String)
            Invalidate()
        End If
    End Sub

    Private Sub MenuEditPasteHTMLOnClick(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        If data.GetDataPresent(DataFormats.Html) Then
            strPastedText = _
                        DirectCast(data.GetData(DataFormats.Html), String)
            Invalidate()
        End If
    End Sub

    Private Sub MenuEditPasteCSVOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        Dim data As IDataObject = Clipboard.GetDataObject()
        If data.GetDataPresent(DataFormats.CommaSeparatedValue) Then
            Dim ms As MemoryStream = _
                            DirectCast(data.GetData("Csv"), MemoryStream)
            Dim sr As New StreamReader(ms)
            strPastedText = sr.ReadToEnd()
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString(strPastedText, Font, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle))
    End Sub
End Class
