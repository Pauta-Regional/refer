'--------------------------------------------
' FourCorners.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FourCorners
    Inherits Form

    Shared Sub Main()
        Application.Run(New FourCorners())
    End Sub

    Sub New()
        Text = "Four Corners Text Alignment"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim strfmt As New StringFormat()

        strfmt.Alignment = StringAlignment.Near
        strfmt.LineAlignment = StringAlignment.Near
        grfx.DrawString("Upper left corner", Font, br, 0, 0, strfmt)

        strfmt.Alignment = StringAlignment.Far
        strfmt.LineAlignment = StringAlignment.Near
        grfx.DrawString("Upper right corner", Font, br, _
                        ClientSize.Width, 0, strfmt)

        strfmt.Alignment = StringAlignment.Near
        strfmt.LineAlignment = StringAlignment.Far
        grfx.DrawString("Lower left corner", Font, br, _
                        0, ClientSize.Height, strfmt)

        strfmt.Alignment = StringAlignment.Far
        strfmt.LineAlignment = StringAlignment.Far
        grfx.DrawString("Lower right corner", Font, br, _
                        ClientSize.Width, ClientSize.Height, strfmt)
    End Sub
End Class
