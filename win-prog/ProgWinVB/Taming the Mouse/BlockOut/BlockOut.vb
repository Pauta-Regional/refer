'-----------------------------------------
' BlockOut.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BlockOut
    Inherits Form

    Private bBlocking, bValidBox As Boolean
    Private ptBeg, ptEnd As Point
    Private rectBox As Rectangle

    Shared Sub Main()
        Application.Run(New BlockOut())
    End Sub

    Sub New()
        Text = "Blockout Rectangle with Mouse"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button = MouseButtons.Left Then
            ptBeg = New Point(mea.X, mea.Y)
            ptEnd = ptBeg
            Dim grfx As Graphics = CreateGraphics()
            grfx.DrawRectangle(New Pen(ForeColor), Rect(ptBeg, ptEnd))
            grfx.Dispose()
            bBlocking = True
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If bBlocking Then
            Dim grfx As Graphics = CreateGraphics()
            grfx.DrawRectangle(New Pen(BackColor), Rect(ptBeg, ptEnd))
            ptEnd = New Point(mea.X, mea.Y)
            grfx.DrawRectangle(New Pen(ForeColor), Rect(ptBeg, ptEnd))
            grfx.Dispose()
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        If bBlocking AndAlso mea.Button = MouseButtons.Left Then
            Dim grfx As Graphics = CreateGraphics()
            rectBox = Rect(ptBeg, New Point(mea.X, mea.Y))
            grfx.DrawRectangle(New Pen(ForeColor), rectBox)
            grfx.Dispose()
            bBlocking = False
            bValidBox = True
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        If bValidBox Then
            grfx.FillRectangle(New SolidBrush(ForeColor), rectBox)
        End If
        If bBlocking Then
            grfx.DrawRectangle(New Pen(ForeColor), Rect(ptBeg, ptEnd))
        End If
    End Sub

    Private Function Rect(ByVal ptBeg As Point, _
                          ByVal ptEnd As Point) As Rectangle
        Return New Rectangle(Math.Min(ptBeg.X, ptEnd.X), _
                             Math.Min(ptBeg.Y, ptEnd.Y), _
                             Math.Abs(ptEnd.X - ptBeg.X), _
                             Math.Abs(ptEnd.Y - ptBeg.Y))
    End Function
End Class
