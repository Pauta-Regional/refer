'------------------------------------------------------
' HelloCenteredMeasured.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HelloCenteredMeasured
    Inherits Form

    Shared Sub Main()
        Application.Run(New HelloCenteredMeasured())
    End Sub

    Sub New()
        Text = "Hello Centered Using MeasureString"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim str As String = "Hello, world!"
        Dim szfText As SizeF = grfx.MeasureString(str, Font)

        grfx.DrawString(str, Font, New SolidBrush(ForeColor), _
                        (ClientSize.Width - szfText.Width) / 2, _
                        (ClientSize.Height - szfText.Height) / 2)
    End Sub
End Class
