'----------------------------------------------
' XMarksTheSpot.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class XMarksTheSpot
    Inherits Form

    Shared Sub Main()
        Application.Run(New XMarksTheSpot())
    End Sub

    Sub New()
        Text = "X Marks The Spot"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)

        grfx.DrawLine(pn, 0, 0, _
                      ClientSize.Width - 1, ClientSize.Height - 1)
        grfx.DrawLine(pn, 0, ClientSize.Height - 1, _
                      ClientSize.Width - 1, 0)
    End Sub
End Class
