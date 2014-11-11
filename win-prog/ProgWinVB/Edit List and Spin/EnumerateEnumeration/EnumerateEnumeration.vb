'-----------------------------------------------------
' EnumerateEnumeration.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Text                ' For the StringBuilder class
Imports System.Windows.Forms

Class EnumerateEnumeration
    Inherits Form

    Private btn As Button
    Private txtboxLibrary, txtboxNamespace, _
            txtboxEnumeration, txtboxOutput As TextBox
    Private chkboxHex As CheckBox

    Shared Sub Main()
        Application.Run(New EnumerateEnumeration())
    End Sub

    Sub New()
        Text = "Enumerate Enumeration"
        ClientSize = New Size(242, 164)

        Dim lbl As New Label()
        lbl.Parent = Me
        lbl.Text = "Library:"
        lbl.Location = New Point(8, 8)
        lbl.Size = New Size(56, 8)

        txtboxLibrary = New TextBox()
        txtboxLibrary.Parent = Me
        txtboxLibrary.Text = "system.windows.forms"
        txtboxLibrary.Location = New Point(64, 8)
        txtboxLibrary.Size = New Size(120, 12)
        txtboxLibrary.Anchor = txtboxLibrary.Anchor Or AnchorStyles.Right

        Dim tip As New ToolTip()
        tip.SetToolTip(txtboxLibrary, _
                            "Enter the name of a .NET dynamic" & vbLf & _
                            "link libary, such as 'mscorlib'," & vbLf & _
                            "'system.windows.forms', or" & vbLf & _
                            "'system.drawing'.")

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = "Namespace:"
        lbl.Location = New Point(8, 24)
        lbl.Size = New Size(56, 8)

        txtboxNamespace = New TextBox()
        txtboxNamespace.Parent = Me
        txtboxNamespace.Text = "System.Windows.Forms"
        txtboxNamespace.Location = New Point(64, 24)
        txtboxNamespace.Size = New Size(120, 12)
        txtboxNamespace.Anchor = txtboxNamespace.Anchor Or AnchorStyles.Right

        tip.SetToolTip(txtboxNamespace, _
                            "Enter the name of a namespace" & vbLf & _
                            "within the library, such as" & vbLf & _
                            "'System', 'System.IO'," & vbLf & _
                            "'System.Drawing'," & vbLf & _
                            "'System.Drawing.Drawing2D'," & vbLf & _
                            "or 'System.Windows.Forms'.")

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = "Enumeration:"
        lbl.Location = New Point(8, 40)
        lbl.Size = New Size(56, 8)

        txtboxEnumeration = New TextBox()
        txtboxEnumeration.Parent = Me
        txtboxEnumeration.Text = "ScrollBars"
        txtboxEnumeration.Location = New Point(64, 40)
        txtboxEnumeration.Size = New Size(120, 12)
        txtboxEnumeration.Anchor = txtboxEnumeration.Anchor Or _
                                                AnchorStyles.Right

        tip.SetToolTip(txtboxEnumeration, _
                           "Enter the name of an enumeration" & vbLf & _
                           "defined in the namespace.")

        chkboxHex = New CheckBox()
        chkboxHex.Parent = Me
        chkboxHex.Text = "Hex"
        chkboxHex.Location = New Point(192, 16)
        chkboxHex.Size = New Size(40, 8)
        chkboxHex.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        AddHandler chkboxHex.CheckedChanged, _
                                    AddressOf CheckBoxOnCheckedChanged

        tip.SetToolTip(chkboxHex, "Check this box to display the" & vbLf & _
                                  "enumeration values in hexadecimal.")

        btn = New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Location = New Point(192, 32)
        btn.Size = New Size(40, 16)
        btn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        AddHandler btn.Click, AddressOf ButtonOkOnClick
        AcceptButton = btn

        tip.SetToolTip(btn, "Click this button to display results.")

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

        ' Initialize the display.
        ButtonOkOnClick(btn, EventArgs.Empty)
    End Sub

    Private Sub CheckBoxOnCheckedChanged(ByVal sender As Object, _
                                         ByVal ea As EventArgs)
        btn.PerformClick()
    End Sub

    Private Sub ButtonOkOnClick(ByVal sender As Object, _
                                ByVal ea As EventArgs)
        FillTextBox(txtboxOutput, txtboxLibrary.Text, _
                    txtboxNamespace.Text, txtboxEnumeration.Text, _
                    chkboxHex.Checked)
    End Sub

    Shared Function FillTextBox(ByVal txtboxOutput As TextBox, _
                                ByVal strLibrary As String, _
                                ByVal strNamespace As String, _
                                ByVal strEnumeration As String, _
                                ByVal bHexadecimal As Boolean) As Boolean
        Dim strEnumText As String = strNamespace & "." & strEnumeration
        Dim strAssembly As String
        Try
            strAssembly = System.Reflection.Assembly. _
                                LoadWithPartialName(strLibrary).FullName
        Catch
            Return False
        End Try

        Dim strFullText As String = strEnumText & "," & strAssembly

        ' Get the type of the enum.
        Dim typ As Type = Type.GetType(strFullText, False, True)
        If typ Is Nothing Then
            txtboxOutput.Text = """" & strFullText & _
                                """ is not a valid type."
            Return False
        ElseIf Not typ.IsEnum Then
            txtboxOutput.Text = """" & strEnumText & _
                                """ is a valid type but not an enum."
            Return False
        End If

        ' Get all the members in that enum.
        Dim astrMembers() As String = System.Enum.GetNames(typ)
        Dim arr As Array = System.Enum.GetValues(typ)
        Dim aobjMembers(arr.GetUpperBound(0)) As Object
        arr.CopyTo(aobjMembers, 0)

        ' Create a StringBuilder for the text.
        Dim sb As New StringBuilder()

        ' Append the enumeration name and headings.
        sb.Append(strEnumeration)
        sb.Append(" Enumeration" & vbCrLf & _
                  "Member" & vbTab & "Value" & vbCrLf)

        ' Append the text rendition and the actual numeric values.
        Dim i As Integer
        For i = 0 To astrMembers.GetUpperBound(0)
            sb.Append(astrMembers(i))
            sb.Append(vbTab)
            If bHexadecimal Then
                sb.Append("&H" & System.Enum.Format(typ, aobjMembers(i), _
                                                    "X"))
            Else
                sb.Append(System.Enum.Format(typ, aobjMembers(i), "D"))
            End If
            sb.Append(vbCrLf)
        Next i

        ' Append some other information.
        sb.Append(vbCrLf & "Total = " & _
                  astrMembers.Length.ToString() & vbCrLf)
        sb.Append(vbCrLf & typ.AssemblyQualifiedName & vbCrLf)

        ' Set the text box Text property from the StringBuilder.
        txtboxOutput.Text = sb.ToString()
        txtboxOutput.SelectionLength = 0
        Return True
    End Function
End Class
