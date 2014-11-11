'-------------------------------------------------------
' NotepadCloneWithFormat.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NotepadCloneWithFormat
    Inherits NotepadCloneWithEdit

    ' Strings for registry
    Const strWordWrap As String = "WordWrap"
    Const strFontFace As String = "FontFace"
    Const strFontSize As String = "FontSize"
    Const strFontStyle As String = "FontStyle"
    Const strForeColor As String = "ForeColor"
    Const strBackColor As String = "BackColor"
    Const strCustomClr As String = "CustomColor"

    Private clrdlg As New ColorDialog()
    Private miFormatWrap As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New NotepadCloneWithFormat())
    End Sub

    Sub New()
        strProgName = "Notepad Clone with Format"
        MakeCaption()

        ' Format
        Dim mi As New MenuItem("F&ormat")
        AddHandler mi.Popup, AddressOf MenuFormatOnPopup
        Menu.MenuItems.Add(mi)
        Dim index As Integer = Menu.MenuItems.Count - 1

        ' Format Word Wrap
        miFormatWrap = New MenuItem("&Word Wrap")
        AddHandler miFormatWrap.Click, AddressOf MenuFormatWrapOnClick
        Menu.MenuItems(index).MenuItems.Add(miFormatWrap)

        ' Format Font
        mi = New MenuItem("&Font...")
        AddHandler mi.Click, AddressOf MenuFormatFontOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)

        ' Format Background Color
        mi = New MenuItem("Background &Color...")
        AddHandler mi.Click, AddressOf MenuFormatColorOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)
    End Sub

    Protected Overrides Sub OnLoad(ByVal ea As EventArgs)
        MyBase.OnLoad(ea)

        ' Help
        Dim mi As New MenuItem("&Help")
        Menu.MenuItems.Add(mi)
        Dim index As Integer = Menu.MenuItems.Count - 1

        ' Help About
        mi = New MenuItem("&About " & strProgName & "...")
        AddHandler mi.Click, AddressOf MenuHelpAboutOnClick
        Menu.MenuItems(index).MenuItems.Add(mi)
    End Sub

    Private Sub MenuFormatOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miFormatWrap.Checked = txtbox.WordWrap
    End Sub

    Private Sub MenuFormatWrapOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        mi.Checked = Not mi.Checked
        txtbox.WordWrap = mi.Checked
    End Sub

    Private Sub MenuFormatFontOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        Dim fntdlg As New FontDialog()
        fntdlg.ShowColor = True
        fntdlg.Font = txtbox.Font
        fntdlg.Color = txtbox.ForeColor
        If fntdlg.ShowDialog() = DialogResult.OK Then
            txtbox.Font = fntdlg.Font
            txtbox.ForeColor = fntdlg.Color
        End If
    End Sub

    Private Sub MenuFormatColorOnClick(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        clrdlg.Color = txtbox.BackColor
        If clrdlg.ShowDialog() = DialogResult.OK Then
            txtbox.BackColor = clrdlg.Color
        End If
    End Sub

    Private Sub MenuHelpAboutOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        MessageBox.Show(strProgName & " " & Chr(169) & _
                        " 2002 by Charles Petzold", strProgName)
    End Sub

    Protected Overrides Sub LoadRegistryInfo(ByVal regkey As RegistryKey)
        MyBase.LoadRegistryInfo(regkey)
        txtbox.WordWrap = CBool(regkey.GetValue(strWordWrap))
        txtbox.Font = New Font( _
            DirectCast(regkey.GetValue(strFontFace), String), _
            Single.Parse(DirectCast(regkey.GetValue(strFontSize), String)), _
            CType(regkey.GetValue(strFontStyle), FontStyle))
        txtbox.ForeColor = Color.FromArgb( _
                                    CInt(regkey.GetValue(strForeColor)))
        txtbox.BackColor = Color.FromArgb( _
                                    CInt(regkey.GetValue(strBackColor)))

        Dim i, aiColors(16) As Integer

        For i = 0 To 15
            aiColors(i) = CInt(regkey.GetValue(strCustomClr & i))
        Next i
        clrdlg.CustomColors = aiColors
    End Sub

    Protected Overrides Sub SaveRegistryInfo(ByVal regkey As RegistryKey)
        MyBase.SaveRegistryInfo(regkey)
        regkey.SetValue(strWordWrap, CInt(txtbox.WordWrap))
        regkey.SetValue(strFontFace, txtbox.Font.Name)
        regkey.SetValue(strFontSize, txtbox.Font.SizeInPoints.ToString())
        regkey.SetValue(strFontStyle, CInt(txtbox.Font.Style))
        regkey.SetValue(strForeColor, txtbox.ForeColor.ToArgb())
        regkey.SetValue(strBackColor, txtbox.BackColor.ToArgb())

        Dim i As Integer
        For i = 0 To 15
            regkey.SetValue(strCustomClr & i, clrdlg.CustomColors(i))
        Next i
    End Sub
End Class
