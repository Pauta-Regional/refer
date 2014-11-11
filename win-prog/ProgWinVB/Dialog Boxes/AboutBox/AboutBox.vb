'-----------------------------------------
' AboutBox.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class AboutBox
    Inherits Form

    Shared Sub Main()
        Application.Run(New AboutBox())
    End Sub

    Sub New()
        Text = "About Box"
        Icon = New Icon(Me.GetType(), "AforAbout.ico")

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Help")
        Menu.MenuItems(0).MenuItems.Add("&About AboutBox...", _
                                  AddressOf MenuAboutOnClick)
    End Sub

    Sub MenuAboutOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New AboutDialogBox()
        dlg.ShowDialog()
    End Sub
End Class

Class AboutDialogBox
    Inherits Form

    Sub New()
        Text = "About AboutBox"

        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        ControlBox = False
        MaximizeBox = False
        MinimizeBox = False
        ShowInTaskbar = False

        ' Create first label with program name.
        Dim lbl1 As New Label()
        lbl1.Parent = Me
        lbl1.Text = " AboutBox Version 1.0 "
        lbl1.Font = New Font(FontFamily.GenericSerif, 24, _
                             FontStyle.Italic)
        lbl1.AutoSize = True
        lbl1.TextAlign = ContentAlignment.MiddleCenter

        ' Create picture box containing icon.
        Dim icon As New Icon(Me.GetType(), "AforAbout.ico")
        Dim picbox As New PictureBox()
        picbox.Parent = Me
        picbox.Image = icon.ToBitmap()
        picbox.SizeMode = PictureBoxSizeMode.AutoSize
        picbox.Location = New Point(lbl1.Font.Height \ 2, _
                                    lbl1.Font.Height \ 2)

        lbl1.Location = New Point(picbox.Right, lbl1.Font.Height \ 2)

        Dim iClientWidth As Integer = lbl1.Right

        ' Create second label with copyright and link.
        Dim lnklbl2 As New LinkLabel()
        lnklbl2.Parent = Me
        lnklbl2.Text = Chr(169) & " 2002 by Charles Petzold"
        lnklbl2.Font = New Font(FontFamily.GenericSerif, 16)
        lnklbl2.Location = New Point(0, lbl1.Bottom + lnklbl2.Font.Height)
        lnklbl2.Size = New Size(iClientWidth, lnklbl2.Font.Height)
        lnklbl2.TextAlign = ContentAlignment.MiddleCenter

        ' Set link area and event handler.
        lnklbl2.LinkArea = New LinkArea(10, 15)
        AddHandler lnklbl2.LinkClicked, AddressOf LinkLabelOnLinkClicked

        ' Create OK button.
        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "OK"
        btn.Size = New Size(4 * btn.Font.Height, _
                            2 * btn.Font.Height)
        btn.Location = New Point((iClientWidth - btn.Size.Width) \ 2, _
                                 lnklbl2.Bottom + 2 * btn.Font.Height)
        btn.DialogResult = DialogResult.OK
        btn.TabIndex = 0

        CancelButton = btn
        AcceptButton = btn
        ClientSize = New Size(iClientWidth, _
                              btn.Bottom + 2 * btn.Font.Height)
    End Sub

    Private Sub LinkLabelOnLinkClicked(ByVal obj As Object, _
            ByVal lllcea As LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://www.charlespetzold.com")
    End Sub
End Class
