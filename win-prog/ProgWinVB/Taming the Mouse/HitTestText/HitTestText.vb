'--------------------------------------------
' HitTestText.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HitTestText
    Inherits TypeAway

    Shared Shadows Sub Main()
        Application.Run(New HitTestText())
    End Sub

    Sub New()
        Text &= " with Hit-Testing"
        Cursor = Cursors.IBeam
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If strText.Length = 0 Then Return

        Dim grfx As Graphics = CreateGraphics()
        Dim xPrev As Single = 0
        Dim i As Integer

        For i = 0 To strText.Length - 1
            Dim szf As SizeF = _
                    grfx.MeasureString(strText.Substring(0, i + 1), _
                                       Font, PointF.Empty, _
                                       StringFormat.GenericTypographic)

            If Math.Abs(mea.X - xPrev) < Math.Abs(mea.X - szf.Width) Then
                Exit For
            End If

            xPrev = szf.Width
        Next i

        iInsert = i
        grfx.Dispose()
        PositionCaret()
    End Sub
End Class
