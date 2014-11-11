'--------------------------------------------------
' CheckBoxWithLabel.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckBoxWithLabel
    Inherits Form

    Private lbl As Label

    Shared Sub Main()
        Application.Run(New CheckBoxWithLabel())
    End Sub

    Sub New()
        Text = "CheckBox Demo with Label"

        Dim cyText As Integer = Font.Height
        Dim cxText As Integer = cyText \ 2
        Dim astrText() As String = {"Bold", "Italic", _
                                    "Underline", "Strikeout"}

        lbl = New Label()
        lbl.Parent = Me
        lbl.Text = Text & ": Sample Text"
        lbl.AutoSize = True

        Dim i As Integer
        For i = 0 To 3
            Dim chkbox As New CheckBox()
            chkbox.Parent = Me
            chkbox.Text = astrText(i)
            chkbox.Location = New Point(2 * cxText, _
                                       (4 + 3 * i) * cyText \ 2)
            chkbox.Size = New Size(12 * cxText, cyText)
            AddHandler chkbox.CheckedChanged, _
                                    AddressOf CheckBoxOnCheckedChanged
        Next i
    End Sub

    Private Sub CheckBoxOnCheckedChanged(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        Dim fs As FontStyle = 0
        Dim afs() As FontStyle = {FontStyle.Bold, FontStyle.Italic, _
                                  FontStyle.Underline, FontStyle.Strikeout}
        Dim i As Integer

        For i = 0 To 3
            If DirectCast(Controls(i + 1), CheckBox).Checked Then
                fs = fs Or afs(i)
            End If
        Next i
        lbl.Font = New Font(lbl.Font, fs)
    End Sub
End Class
