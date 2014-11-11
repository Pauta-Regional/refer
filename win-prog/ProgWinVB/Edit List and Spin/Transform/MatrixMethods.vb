'----------------------------------------------
' MatrixMethods.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class MatrixMethods
    Inherits Form

    Private matx As Matrix
    Private btnInvert As Button
    Private updown(2) As NumericUpDown
    Private radbtn(1) As RadioButton

    Sub New()
        Text = "Matrix Methods"
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        Location = Point.op_Addition(ActiveForm.Location, _
                   Size.op_Addition(SystemInformation.CaptionButtonSize, _
                                    SystemInformation.FrameBorderSize))

        Dim astrLabel() As String = {"X / DX:", "Y / DY:", "Angle:"}
        Dim i As Integer

        For i = 0 To 2
            Dim lbl As New Label()
            lbl.Parent = Me
            lbl.Text = astrLabel(i)
            lbl.Location = New Point(8, 8 + 16 * i)
            lbl.Size = New Size(32, 8)

            updown(i) = New NumericUpDown()
            updown(i).Parent = Me
            updown(i).Location = New Point(40, 8 + 16 * i)
            updown(i).Size = New Size(48, 12)
            updown(i).TextAlign = HorizontalAlignment.Right
            updown(i).DecimalPlaces = 2
            updown(i).Increment = 0.1D
            updown(i).Minimum = Decimal.MinValue
            updown(i).Maximum = Decimal.MaxValue
        Next i

        ' Create group box and radio buttons.
        Dim grpbox As New GroupBox()
        grpbox.Parent = Me
        grpbox.Text = "Order"
        grpbox.Location = New Point(8, 60)
        grpbox.Size = New Size(80, 32)

        For i = 0 To 1
            radbtn(i) = New RadioButton()
            radbtn(i).Parent = grpbox
            radbtn(i).Text = New String() {"Prepend", "Append"}(i)
            radbtn(i).Location = New Point(8, 8 + 12 * i)
            radbtn(i).Size = New Size(50, 10)
            radbtn(i).Checked = (i = 0)
        Next i

        ' Create 8 buttons for terminating dialog.
        Dim astrButton() As String = {"Reset", "Invert", "Translate", _
                                      "Scale", "Rotate", "RotateAt", _
                                      "Shear", "Cancel"}
        Dim aeh() As EventHandler = {AddressOf ButtonResetOnClick, _
                                     AddressOf ButtonInvertOnClick, _
                                     AddressOf ButtonTranslateOnClick, _
                                     AddressOf ButtonScaleOnClick, _
                                     AddressOf ButtonRotateOnClick, _
                                     AddressOf ButtonRotateAtOnClick, _
                                     AddressOf ButtonShearOnClick}
        For i = 0 To 7
            Dim btn As New Button()
            btn.Parent = Me
            btn.Text = astrButton(i)
            btn.Location = New Point(100 + 72 * (i \ 4),8 + 24 * (i Mod 4))
            btn.Size = New Size(64, 14)

            If i = 0 Then AcceptButton = btn
            If i = 1 Then btnInvert = btn

            If i < 7 Then
                AddHandler btn.Click, aeh(i)
                btn.DialogResult = DialogResult.OK
            Else
                btn.DialogResult = DialogResult.Cancel
                CancelButton = btn
            End If
        Next i

        ClientSize = New Size(240, 106)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Property Matrix() As Matrix
        Set(ByVal Value As Matrix)
            matx = Value
            btnInvert.Enabled = Matrix.IsInvertible
        End Set
        Get
            Return matx
        End Get
    End Property

    Private Sub ButtonResetOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        Matrix.Reset()
    End Sub

    Private Sub ButtonInvertOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Matrix.Invert()
    End Sub

    Private Sub ButtonTranslateOnClick(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        Matrix.Translate(CSng(updown(0).Value), _
                         CSng(updown(1).Value), PrependOrAppend)
    End Sub

    Private Sub ButtonScaleOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        Matrix.Scale(CSng(updown(0).Value), _
                     CSng(updown(1).Value), PrependOrAppend)
    End Sub

    Private Sub ButtonRotateOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Matrix.Rotate(CSng(updown(2).Value), PrependOrAppend)
    End Sub

    Private Sub ButtonRotateAtOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        Matrix.RotateAt(CSng(updown(2).Value), _
                        New PointF(CSng(updown(0).Value), _
                                   CSng(updown(1).Value)), PrependOrAppend)
    End Sub

    Private Sub ButtonShearOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        Matrix.Shear(CSng(updown(0).Value), _
                     CSng(updown(1).Value), PrependOrAppend)
    End Sub

    Private Function PrependOrAppend() As MatrixOrder
        If radbtn(0).Checked Then
            Return MatrixOrder.Prepend
        Else
            Return MatrixOrder.Append
        End If
    End Function
End Class
