'-------------------------------------------------------
' StatusBarAndAutoScroll.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class StatusBarAndAutoScroll
    Inherits Form

    Shared Sub Main()
        Application.Run(New StatusBarAndAutoScroll())
    End Sub

    Sub New()
        Text = "Status Bar and Auto-Scroll"
        AutoScroll = True

        ' Create status bar.
        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.Text = "My initial status bar text"

        ' Create labels as children of the form.
        Dim lbl As New Label()
        lbl.Parent = Me
        lbl.Text = "Upper left"
        lbl.Location = New Point(0, 0)
        lbl.AutoSize = True

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = "Lower right"
        lbl.Location = New Point(250, 250)
        lbl.AutoSize = True
    End Sub
End Class
