'-------------------------------------
' Wink.vb (c) 2002 by Charles Petzold
'-------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class Wink
    Inherits Form

    Protected aimg(3) As Image
    Protected iImage As Integer = 0
    Protected iIncr As Integer = 1

    Shared Sub Main()
        Application.Run(New Wink())
    End Sub

    Sub New()
        Text = "Wink"
        ResizeRedraw = True
        BackColor = Color.White

        Dim i As Integer
        For i = 0 To 3
            aimg(i) = New Bitmap(Me.GetType(), "Eye" & (i + 1) & ".png")
        Next i

        Dim tmr As New Timer()
        tmr.Interval = 100
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Enabled = True
    End Sub

    Protected Overridable Sub TimerOnTick(ByVal obj As Object, _
                                          ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        grfx.DrawImage(aimg(iImage), _
                       (ClientSize.Width - aimg(iImage).Width) \ 2, _
                       (ClientSize.Height - aimg(iImage).Height) \ 2, _
                       aimg(iImage).Width, aimg(iImage).Height)
        grfx.Dispose()
        iImage += iIncr

        If iImage = 3 Then
            iIncr = -1
        ElseIf iImage = 0 Then
            iIncr = 1
        End If
    End Sub
End Class
