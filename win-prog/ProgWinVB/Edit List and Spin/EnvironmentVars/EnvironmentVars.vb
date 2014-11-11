'------------------------------------------------
' EnvironmentVars.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms

Class EnvironmentVars
    Inherits Form

    Private lbl As Label

    Shared Sub Main()
        Application.Run(New EnvironmentVars())
    End Sub

    Sub New()
        Text = "Environment Variables"

        ' Create Label control.
        lbl = New Label()
        lbl.Parent = Me
        lbl.Anchor = lbl.Anchor Or AnchorStyles.Right
        lbl.Location = New Point(Font.Height, Font.Height)
        lbl.Size = New Size(ClientSize.Width - 2 * Font.Height, Font.Height)

        ' Create ListBox control.
        Dim lstbox As New ListBox()
        lstbox.Parent = Me
        lstbox.Location = New Point(Font.Height, 3 * Font.Height)
        lstbox.Size = New Size(12 * Font.Height, 8 * Font.Height)
        lstbox.Sorted = True
        AddHandler lstbox.SelectedIndexChanged, _
                                    AddressOf ListBoxOnSelectedIndexChanged

        ' Set environment strings in ListBox control.
        Dim dict As IDictionary = Environment.GetEnvironmentVariables()
        Dim astr(dict.Keys.Count - 1) As String

        dict.Keys.CopyTo(astr, 0)
        lstbox.Items.AddRange(astr)
        lstbox.SelectedIndex = 0
    End Sub

    Private Sub ListBoxOnSelectedIndexChanged(ByVal obj As Object, _
                                              ByVal ea As EventArgs)
        Dim lstbox As ListBox = DirectCast(obj, ListBox)
        Dim strItem As String = DirectCast(lstbox.SelectedItem, String)

        lbl.Text = Environment.GetEnvironmentVariable(strItem)
    End Sub
End Class
