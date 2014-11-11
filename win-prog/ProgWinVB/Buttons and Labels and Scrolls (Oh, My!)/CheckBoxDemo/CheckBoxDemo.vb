'---------------------------------------------
' CheckBoxDemo.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckBoxDemo
    Inherits Form

    Shared Sub Main()
        Application.Run(New CheckBoxDemo())
    End Sub

    Sub New()
        Text = "CheckBox Demo"

        Dim achkbox(3) As CheckBox
        Dim cyText As Integer = Font.Height
        Dim cxText As Integer = cyText \ 2
        Dim astrText() As String = {"Bold", "Italic", _
                                    "Underline", "Strikeout"}
        Dim i As Integer

        For i = 0 To 3
            achkbox(i) = New CheckBox()
            achkbox(i).Text = astrText(i)
            achkbox(i).Location = New Point(2 * cxText, _
                                           (4 + 3 * i) * cyText \ 2)
            achkbox(i).Size = New Size(12 * cxText, cyText)
            AddHandler achkbox(i).CheckedChanged, _
                                        AddressOf CheckBoxOnCheckedChanged
        Next i
        Controls.AddRange(achkbox)
    End Sub

    Private Sub CheckBoxOnCheckedChanged(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        Invalidate(False)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim fs As FontStyle = 0
        Dim afs() As FontStyle = {FontStyle.Bold, FontStyle.Italic, _
                                  FontStyle.Underline, FontStyle.Strikeout}
        Dim i As Integer

        For i = 0 To 3
            If DirectCast(Controls(i), CheckBox).Checked Then
                fs = fs Or afs(i)
            End If
        Next i

        Dim fnt As New Font(Font, fs)
        grfx.DrawString(Text, fnt, New SolidBrush(ForeColor), 0, 0)
    End Sub
End Class
