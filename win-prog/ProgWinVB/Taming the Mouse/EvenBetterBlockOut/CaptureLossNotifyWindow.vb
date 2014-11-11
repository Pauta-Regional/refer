'--------------------------------------------------------
' CaptureLossNotifyWindow.vb (c) 2002 by Charles Petzold
'--------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Interface ICaptureLossNotify
    Sub OnLostCapture()
End Interface

Class CaptureLossNotifyWindow
    Inherits NativeWindow

    Public ctrl As ICaptureLossNotify

    Protected Overrides Sub WndProc(ByRef message As Message)
        ' Process WM_CAPTURECHANGED message.
        If message.Msg = 533 Then ctrl.OnLostCapture()
        MyBase.WndProc(message)
    End Sub
End Class
