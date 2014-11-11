'------------------------------------------------
' SysInfoFirstTry.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoFirstTry
    Inherits Form

    Shared Sub Main()
        Application.Run(New SysInfoFirstTry())
    End Sub

    Sub New()
        Text = "System Information: First Try"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim y As Integer = 0

        grfx.DrawString("ArrangeDirection: " & _
                SystemInformation.ArrangeDirection.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("ArrangeStartingPosition: " & _
                SystemInformation.ArrangeStartingPosition.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("BootMode: " & _
                SystemInformation.BootMode.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("Border3DSize: " & _
                SystemInformation.Border3DSize.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("BorderSize: " & _
                SystemInformation.BorderSize.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("CaptionButtonSize: " & _
                SystemInformation.CaptionButtonSize.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("CaptionHeight: " & _
                SystemInformation.CaptionHeight.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("ComputerName: " & _
                SystemInformation.ComputerName.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("CursorSize: " & _
                SystemInformation.CursorSize.ToString(), _
                Font, br, 0, y)

        y += Font.Height
        grfx.DrawString("DbcsEnabled: " & _
                SystemInformation.DbcsEnabled.ToString(), _
                Font, br, 0, y)
    End Sub
End Class
