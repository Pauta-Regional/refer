'-----------------------------------------
' DualWink.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DualWink
    Inherits Wink

    Private aimgRev(3) As Image

    Shared Shadows Sub Main()
        Application.Run(New DualWink())
    End Sub

    Sub New()
        Text = "Dual " & Text

        Dim i As Integer
        For i = 0 To 3
            aimgRev(i) = DirectCast(aimg(i).Clone(), Image)
            aimgRev(i).RotateFlip(RotateFlipType.RotateNoneFlipX)
        Next i
    End Sub

    Protected Overrides Sub TimerOnTick(ByVal obj As Object, _
                                        ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()

        grfx.DrawImage(aimg(iImage), _
                ClientSize.Width \ 2, _
                (ClientSize.Height - aimg(iImage).Height) \ 2, _
                aimg(iImage).Width, aimg(iImage).Height)

        grfx.DrawImage(aimgRev(3 - iImage), _
                ClientSize.Width \ 2 - aimgRev(3 - iImage).Width, _
                (ClientSize.Height - aimgRev(3 - iImage).Height) \ 2, _
                aimgRev(3 - iImage).Width, _
                aimgRev(3 - iImage).Height)
        grfx.Dispose()

        iImage += iIncr
        If iImage = 3 Then
            iIncr = -1
        ElseIf iImage = 0 Then
            iIncr = 1
        End If
    End Sub
End Class
