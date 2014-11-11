'---------------------------------------------
' SimpleDialog.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleDialog
    Inherits Form

    Private strDisplay As String = ""

    Shared Sub Main()
        Application.Run(New SimpleDialog())
    End Sub

    Sub New()
        Text = "Simple Dialog"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Dialog!", AddressOf MenuOnClick)
    End Sub

    Sub MenuOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New SimpleDialogBox()

        dlg.ShowDialog()    ' Returns when dialog box terminated.

        strDisplay = "Dialog box terminated with " & _
                     dlg.DialogResult.ToString() & "!"
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString(strDisplay, Font, New SolidBrush(ForeColor), 0, 0)
    End Sub
End Class

Class SimpleDialogBox
    Inherits Form

    Sub New()
        Text = "Simple Dialog Box"

        ' Standard stuff for dialog boxes
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False

        ' Create OK button.
        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Location = New Point(50, 50)
        btn.Size = New Size(10 * Font.Height, 2 * Font.Height)
        AddHandler btn.Click, AddressOf ButtonOkOnClick

        ' Create Cancel button.
        btn = New Button()
        btn.Parent = Me
        btn.Text = "Cancel"
        btn.Location = New Point(50, 100)
        btn.Size = New Size(10 * Font.Height, 2 * Font.Height)
        AddHandler btn.Click, AddressOf ButtonCancelOnClick
    End Sub

    Sub ButtonOkOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        DialogResult = DialogResult.OK
    End Sub

    Sub ButtonCancelOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        DialogResult = DialogResult.Cancel
    End Sub
End Class
