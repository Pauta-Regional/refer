'-----------------------------------------------------
' DigitalClockWithDate.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DigitalClockWithDate
    Inherits DigitalClock

    Shared Shadows Sub Main()
        Application.Run(new DigitalClockWithDate())
    End Sub

    Sub New()
        Text &= " with Date"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim dt As DateTime = DateTime.Now
        Dim strTime As String = dt.ToString("d") & vbLf & dt.ToString("T")
        Dim szf As SizeF = grfx.MeasureString(strTime, Font)
        Dim fScale As Single = Math.Min(ClientSize.Width / szf.Width, _
                                        ClientSize.Height / szf.Height)
        Dim fnt As New Font(Font.FontFamily, fScale * Font.SizeInPoints)
        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center
        grfx.DrawString(strTime, fnt, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
