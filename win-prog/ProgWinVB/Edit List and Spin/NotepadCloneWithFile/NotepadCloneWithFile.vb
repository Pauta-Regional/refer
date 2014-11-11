'-----------------------------------------------------
' NotepadCloneWithFile.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports Microsoft.Win32       ' For registry classes
Imports System
Imports System.ComponentModel ' For CancelEventArgs class
Imports System.Drawing
Imports System.IO
Imports System.Text           ' For Encoding class
Imports System.Windows.Forms

Class NotepadCloneWithFile
    Inherits NotepadCloneWithRegistry

    ' Fields
    Protected strFileName As String
    Const strEncoding As String = "Encoding"     ' For registry
    Const strFilter As String = _
                        "Text Documents(*.txt)|*.txt|All Files(*.*)|*.*"
    Private miEncoding As MenuItem
    Private mieChecked As MenuItemEncoding

    ' Entry point
    Shared Shadows Sub Main()
        Application.Run(New NotepadCloneWithFile())
    End Sub

    ' Constructor
    Sub New()
        strProgName = "Notepad Clone with File"
        MakeCaption()

        Menu = New MainMenu()

        ' File menu
        Dim mi As New MenuItem("&File")
        Menu.MenuItems.Add(mi)
        Dim index As Integer = Menu.MenuItems.Count - 1

        ' File New
        mi = New MenuItem("&New")
        AddHandler mi.Click, AddressOf MenuFileNewOnClick
        mi.Shortcut = Shortcut.CtrlN
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' File Open
        Dim miFileOpen As New MenuItem("&Open...")
        AddHandler miFileOpen.Click, AddressOf MenuFileOpenOnClick
        miFileOpen.Shortcut = Shortcut.CtrlO
        Menu.MenuItems(index).MenuItems.Add(miFileOpen)

        ' File Save
        Dim miFileSave As New MenuItem("&Save")
        AddHandler miFileSave.Click, AddressOf MenuFileSaveOnClick
        miFileSave.Shortcut = Shortcut.CtrlS
        Menu.MenuItems(index).MenuItems.Add(miFileSave)

        ' File Save As
        mi = New MenuItem("Save &As...")
        AddHandler mi.Click, AddressOf MenuFileSaveAsOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' File Encoding
        miEncoding = New MenuItem("&Encoding")
        Menu.MenuItems(index).MenuItems.Add(miEncoding)
        Menu.MenuItems(index).MenuItems.Add("-")

        ' File Encoding submenu
        Dim eh As EventHandler = AddressOf MenuFileEncodingOnClick
        Dim astrEncodings() As String = {"&ASCII", "&Unicode", _
                                         "&Big-Endian Unicode", _
                                         "UTF-&7", "&UTF-&8"}
        Dim aenc() As Encoding = {Encoding.ASCII, Encoding.Unicode, _
                                  Encoding.BigEndianUnicode, _
                                  Encoding.UTF7, Encoding.UTF8}
        Dim i As Integer

        For i = 0 To astrEncodings.GetUpperBound(0)
            Dim mie As New MenuItemEncoding()
            mie.Text = astrEncodings(i)
            mie.encode = aenc(i)
            mie.RadioCheck = True
            AddHandler mie.Click, eh
            miEncoding.MenuItems.Add(mie)
        Next i
        ' Set default to UTF-8
        mieChecked = DirectCast(miEncoding.MenuItems(4), MenuItemEncoding)
        mieChecked.Checked = True

        ' File Page Setup
        mi = New MenuItem("Page Set&up...")
        AddHandler mi.Click, AddressOf MenuFileSetupOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' File Print Preview
        mi = New MenuItem("Print Pre&view...")
        AddHandler mi.Click, AddressOf MenuFilePreviewOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' File Print
        mi = New MenuItem("&Print...")
        AddHandler mi.Click, AddressOf MenuFilePrintOnClick
        mi.Shortcut = Shortcut.CtrlP
        Menu.MenuItems(index).MenuItems.Add(mi)
        Menu.MenuItems(index).MenuItems.Add("-")

        ' File Exit
        mi = New MenuItem("E&xit")
        AddHandler mi.Click, AddressOf MenuFileExitOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Set system event.
        AddHandler SystemEvents.SessionEnding, AddressOf OnSessionEnding
    End Sub

    ' Event overrides
    Protected Overrides Sub OnLoad(ByVal ea As EventArgs)
        MyBase.OnLoad(ea)

        ' Deal with the command-line argument.
        Dim astrArgs As String() = Environment.GetCommandLineArgs()
        If astrArgs.Length > 1 Then
            If File.Exists(astrArgs(1)) Then
                LoadFile(astrArgs(1))
            Else
                Dim dr As DialogResult = _
                    MessageBox.Show("Cannot find the " & _
                                    Path.GetFileName(astrArgs(1)) & _
                                    " file." & vbLf & vbLf & _
                                    "Do you want to create a new file?", _
                                    strProgName, _
                                    MessageBoxButtons.YesNoCancel, _
                                    MessageBoxIcon.Question)
                Select Case dr
                    Case DialogResult.Yes    ' Create and close file.
                        strFileName = astrArgs(1)
                        File.Create(strFileName).Close()
                        MakeCaption()

                    Case DialogResult.No     ' Don't do anything.

                    Case DialogResult.Cancel ' Close program
                        Close()
                End Select
            End If
        End If
    End Sub

    Protected Overrides Sub OnClosing(ByVal cea As CancelEventArgs)
        MyBase.OnClosing(cea)
        cea.Cancel = Not OkToTrash()
    End Sub

    ' Event handlers
    Private Sub OnSessionEnding(ByVal obj As Object, _
                                ByVal seea As SessionEndingEventArgs)
        seea.Cancel = Not OkToTrash()
    End Sub

    ' Menu items
    Private Sub MenuFileNewOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        If Not OkToTrash() Then Return

        txtbox.Clear()
        txtbox.ClearUndo()
        txtbox.Modified = False
        strFileName = Nothing
        MakeCaption()
    End Sub

    Private Sub MenuFileOpenOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        If Not OkToTrash() Then Return

        Dim ofd As New OpenFileDialog()
        ofd.Filter = strFilter
        ofd.FileName = "*.txt"
        If ofd.ShowDialog() = DialogResult.OK Then LoadFile(ofd.FileName)
    End Sub

    Private Sub MenuFileEncodingOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        mieChecked.Checked = False
        mieChecked = DirectCast(obj, MenuItemEncoding)
        mieChecked.Checked = True
    End Sub

    Private Sub MenuFileSaveOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        If strFileName Is Nothing OrElse strFileName.Length = 0 Then
            SaveFileDlg()
        Else
            SaveFile()
        End If
    End Sub

    Private Sub MenuFileSaveAsOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        SaveFileDlg()
    End Sub

    Protected Overridable Sub MenuFileSetupOnClick(ByVal obj As Object, _
                                                   ByVal ea As EventArgs)
        MessageBox.Show("Page Setup not yet implemented!", strProgName)
    End Sub

    Protected Overridable Sub MenuFilePreviewOnClick(ByVal obj As Object, _
                                                     ByVal ea As EventArgs)
        MessageBox.Show("Print Preview not yet implemented!", strProgName)
    End Sub

    Protected Overridable Sub MenuFilePrintOnClick(ByVal obj As Object, _
                                                   ByVal ea As EventArgs)
        MessageBox.Show("Print not yet implemented!", strProgName)
    End Sub

    Private Sub MenuFileExitOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        If OkToTrash() Then Application.Exit()
    End Sub

    ' Method overrides
    Protected Overrides Sub LoadRegistryInfo(ByVal regkey As RegistryKey)
        MyBase.LoadRegistryInfo(regkey)

        ' Set encoding setting.
        Dim index As Integer = DirectCast(regkey.GetValue(strEncoding, 4), _
                                          Integer)
        mieChecked.Checked = False
        mieChecked = DirectCast(miEncoding.MenuItems(index), _
                                MenuItemEncoding)
        mieChecked.Checked = True
    End Sub

    Protected Overrides Sub SaveRegistryInfo(ByVal regkey As RegistryKey)
        MyBase.SaveRegistryInfo(regkey)
        regkey.SetValue(strEncoding, mieChecked.Index)
    End Sub

    ' Utility routines
    Protected Sub LoadFile(ByVal strFileName As String)
        Dim sr As StreamReader
        Try
            sr = New StreamReader(strFileName)
        Catch exc As Exception
            MessageBox.Show(exc.Message, strProgName, _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Asterisk)
            Return
        End Try

        txtbox.Text = sr.ReadToEnd()
        sr.Close()
        Me.strFileName = strFileName
        MakeCaption()

        txtbox.SelectionStart = 0
        txtbox.SelectionLength = 0
        txtbox.Modified = False
        txtbox.ClearUndo()
    End Sub

    Private Sub SaveFile()
        Try
            Dim sw As New StreamWriter(strFileName, False, _
                                        mieChecked.encode)
            sw.Write(txtbox.Text)
            sw.Close()
        Catch exc As Exception
            MessageBox.Show(exc.Message, strProgName, _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Asterisk)
            Return
        End Try
        txtbox.Modified = False
    End Sub

    Private Function SaveFileDlg() As Boolean
        Dim sfd As New SaveFileDialog()

        If Not strFileName Is Nothing AndAlso strFileName.Length > 1 Then
            sfd.FileName = strFileName
        Else
            sfd.FileName = "*.txt"
        End If

        sfd.Filter = strFilter

        If sfd.ShowDialog() = DialogResult.OK Then
            strFileName = sfd.FileName
            SaveFile()
            MakeCaption()
            Return True
        Else
            Return False   ' Return values are for OkToTrash.
        End If
    End Function

    Protected Sub MakeCaption()
        Text = strProgName & " - " & FileTitle()
    End Sub

    Protected Function FileTitle() As String
        If Not strFileName = Nothing AndAlso strFileName.Length > 1 Then
            Return Path.GetFileName(strFileName)
        Else
            Return "Untitled"
        End If
    End Function

    Protected Function OkToTrash() As Boolean
        If Not txtbox.Modified Then
            Return True
        End If
        Dim dr As DialogResult = _
                MessageBox.Show("The text in the " & FileTitle() & _
                                " file has changed." & vbLf & vbLf & _
                                "Do you want to save the changes?", _
                                strProgName, _
                                MessageBoxButtons.YesNoCancel, _
                                MessageBoxIcon.Exclamation)
        Select Case dr
            Case DialogResult.Yes : Return SaveFileDlg()
            Case DialogResult.No : Return True
            Case DialogResult.Cancel : Return False
        End Select
        Return False
    End Function
End Class

Class MenuItemEncoding
    Inherits MenuItem

    Public encode As Encoding
End Class
