'----------------------------------------------
' PrintableForm.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class PrintableForm
    Inherits Form

    Shared Sub Main()
        Application.Run(New PrintableForm())
    End Sub

    Sub New()
        Text = "Printable Form"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        DoPage(pea.Graphics, ForeColor, _
              ClientSize.Width, ClientSize.Height)
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
        Dim szf As SizeF = grfx.VisibleClipBounds.Size
        DoPage(grfx, Color.Black, CInt(szf.Width), CInt(szf.Height))
    End Sub

    Protected Overridable Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr)
        grfx.DrawLine(pn, 0, 0, cx - 1, cy - 1)
        grfx.DrawLine(pn, cx - 1, 0, 0, cy - 1)
    End Sub
End Class
