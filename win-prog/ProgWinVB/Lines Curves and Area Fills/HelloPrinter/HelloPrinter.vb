'---------------------------------------------
' HelloPrinter.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class HelloPrinter
    Inherits Form

    Shared Sub Main()
        Application.Run(New HelloPrinter())
    End Sub

    Sub New()
        Text = "Hello Printer!"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim strfmt As New StringFormat()

        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center
        grfx.DrawString("Click to print", Font, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub

    Protected Overrides Sub OnClick(ByVal ea As EventArgs)
        Dim prndoc As New PrintDocument()
        prndoc.DocumentName = Text
        AddHandler prndoc.PrintPage, AddressOf PrintDocumentOnPrintPage
        prndoc.Print()
    End Sub

    Private Sub PrintDocumentOnPrintPage(ByVal obj As Object, _
                                         ByVal ppea As PrintPageEventArgs)
        Dim grfx As Graphics = ppea.Graphics
        grfx.DrawString(Text, Font, Brushes.Black, 0, 0)

        Dim szf As SizeF = grfx.MeasureString(Text, Font)
        grfx.DrawLine(Pens.Black, szf.ToPointF(), _
                             grfx.VisibleClipBounds.Size.ToPointF())
    End Sub
End Class
