'--------------------------------------------------
' FindReplaceDialog.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FindDialog
    Inherits FindReplaceDialog

    Sub New()
        Text = "Find"
        lblReplace.Visible = False
        txtboxReplace.Visible = False
        btnReplace.Visible = False
        btnReplaceAll.Visible = False
        btnCancel.Location = btnReplace.Location
    End Sub
End Class

Class ReplaceDialog
    Inherits FindReplaceDialog

    Sub New()
        Text = "Replace"
        grpboxDirection.Visible = False
    End Sub
End Class

MustInherit Class FindReplaceDialog
    Inherits Form

    ' Fields
    Protected lblFind, lblReplace As Label
    Protected txtboxFind, txtboxReplace As TextBox
    Protected chkboxMatchCase As CheckBox
    Protected grpboxDirection As GroupBox
    Protected radbtnUp, radbtnDown As RadioButton
    Protected btnFindNext, btnReplace, btnReplaceAll, btnCancel As Button

    ' Public events
    Event FindNext As EventHandler
    Event Replace As EventHandler
    Event ReplaceAll As EventHandler
    Event CloseDlg As EventHandler

    ' Constructor
    Sub New()
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Location = Point.op_Addition(ActiveForm.Location, _
                   Size.op_Addition(SystemInformation.CaptionButtonSize, _
                                    SystemInformation.FrameBorderSize))

        lblFind = New Label()
        lblFind.Parent = Me
        lblFind.Text = "Fi&nd what:"
        lblFind.Location = New Point(8, 8)
        lblFind.Size = New Size(64, 8)

        txtboxFind = New TextBox()
        txtboxFind.Parent = Me
        txtboxFind.Location = New Point(72, 8)
        txtboxFind.Size = New Size(136, 8)
        AddHandler txtboxFind.TextChanged, _
                                AddressOf TextBoxFindOnTextChanged

        lblReplace = New Label()
        lblReplace.Parent = Me
        lblReplace.Text = "Re&place with:"
        lblReplace.Location = New Point(8, 24)
        lblReplace.Size = New Size(64, 8)

        txtboxReplace = New TextBox()
        txtboxReplace.Parent = Me
        txtboxReplace.Location = New Point(72, 24)
        txtboxReplace.Size = New Size(136, 8)

        chkboxMatchCase = New CheckBox()
        chkboxMatchCase.Parent = Me
        chkboxMatchCase.Text = "Match &case"
        chkboxMatchCase.Location = New Point(8, 50)
        chkboxMatchCase.Size = New Size(64, 8)

        grpboxDirection = New GroupBox()
        grpboxDirection.Parent = Me
        grpboxDirection.Text = "Direction"
        grpboxDirection.Location = New Point(100, 40)
        grpboxDirection.Size = New Size(96, 24)

        radbtnUp = New RadioButton()
        radbtnUp.Parent = grpboxDirection
        radbtnUp.Text = "&Up"
        radbtnUp.Location = New Point(8, 8)
        radbtnUp.Size = New Size(32, 12)

        radbtnDown = New RadioButton()
        radbtnDown.Parent = grpboxDirection
        radbtnDown.Text = "&Down"
        radbtnDown.Location = New Point(40, 8)
        radbtnDown.Size = New Size(40, 12)

        btnFindNext = New Button()
        btnFindNext.Parent = Me
        btnFindNext.Text = "&Find Next"
        btnFindNext.Enabled = False
        btnFindNext.Location = New Point(216, 4)
        btnFindNext.Size = New Size(64, 16)
        AddHandler btnFindNext.Click, AddressOf ButtonFindNextOnClick

        btnReplace = New Button()
        btnReplace.Parent = Me
        btnReplace.Text = "&Replace"
        btnReplace.Enabled = False
        btnReplace.Location = New Point(216, 24)
        btnReplace.Size = New Size(64, 16)
        AddHandler btnReplace.Click, AddressOf ButtonReplaceOnClick

        btnReplaceAll = New Button()
        btnReplaceAll.Parent = Me
        btnReplaceAll.Text = "Replace &All"
        btnReplaceAll.Enabled = False
        btnReplaceAll.Location = New Point(216, 44)
        btnReplaceAll.Size = New Size(64, 16)
        AddHandler btnReplaceAll.Click, AddressOf ButtonReplaceAllOnClick

        btnCancel = New Button()
        btnCancel.Parent = Me
        btnCancel.Text = "Cancel"
        btnCancel.Location = New Point(216, 64)
        btnCancel.Size = New Size(64, 16)
        AddHandler btnCancel.Click, AddressOf ButtonCancelOnClick
        CancelButton = btnCancel

        ClientSize = New Size(288, 84)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    ' Properties
    Property FindText() As String
        Set(ByVal Value As String)
            txtboxFind.Text = Value
        End Set
        Get
            Return txtboxFind.Text
        End Get
    End Property

    Property ReplaceText() As String
        Set(ByVal Value As String)
            txtboxReplace.Text = Value
        End Set
        Get
            Return txtboxReplace.Text
        End Get
    End Property

    Property MatchCase() As Boolean
        Set(ByVal Value As Boolean)
            chkboxMatchCase.Checked = Value
        End Set
        Get
            Return chkboxMatchCase.Checked
        End Get
    End Property

    Property FindDown() As Boolean
        Set(ByVal Value As Boolean)
            If Value Then
                radbtnDown.Checked = True
            Else
                radbtnUp.Checked = True
            End If
        End Set
        Get
            Return radbtnDown.Checked
        End Get
    End Property

    ' Event handlers
    Private Sub TextBoxFindOnTextChanged(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        btnReplace.Enabled = txtboxFind.Text.Length > 0
        btnFindNext.Enabled = btnReplace.Enabled
        btnReplaceAll.Enabled = btnReplace.Enabled
    End Sub

    Private Sub ButtonFindNextOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        RaiseEvent FindNext(Me, EventArgs.Empty)
    End Sub

    Private Sub ButtonReplaceOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        RaiseEvent Replace(Me, EventArgs.Empty)
    End Sub

    Private Sub ButtonReplaceAllOnClick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        RaiseEvent ReplaceAll(Me, EventArgs.Empty)
    End Sub

    Private Sub ButtonCancelOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        RaiseEvent CloseDlg(Me, EventArgs.Empty)
        Close()
    End Sub
End Class
