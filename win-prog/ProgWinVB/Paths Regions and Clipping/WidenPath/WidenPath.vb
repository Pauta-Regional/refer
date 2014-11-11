'------------------------------------------
' WidenPath.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class WidenPath
    Inherits PrintableForm

    Private path As GraphicsPath

    Shared Shadows Sub Main()
        Application.Run(New WidenPath())
    End Sub

    Sub New()
        Text = "Widen Path"

        path = New GraphicsPath()
        ' Create open subpath.
        path.AddLines(New Point() {New Point(20, 10), _
                                   New Point(50, 50), _
                                   New Point(80, 10)})
        ' Create closed subpath.
        path.AddPolygon(New Point() {New Point(20, 30), _
                                     New Point(50, 70), _
                                     New Point(80, 30)})
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.ScaleTransform(cx / 300.0F, cy / 200.0F)
        Dim i As Integer

        For i = 0 To 5
            Dim pathClone As GraphicsPath = _
                                DirectCast(path.Clone(), GraphicsPath)
            Dim matx As New Matrix()
            Dim pnThin As New Pen(clr, 1)
            Dim pnThick As New Pen(clr, 5)
            Dim pnWiden As New Pen(clr, 7.5F)
            Dim br As New SolidBrush(clr)

            matx.Translate((i Mod 3) * 100, (i \ 3) * 100)

            If i < 3 Then
                pathClone.Transform(matx)
            Else
                pathClone.Widen(pnWiden, matx)
            End If

            Select Case i Mod 3
                Case 0
                    grfx.DrawPath(pnThin, pathClone)
                Case 1
                    grfx.DrawPath(pnThick, pathClone)
                Case 2
                    grfx.FillPath(br, pathClone)
            End Select
        Next i
    End Sub
End Class
