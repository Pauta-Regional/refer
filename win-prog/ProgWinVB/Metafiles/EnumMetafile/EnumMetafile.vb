'---------------------------------------------
' EnumMetafile.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Class EnumMetafile
    Inherits Form

    Private mf As Metafile
    Private pnl As panel
    Private txtbox As TextBox
    Private strCaption As String
    Private strwrite As StringWriter

    Shared Sub Main()
        Application.Run(New EnumMetafile())
    End Sub

    Sub New()
        strCaption = "Enumerate Metafile"
        Text = strCaption

        ' Create the text box for displaying records.
        txtbox = New TextBox()
        txtbox.Parent = Me
        txtbox.Dock = DockStyle.Fill
        txtbox.Multiline = True
        txtbox.WordWrap = False
        txtbox.ReadOnly = True
        txtbox.TabStop = False
        txtbox.ScrollBars = ScrollBars.Vertical

        ' Create the splitter between the panel and the text box.
        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left

        ' Create the panel for displaying the metafile.
        pnl = New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Left
        AddHandler pnl.Paint, AddressOf PanelOnPaint

        ' Create the menu.
        Menu = New MainMenu()
        Menu.MenuItems.Add("&Open!", AddressOf MenuOpenOnClick)
    End Sub

    Private Sub MenuOpenOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New OpenFileDialog()
        dlg.Filter = "All Metafiles|*.wmf;*.emf|" & _
                     "Windows Metafile (*.wmf)|*.wmf|" & _
                     "Enhanced Metafile (*.emf)|*.emf"
        If dlg.ShowDialog() = DialogResult.OK Then
            Try
                mf = New Metafile(dlg.FileName)
            Catch exc As Exception
                MessageBox.Show(exc.Message, strCaption)
                Return
            End Try

            Text = strCaption & " - " & Path.GetFileName(dlg.FileName)
            pnl.Invalidate()

            ' Enumerate the metafile for the text box.
            strwrite = New StringWriter()
            Dim grfx As Graphics = CreateGraphics()
            grfx.EnumerateMetafile(mf, New Point(0, 0), _
                                   AddressOf EnumMetafileProc)
            grfx.Dispose()

            txtbox.Text = strwrite.ToString()
            txtbox.SelectionLength = 0
        End If
    End Sub

    Private Function EnumMetafileProc(ByVal eprt As EmfPlusRecordType, _
                ByVal iFlags As Integer, ByVal iDataSize As Integer, _
                ByVal ipData As IntPtr, _
                ByVal prc As PlayRecordCallback) As Boolean
        strwrite.Write("{0} ({1}, {2})", eprt, iFlags, iDataSize)

        If iDataSize > 0 Then
            Dim abyData(iDataSize) As Byte
            Marshal.Copy(ipData, abyData, 0, iDataSize)
            Dim by As Byte
            For Each by In abyData
                strwrite.Write(" {0:X2}", by)
            Next by
        End If
        strwrite.WriteLine()
        Return True
    End Function

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics

        If Not mf Is Nothing Then
            grfx.DrawImage(mf, 0, 0)
        End If
    End Sub
End Class
