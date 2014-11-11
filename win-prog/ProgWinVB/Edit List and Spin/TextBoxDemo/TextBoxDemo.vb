'--------------------------------------------
' TextBoxDemo.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TextBoxDemo
    Inherits Form

    Private lbl As Label

    Shared Sub Main()
        Application.Run(New TextBoxDemo())
    End Sub

    Sub New()
        Text = "TextBox Demo"

        ' Create text box control.
        Dim txtbox As New TextBox()
        txtbox.Parent = Me
        txtbox.Location = New Point(Font.Height, Font.Height)
        txtbox.Size = New Size(ClientSize.Width - 2 * Font.Height, _
                               Font.Height)
        txtbox.Anchor = txtbox.Anchor Or AnchorStyles.Right
        AddHandler txtbox.TextChanged, AddressOf TextBoxOnTextChanged

        ' Create label control.
        lbl = New Label()
        lbl.Parent = Me
        lbl.Location = New Point(Font.Height, 3 * Font.Height)
        lbl.AutoSize = True
    End Sub

    Private Sub TextBoxOnTextChanged(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        Dim txtbox As TextBox = DirectCast(obj, TextBox)
        lbl.Text = txtbox.Text
    End Sub
End Class
