'---------------------------------------------
' MouseConnect.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MouseConnect
    Inherits Form

    Const iMaxPoint As Integer = 1000
    Private iNumPoints As Integer = 0
    Private apt(iMaxPoint) As Point

    Shared Sub Main()
        Application.Run(New MouseConnect())
    End Sub

    Sub New()
        Text = "Mouse Connect: Press, drag quickly, release"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Double the client area.
        ClientSize = Size.op_Addition(ClientSize, ClientSize)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button = MouseButtons.Left Then
            iNumPoints = 0
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If mea.Button = MouseButtons.Left And iNumPoints <= iMaxPoint Then
            apt(iNumPoints) = New Point(mea.X, mea.Y)
            iNumPoints += 1

            Dim grfx As Graphics = CreateGraphics()
            grfx.DrawLine(New Pen(ForeColor), mea.X, mea.Y, _
                                              mea.X, mea.Y + 1)
            grfx.Dispose()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        If mea.Button = MouseButtons.Left Then Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)
        Dim i, j As Integer

        For i = 0 To iNumPoints - 2
            For j = i + 1 To iNumPoints - 1
                grfx.DrawLine(pn, apt(i), apt(j))
            Next j
        Next i
    End Sub
End Class
