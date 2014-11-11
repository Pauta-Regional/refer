'---------------------------------------------
' ButtonStyles.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ButtonStyles
    Inherits Form

    Shared Sub Main()
        Application.Run(New ButtonStyles())
    End Sub

    Sub New()
        Text = "Button Styles"

        Dim y As Integer = 50
        Dim fs As FlatStyle

        For Each fs In System.Enum.GetValues(GetType(FlatStyle))
            Dim btn As New Button()
            btn.Parent = Me
            btn.FlatStyle = fs
            btn.Text = fs.ToString()
            btn.Location = New Point(50, y)
            y += 50
        Next fs
    End Sub
End Class
