'--------------------------------------------
' CaptureLoss.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CaptureLoss
    Inherits Form

    Shared Sub Main()
        Application.Run(new CaptureLoss())
    End Sub

    Sub New()
        Text = "Capture Loss"
    End Sub

    Protected Overrides Sub WndProc(ByRef msg As Message)
        ' Process WM_CAPTURECHANGED message.
        If msg.Msg = 533 Then Invalidate()
        MyBase.WndProc(msg)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        If Capture Then
            grfx.FillRectangle(Brushes.Red, ClientRectangle)
        Else
            grfx.FillRectangle(Brushes.Gray, ClientRectangle)
        End If
    End Sub
End Class
