'----------------------------------------
' Checker.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Checker
    Inherits Form

    Protected Const xNum As Integer = 5     ' Number of boxes horizontally
    Protected Const yNum As Integer = 4     ' Number of boxes vertically
    Protected abChecked(yNum, xNum) As Boolean
    Protected cxBlock, cyBlock As Integer

    Shared Sub Main()
        Application.Run(New Checker())
    End Sub

    Sub New()
        Text = "Checker"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)      ' Or else ResizeRedraw doesn't work
        cxBlock = ClientSize.Width \ xNum
        cyBlock = ClientSize.Height \ yNum
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        Dim x As Integer = mea.X \ cxBlock
        Dim y As Integer = mea.Y \ cyBlock

        If x < xNum AndAlso y < yNum Then
            abChecked(y, x) = Not abChecked(y, x)
            Invalidate(New Rectangle(x * cxBlock, y * cyBlock, _
                                     cxBlock, cyBlock))
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim x, y As Integer
        Dim pn As New Pen(ForeColor)

        For y = 0 To yNum - 1
            For x = 0 To xNum - 1
                grfx.DrawRectangle(pn, x * cxBlock, y * cyBlock, _
                                   cxBlock, cyBlock)
                If abChecked(y, x) Then
                    grfx.DrawLine(pn, x * cxBlock, y * cyBlock, _
                                  (x + 1) * cxBlock, (y + 1) * cyBlock)
                    grfx.DrawLine(pn, x * cxBlock, (y + 1) * cyBlock, _
                                  (x + 1) * cxBlock, y * cyBlock)
                End If
            Next x
        Next y
    End Sub
End Class
