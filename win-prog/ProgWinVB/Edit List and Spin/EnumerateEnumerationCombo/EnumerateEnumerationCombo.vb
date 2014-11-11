'----------------------------------------------------------
' EnumerateEnumerationCombo.vb (c) 2002 by Charles Petzold
'----------------------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class EnumerateEnumerationCombo
    Inherits Form

    Private comboHex As CheckBox
    Private comboLibrary, comboNamespace, comboEnumeration As ComboBox
    Private txtboxOutput As TextBox

    Const strRegKeyBase As String = _
        "Software\ProgrammingWindowsWithVBdotNet\EnumerateEnumerationCombo"

    Shared Sub Main()
        Application.Run(New EnumerateEnumerationCombo())
    End Sub

    Sub New()
        Text = "Enumerate Enumeration (Combo)"
        ClientSize = New Size(242, 164)

        Dim lbl As New Label()
        lbl.Parent = Me
        lbl.Text = "Library:"
        lbl.Location = New Point(8, 8)
        lbl.Size = New Size(56, 8)

        comboLibrary = New ComboBox()
        comboLibrary.Parent = Me
        comboLibrary.DropDownStyle = ComboBoxStyle.DropDown
        comboLibrary.Sorted = True
        comboLibrary.Location = New Point(64, 8)
        comboLibrary.Size = New Size(120, 12)
        comboLibrary.Anchor = comboLibrary.Anchor Or AnchorStyles.Right
        AddHandler comboLibrary.TextChanged, _
                                AddressOf ComboBoxLibraryOnTextChanged

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = "Namespace:"
        lbl.Location = New Point(8, 24)
        lbl.Size = New Size(56, 8)

        comboNamespace = New ComboBox()
        comboNamespace.Parent = Me
        comboNamespace.DropDownStyle = ComboBoxStyle.DropDown
        comboNamespace.Sorted = True
        comboNamespace.Location = New Point(64, 24)
        comboNamespace.Size = New Size(120, 12)
        comboNamespace.Anchor = comboNamespace.Anchor Or AnchorStyles.Right
        AddHandler comboNamespace.TextChanged, _
                                AddressOf ComboBoxNamespaceOnTextChanged

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = "Enumeration:"
        lbl.Location = New Point(8, 40)
        lbl.Size = New Size(56, 8)

        comboEnumeration = New ComboBox()
        comboEnumeration.Parent = Me
        comboEnumeration.DropDownStyle = ComboBoxStyle.DropDown
        comboEnumeration.Sorted = True
        comboEnumeration.Location = New Point(64, 40)
        comboEnumeration.Size = New Size(120, 12)
        comboEnumeration.Anchor = comboEnumeration.Anchor Or _
                                  AnchorStyles.Right
        AddHandler comboEnumeration.TextChanged, _
                                AddressOf ComboBoxEnumerationOnTextChanged

        comboHex = New CheckBox()
        comboHex.Parent = Me
        comboHex.Text = "Hex"
        comboHex.Location = New Point(192, 25)
        comboHex.Size = New Size(40, 8)
        comboHex.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        AddHandler comboHex.CheckedChanged, _
                                AddressOf CheckBoxOnCheckedChanged

        txtboxOutput = New TextBox()
        txtboxOutput.Parent = Me
        txtboxOutput.ReadOnly = True
        txtboxOutput.Multiline = True
        txtboxOutput.ScrollBars = ScrollBars.Vertical
        txtboxOutput.Location = New Point(8, 56)
        txtboxOutput.Size = New Size(226, 100)
        txtboxOutput.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or _
                              AnchorStyles.Right Or AnchorStyles.Bottom

        AutoScaleBaseSize = New Size(4, 8)

        ' Initialize display.
        FillComboBox(comboLibrary, strRegKeyBase)
        UpdateTextBox()
    End Sub

    Private Sub ComboBoxLibraryOnTextChanged(ByVal obj As Object, _
                                             ByVal ea As EventArgs)
        FillComboBox(comboNamespace, strRegKeyBase & "\" & _
                                     comboLibrary.Text)
        ComboBoxNamespaceOnTextChanged(obj, ea)
    End Sub

    Private Sub ComboBoxNamespaceOnTextChanged(ByVal obj As Object, _
                                               ByVal ea As EventArgs)
        FillComboBox(comboEnumeration, strRegKeyBase & "\" & _
                                       comboLibrary.Text & "\" & _
                                       comboNamespace.Text)
        ComboBoxEnumerationOnTextChanged(obj, ea)
    End Sub

    Private Sub ComboBoxEnumerationOnTextChanged(ByVal obj As Object, _
                                                 ByVal ea As EventArgs)
        UpdateTextBox()
    End Sub

    Private Sub CheckBoxOnCheckedChanged(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        UpdateTextBox()
    End Sub

    Private Sub UpdateTextBox()
        If EnumerateEnumeration.FillTextBox(txtboxOutput, _
                comboLibrary.Text, comboNamespace.Text, _
                comboEnumeration.Text, comboHex.Checked) Then
            If Not comboLibrary.Items.Contains(comboLibrary.Text) Then
                comboLibrary.Items.Add(comboLibrary.Text)
            End If
            If Not comboNamespace.Items.Contains(comboNamespace.Text) Then
                comboNamespace.Items.Add(comboNamespace.Text)
            End If
            If Not comboEnumeration.Items.Contains( _
                                                comboEnumeration.Text) Then
                comboEnumeration.Items.Add(comboEnumeration.Text)
            End If
            Dim strRegKey As String = strRegKeyBase & "\" & _
                                      comboLibrary.Text & "\" & _
                                      comboNamespace.Text & "\" & _
                                      comboEnumeration.Text
            Dim regkey As RegistryKey = _
                        Registry.CurrentUser.OpenSubKey(strRegKey)
            If regkey Is Nothing Then
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey)
            End If
            regkey.Close()
        End If
    End Sub

    Private Function FillComboBox(ByVal combo As ComboBox, _
                                  ByVal strRegKey As String) As Boolean
        combo.Items.Clear()
        Dim regkey As RegistryKey = _
                            Registry.CurrentUser.OpenSubKey(strRegKey)
        If Not regkey Is Nothing Then
            Dim astrSubKeys() As String = regkey.GetSubKeyNames()
            regkey.Close()
            If astrSubKeys.Length > 0 Then
                combo.Items.AddRange(astrSubKeys)
                combo.SelectedIndex = 0
                Return True
            End If
        End If
        combo.Text = ""
        Return False
    End Function
End Class
