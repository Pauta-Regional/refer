'-------------------------------------------------
' ScribbleWithSave.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Collections    ' For ArrayList
Imports System.Drawing
Imports System.Windows.Forms

Class ScribbleWithSave
    Inherits Form

    Private arrlstApts As New ArrayList()
    Private arrlstPts As ArrayList
    Private bTracking As Boolean

    Shared Sub Main()
        Application.Run(New ScribbleWithSave())
    End Sub

    Sub New()
        Text = "Scribble with Save"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If mea.Button <> MouseButtons.Left Then Return

        arrlstPts = New ArrayList()
        arrlstPts.Add(New Point(mea.X, mea.Y))
        bTracking = True
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal mea As MouseEventArgs)
        If Not bTracking Then Return

        arrlstPts.Add(New Point(mea.X, mea.Y))
        Dim grfx As Graphics = CreateGraphics()
        grfx.DrawLine(New Pen(ForeColor), _
                      CType(arrlstPts(arrlstPts.Count - 2), Point), _
                      CType(arrlstPts(arrlstPts.Count - 1), Point))
        grfx.Dispose()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal mea As MouseEventArgs)
        If Not bTracking Then Return

        Dim apt() As Point = _
                    CType(arrlstPts.ToArray(GetType(Point)), Point())
        arrlstApts.Add(apt)
        bTracking = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim pn As New Pen(ForeColor)
        Dim i As Integer

        For i = 0 To arrlstApts.Count - 1
            Dim apt() As Point = CType(arrlstApts(i), Point())
            If apt.Length > 1 Then grfx.DrawLines(pn, apt)
        Next i
    End Sub
End Class
