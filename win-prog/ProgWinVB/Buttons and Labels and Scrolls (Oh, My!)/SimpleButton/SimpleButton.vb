'---------------------------------------------
' SimpleButton.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleButton
    Inherits Form

    Shared Sub Main()
        Application.Run(New SimpleButton())
    End Sub

    Sub New()
        Text = "Simple Button"

        Dim btn As New Button()
        btn.Parent = Me
        btn.Text = "Click Me!"
        btn.Location = New Point(100, 100)
        AddHandler btn.Click, AddressOf ButtonOnClick
    End Sub

    Private Sub ButtonOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim grfx As Graphics = CreateGraphics()
        Dim ptfText As PointF = PointF.Empty
        Dim str As String = "Button clicked!"

        grfx.DrawString(str, Font, New SolidBrush(ForeColor), ptfText)
        System.Threading.Thread.Sleep(250)
        grfx.FillRectangle(New SolidBrush(BackColor), _
                    New RectangleF(ptfText, grfx.MeasureString(str, Font)))
        grfx.Dispose()
    End Sub
End Class
