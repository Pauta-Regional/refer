'---------------------------------------------------
' TwoStatusBarPanels.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TwoStatusBarPanels
    Inherits Form

    Shared Sub Main()
        Application.Run(new TwoStatusBarPanels())
    End Sub

    Sub New()
        Text = "Two Status Bar Panels"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.ShowPanels = True

        Dim sbp1 As New StatusBarPanel()
        sbp1.Text = "Panel 1"

        Dim sbp2 As New StatusBarPanel()
        sbp2.Text = "Panel 2"

        sbar.Panels.Add(sbp1)
        sbar.Panels.Add(sbp2)
    End Sub
End Class
