'-------------------------------------------------------
' HelloCenteredRectangle.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HelloCenteredRectangle
    Inherits Form

    Shared Sub Main()
        Application.Run(New HelloCenteredRectangle())
    End Sub

    Sub New()
        Text = "Hello Centered Using Rectangle"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim strfmt As New StringFormat()

        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        grfx.DrawString("Hello, world!", Font, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
