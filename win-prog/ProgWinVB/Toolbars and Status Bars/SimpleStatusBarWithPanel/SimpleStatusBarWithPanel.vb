'---------------------------------------------------------
' SimpleStatusBarWithPanel.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleStatusBarWithPanel
    Inherits Form

    Shared Sub Main()
        Application.Run(New SimpleStatusBarWithPanel())
    End Sub

    Sub New()
        Text = "Simple Status Bar with Panel"

        ' Create panel.
        Dim pnl As New Panel()
        pnl.Parent = Me
        pnl.BackColor = SystemColors.Window
        pnl.ForeColor = SystemColors.WindowText
        pnl.AutoScroll = True
        pnl.Dock = DockStyle.Fill
        pnl.BorderStyle = BorderStyle.Fixed3D

        ' Create status bar as child of form.
        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.Text = "My initial status bar text"

        ' Create labels as children of panel.
        Dim lbl As New Label()
        lbl.Parent = pnl
        lbl.Text = "Upper left"
        lbl.Location = New Point(0, 0)
        lbl.AutoSize = True

        lbl = New Label()
        lbl.Parent = pnl
        lbl.Text = "Lower right"
        lbl.Location = New Point(250, 250)
        lbl.AutoSize = True
    End Sub
End Class
