'-----------------------------------------------
' MatrixElements.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class MatrixElements
    Inherits Form

    Private matx As Matrix
    Private btnUpdate As Button
    Private updown(5) As NumericUpDown

    Event Changed As EventHandler

    Sub New()
        Text = "Matrix Elements"
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False

        Dim strLabel() As String = {"X Scale:", "Y Shear:", _
                                    "X Shear:", "Y Scale:", _
                                    "X Translate:", "Y Translate:"}
        Dim i As Integer

        For i = 0 To 5
            Dim lbl As New Label()
            lbl.Parent = Me
            lbl.Text = strLabel(i)
            lbl.Location = New Point(8, 8 + 16 * i)
            lbl.Size = New Size(64, 8)

            updown(i) = New NumericUpDown()
            updown(i).Parent = Me
            updown(i).Location = New Point(76, 8 + 16 * i)
            updown(i).Size = New Size(48, 12)
            updown(i).TextAlign = HorizontalAlignment.Right
            AddHandler updown(i).ValueChanged, AddressOf UpDownOnValueChanged
            updown(i).DecimalPlaces = 2
            updown(i).Increment = 0.1D
            updown(i).Minimum = Decimal.MinValue
            updown(i).Maximum = Decimal.MaxValue
        Next i

        btnUpdate = New Button()
        btnUpdate.Parent = Me
        btnUpdate.Text = "Update"
        btnUpdate.Location = New Point(8, 108)
        btnUpdate.Size = New Size(50, 16)
        AddHandler btnUpdate.Click, AddressOf ButtonUpdateOnClick
        AcceptButton = btnUpdate

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "Methods..."
        btn.Location = New Point(76, 108)
        btn.Size = New Size(50, 16)
        AddHandler btn.Click, AddressOf ButtonMethodsOnClick

        ClientSize = New Size(134, 132)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Property Matrix() As Matrix
        Set(ByVal Value As Matrix)
            matx = Value
            Dim i As Integer
            For i = 0 To 5
                updown(i).Value = CDec(Value.Elements(i))
            Next i
        End Set
        Get
            matx = New Matrix(CSng(updown(0).Value), _
                              CSng(updown(1).Value), _
                              CSng(updown(2).Value), _
                              CSng(updown(3).Value), _
                              CSng(updown(4).Value), _
                              CSng(updown(5).Value))
            Return matx
        End Get
    End Property

    Private Sub UpDownOnValueChanged(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        Dim boolEnableButton As Boolean = True

        Try
            grfx.Transform = Matrix
        Catch
            boolEnableButton = False
        End Try

        btnUpdate.Enabled = boolEnableButton
        grfx.Dispose()
    End Sub

    Private Sub ButtonUpdateOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        RaiseEvent Changed(Me, EventArgs.Empty)
    End Sub

    Private Sub ButtonMethodsOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim dlg As New MatrixMethods()
        dlg.Matrix = Matrix
        If dlg.ShowDialog() = DialogResult.OK Then
            Matrix = dlg.Matrix
            btnUpdate.PerformClick()
        End If
    End Sub
End Class
