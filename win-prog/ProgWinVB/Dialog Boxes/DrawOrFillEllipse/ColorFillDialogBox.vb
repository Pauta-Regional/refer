'---------------------------------------------------
' ColorFillDialogBox.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ColorFillDialogBox
    Inherits Form

    Protected grpbox As GroupBox
    Protected chkbox As CheckBox

    Sub New()
        Text = "Color/Fill Select"

        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        Location = Point.op_Addition(ActiveForm.Location, _
                   Size.op_Addition(SystemInformation.CaptionButtonSize, _
                                    SystemInformation.FrameBorderSize))

        Dim astrColor() As String = {"Black", "Blue", "Green", "Cyan", _
                                     "Red", "Magenta", "Yellow", "White"}
        grpbox = New GroupBox()
        grpbox.Parent = Me
        grpbox.Text = "Color"
        grpbox.Location = New Point(8, 8)
        grpbox.Size = New Size(96, 12 * (astrColor.Length + 1))

        Dim i As Integer
        For i = 0 To astrColor.GetUpperBound(0)
            Dim radiobtn As New RadioButton()
            radiobtn.Parent = grpbox
            radiobtn.Text = astrColor(i)
            radiobtn.Location = New Point(8, 12 * (i + 1))
            radiobtn.Size = New Size(80, 10)
        Next i

        chkbox = New CheckBox()
        chkbox.Parent = Me
        chkbox.Text = "Fill Ellipse"
        chkbox.Location = New Point(8, grpbox.Bottom + 4)
        chkbox.Size = New Size(80, 10)

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Location = New Point(8, chkbox.Bottom + 4)
        btn.Size = New Size(40, 16)
        btn.DialogResult = DialogResult.OK

        AcceptButton = btn

        btn = New Button()
        btn.Parent = Me
        btn.Text = "Cancel"
        btn.Location = New Point(64, chkbox.Bottom + 4)
        btn.Size = New Size(40, 16)
        btn.DialogResult = DialogResult.Cancel

        CancelButton = btn

        ClientSize = New Size(112, btn.Bottom + 8)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Property Color() As Color
        Set(ByVal Value As Color)
            Dim i As Integer
            For i = 0 To grpbox.Controls.Count - 1
                Dim radiobtn As RadioButton = _
                    DirectCast(grpbox.Controls(i), RadioButton)
                If Value.Equals(Color.FromName(radiobtn.Text)) Then
                    radiobtn.Checked = True
                    Exit For
                End If
            Next i
        End Set
        Get
            Dim i As Integer
            For i = 0 To grpbox.Controls.Count - 1
                Dim radiobtn As RadioButton = _
                    DirectCast(grpbox.Controls(i), RadioButton)
                If radiobtn.Checked Then
                    Return Color.FromName(radiobtn.Text)
                End If
            Next i
            Return Color.Black
        End Get
    End Property

    Property Fill() As Boolean
        Set(ByVal Value As Boolean)
            chkbox.Checked = Value
        End Set
        Get
            Return chkbox.Checked
        End Get
    End Property
End Class
