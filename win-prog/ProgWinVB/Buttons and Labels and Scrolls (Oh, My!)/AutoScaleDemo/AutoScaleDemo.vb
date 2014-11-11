'----------------------------------------------
' AutoScaleDemo.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class AutoScaleDemo
    Inherits Form

    Shared Sub Main()
        Application.Run(new AutoScaleDemo())
    End Sub

    Sub New()
        Text = "Auto-Scale Demo"
        Font = new Font("Arial", 12)
        FormBorderStyle = FormBorderStyle.FixedSingle

        Dim aiPointSize() As Integer = {8, 12, 16, 24, 32}
        Dim i As Integer
        For i = 0 To aiPointSize.GetUpperBound(0)
            Dim btn As New Button()
            btn.Parent = Me
            btn.Text = "Use " & aiPointSize(i).ToString() & "-point font"
            btn.Tag = aiPointSize(i)
            btn.Location = New Point(4, 16 + 24 * i)
            btn.Size = New Size(80, 16)
            AddHandler btn.Click, AddressOf ButtonOnClick
        Next i

        ClientSize = New Size(88, 16 + 24 * aiPointSize.Length)
        AutoScaleBaseSize = New Size(4, 8)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        pea.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0)
    End Sub

    Private Sub ButtonOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim btn As Button = DirectCast(obj, Button)

        Dim szfOld As SizeF = GetAutoScaleSize(Font)
        Font = New Font(Font.FontFamily, DirectCast(btn.Tag, Integer))
        Dim szfNew As SizeF = GetAutoScaleSize(Font)

        Scale(szfNew.Width / szfOld.Width, szfNew.Height / szfOld.Height)
    End Sub
End Class
