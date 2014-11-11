'---------------------------------------------------
' NotepadCloneNoMenu.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NotepadCloneNoMenu
    Inherits Form

    Protected txtbox As TextBox

    Shared Sub Main()
        Application.Run(New NotepadCloneNoMenu())
    End Sub

    Sub New()
        Text = "Notepad Clone No Menu"

        txtbox = New TextBox()
        txtbox.Parent = Me
        txtbox.Dock = DockStyle.Fill
        txtbox.BorderStyle = BorderStyle.None
        txtbox.Multiline = True
        txtbox.ScrollBars = ScrollBars.Both
        txtbox.AcceptsTab = True
    End Sub
End Class
