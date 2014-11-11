'-----------------------------------------------
' CustomCheckBox.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CustomCheckBox
    Inherits Form

    Shared Sub Main()
        Application.Run(New CustomCheckBox())
    End Sub

    Sub New()
        Text = "Custom CheckBox Demo"

        Dim cyText As Integer = Font.Height
        Dim cxText As Integer = cyText \ 2
        Dim afs As FontStyle() = {FontStyle.Bold, FontStyle.Italic, _
                                  FontStyle.Underline, FontStyle.Strikeout}

        Dim lbl As New Label()
        lbl.Parent = Me
        lbl.Text = Text & ": Sample Text"
        lbl.AutoSize = True

        Dim i As Integer
        For i = 0 To 3
            Dim chkbox As New FontStyleCheckBox()
            chkbox.Parent = Me
            chkbox.Text = afs(i).ToString()
            chkbox.fntstyle = afs(i)
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
        Dim lbl As Label = Nothing
        Dim ctrl As Control

        For Each ctrl In Controls
            If ctrl.GetType() Is GetType(Label) Then
                lbl = DirectCast(ctrl, Label)
            ElseIf ctrl.GetType() Is GetType(FontStyleCheckBox) Then
                If DirectCast(ctrl, FontStyleCheckBox).Checked Then
                    fs = fs Or DirectCast(ctrl, FontStyleCheckBox).fntstyle
                End If
            End If
        Next

        lbl.Font = New Font(lbl.Font, fs)
    End Sub
End Class

Class FontStyleCheckBox
    Inherits CheckBox

    Public fntstyle As FontStyle
End Class
