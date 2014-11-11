'------------------------------------------
' AntiAlias.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class AntiAlias
    Inherits Form

    Shared Sub Main()
        Application.Run(New AntiAlias())
    End Sub

    Sub New()
        Text = "Anti-Alias Demo"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)

        grfx.SmoothingMode = SmoothingMode.None
        grfx.PixelOffsetMode = PixelOffsetMode.Default

        grfx.DrawLine(pn, 2, 2, 18, 10)
    End Sub
End Class
