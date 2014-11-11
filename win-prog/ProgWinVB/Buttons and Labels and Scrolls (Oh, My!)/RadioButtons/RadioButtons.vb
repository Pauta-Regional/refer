'---------------------------------------------
' RadioButtons.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class RadioButtons
    Inherits Form

    Private bFillEllipse As Boolean
    Private clrEllipse As Color

    Shared Sub Main()
        Application.Run(New RadioButtons())
    End Sub

    Sub New()
        Text = "Radio Buttons Demo"
        ResizeRedraw = True

        Dim astrColor() As String = {"Black", "Blue", "Green", "Cyan", _
                                     "Red", "Magenta", "Yellow", "White"}
        Dim grpbox As New GroupBox()
        grpbox.Parent = Me
        grpbox.Text = "Color"
        grpbox.Location = New Point(Font.Height \ 2, Font.Height \ 2)
        grpbox.Size = New Size(9 * Font.Height, _
                              (3 * astrColor.Length + 4) * Font.Height \ 2)
        Dim i As Integer
        For i = 0 To astrColor.GetUpperBound(0)
            Dim radbtn As New RadioButton()
            radbtn.Parent = grpbox
            radbtn.Text = astrColor(i)
            radbtn.Location = New Point(Font.Height, _
                                        3 * (i + 1) * Font.Height \ 2)
            radbtn.Size = New Size(7 * Font.Height, _
                                   3 * Font.Height \ 2)
            AddHandler radbtn.CheckedChanged, _
                                    AddressOf RadioButtonOnCheckedChanged
            If i = 0 Then
                radbtn.Checked = True
            End If
        Next i

        Dim chkbox As New CheckBox()
        chkbox.Parent = Me
        chkbox.Text = "Fill Ellipse"
        chkbox.Location = New Point(Font.Height, _
                            3 * (astrColor.Length + 2) * Font.Height \ 2)
        chkbox.Size = New Size(Font.Height * 7, 3 * Font.Height \ 2)
        AddHandler chkbox.CheckedChanged, AddressOf CheckBoxOnCheckedChanged
    End Sub

    Private Sub RadioButtonOnCheckedChanged(ByVal obj As Object, _
                                            ByVal ea As EventArgs)
        Dim radbtn As RadioButton = DirectCast(obj, RadioButton)

        If radbtn.Checked Then
            clrEllipse = Color.FromName(radbtn.Text)
            Invalidate(False)
        End If
    End Sub

    Private Sub CheckBoxOnCheckedChanged(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        bFillEllipse = DirectCast(obj, CheckBox).Checked
        Invalidate(False)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim rect As New Rectangle(10 * Font.Height, 0, _
                                  ClientSize.Width - 10 * Font.Height - 1, _
                                  ClientSize.Height - 1)
        If bFillEllipse Then
            grfx.FillEllipse(New SolidBrush(clrEllipse), rect)
        Else
            grfx.DrawEllipse(New Pen(clrEllipse), rect)
        End If
    End Sub
End Class
