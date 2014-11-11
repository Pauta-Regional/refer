'----------------------------------------
' ExitOnX.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ExitOnX
    Inherits Form

    Shared Sub Main()
        Application.Run(new ExitOnX())
    End Sub

    Sub New()
        Text = "Exit on X"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        If kea.KeyCode = Keys.X Then Close()
    End Sub
End Class
