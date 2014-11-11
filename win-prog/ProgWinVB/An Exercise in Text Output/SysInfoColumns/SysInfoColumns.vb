'-----------------------------------------------
' SysInfoColumns.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoColumns
    Inherits Form

    Shared Sub Main()
        Application.Run(New SysInfoColumns())
    End Sub

    Sub New()
        Text = "System Information: Columns"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim szf As SizeF = _
                grfx.MeasureString("ArrangeStartingPosition ", Font)
        Dim cxCol As Single = szf.Width
        Dim y As Single = 0
        Dim cySpace As Integer = Font.Height

        grfx.DrawString("ArrangeDirection", Font, br, 0, y)
        grfx.DrawString(SystemInformation.ArrangeDirection.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("ArrangeStartingPosition", Font, br, 0, y)
        grfx.DrawString( _
                SystemInformation.ArrangeStartingPosition.ToString(), _
                Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("BootMode", Font, br, 0, y)
        grfx.DrawString(SystemInformation.BootMode.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("Border3DSize", Font, br, 0, y)
        grfx.DrawString(SystemInformation.Border3DSize.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("BorderSize", Font, br, 0, y)
        grfx.DrawString(SystemInformation.BorderSize.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("CaptionButtonSize", Font, br, 0, y)
        grfx.DrawString(SystemInformation.CaptionButtonSize.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("CaptionHeight", Font, br, 0, y)
        grfx.DrawString(SystemInformation.CaptionHeight.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("ComputerName", Font, br, 0, y)
        grfx.DrawString(SystemInformation.ComputerName, _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("CursorSize", Font, br, 0, y)
        grfx.DrawString(SystemInformation.CursorSize.ToString(), _
                        Font, br, cxCol, y)
        y += cySpace
        grfx.DrawString("DbcsEnabled", Font, br, 0, y)
        grfx.DrawString(SystemInformation.DbcsEnabled.ToString(), _
                        Font, br, cxCol, y)
    End Sub
End Class
