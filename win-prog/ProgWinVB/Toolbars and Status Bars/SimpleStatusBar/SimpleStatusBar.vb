'------------------------------------------------
' SimpleStatusBar.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleStatusBar
    Inherits Form

    Shared Sub Main()
        Application.Run(New SimpleStatusBar())
    End Sub

    Sub New()
        Text = "Simple Status Bar"
        ResizeRedraw = True

        ' Create status bar.
        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.Text = "My initial status bar text"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)

        grfx.DrawLine(pn, 0, 0, ClientSize.Width, ClientSize.Height)
        grfx.DrawLine(pn, ClientSize.Width, 0, 0, ClientSize.Height)
    End Sub
End Class
