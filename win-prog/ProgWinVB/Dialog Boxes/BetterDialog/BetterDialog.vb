'---------------------------------------------
' BetterDialog.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BetterDialog
    Inherits Form

    Private strDisplay As String = ""

    Shared Sub Main()
        Application.Run(New BetterDialog())
    End Sub

    Sub New()
        Text = "Better Dialog"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Dialog!", AddressOf MenuOnClick)
    End Sub

    Sub MenuOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New BetterDialogBox()
        Dim dr As DialogResult = dlg.ShowDialog()

        strDisplay = "Dialog box terminated with " & dr.ToString() & "!"
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString(strDisplay, Font, New SolidBrush(ForeColor), 0, 0)
    End Sub
End Class

Class BetterDialogBox
    Inherits Form

    Sub New()
        Text = "Better Dialog Box"

        ' Standard stuff for dialog boxes
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Location = Point.op_Addition(ActiveForm.Location, _
                   Size.op_Addition(SystemInformation.CaptionButtonSize, _
                                    SystemInformation.FrameBorderSize))
        ' Create OK button.
        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Location = New Point(50, 50)
        btn.Size = New Size(10 * Font.Height, 2 * Font.Height)
        btn.DialogResult = DialogResult.OK
        AcceptButton = btn

        ' Create Cancel button.
        btn = New Button()
        btn.Parent = Me
        btn.Text = "Cancel"
        btn.Location = New Point(50, 100)
        btn.Size = New Size(10 * Font.Height, 2 * Font.Height)
        btn.DialogResult = DialogResult.Cancel
        CancelButton = btn
    End Sub
End Class
