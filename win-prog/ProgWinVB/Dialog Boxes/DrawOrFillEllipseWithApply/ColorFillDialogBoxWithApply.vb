'------------------------------------------------------------
' ColorFillDialogBoxWithApply.vb (c) 2002 by Charles Petzold
'------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ColorFillDialogBoxWithApply
    Inherits ColorFillDialogBox

    Private btnApply As Button

    Event Apply As EventHandler

    Sub New()
        grpbox.Location = New Point(36, 8)
        chkbox.Location = New Point(36, grpbox.Bottom + 4)

        btnApply = New Button()
        btnApply.Parent = Me
        btnApply.Enabled = False
        btnApply.Text = "Apply"
        btnApply.Location = New Point(120, chkbox.Bottom + 4)
        btnApply.Size = New Size(40, 16)
        AddHandler btnApply.Click, AddressOf ButtonApplyOnClick

        ClientSize = New Size(168, btnApply.Bottom + 8)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Property ShowApply() As Boolean
        Set(ByVal Value As Boolean)
            btnApply.Enabled = Value
        End Set
        Get
            Return btnApply.Enabled
        End Get
    End Property

    Sub ButtonApplyOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        RaiseEvent Apply(Me, EventArgs.Empty)
    End Sub
End Class
