'-------------------------------------------------------
' PrinterSelectionDialog.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class PrinterSelectionDialog
    Inherits Form

    Private combo As ComboBox

    Sub New()
        Text = "Select Printer"
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Location = Point.op_Addition(ActiveForm.Location, _
                   Size.op_Addition(SystemInformation.CaptionButtonSize, _
                                    SystemInformation.FrameBorderSize))
        Dim lbl As New Label()
        lbl.Parent = Me
        lbl.Text = "Printer:"
        lbl.Location = New Point(8, 8)
        lbl.Size = New Size(40, 8)

        combo = New ComboBox()
        combo.Parent = Me
        combo.DropDownStyle = ComboBoxStyle.DropDownList
        combo.Location = New Point(48, 8)
        combo.Size = New Size(144, 8)

        ' Add the installed printers to the combo box.
        Dim str As String
        For Each str In PrinterSettings.InstalledPrinters
            combo.Items.Add(str)
        Next str

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Location = New Point(40, 32)
        btn.Size = New Size(40, 16)
        btn.DialogResult = DialogResult.OK
        AcceptButton = btn

        btn = New Button()
        btn.Parent = Me
        btn.Text = "Cancel"
        btn.Location = New Point(120, 32)
        btn.Size = New Size(40, 16)
        btn.DialogResult = DialogResult.Cancel
        CancelButton = btn

        ClientSize = New Size(200, 56)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Property PrinterName() As String
        Set(ByVal Value As String)
            combo.SelectedItem = Value
        End Set
        Get
            Return combo.SelectedItem.ToString()
        End Get
    End Property
End Class
