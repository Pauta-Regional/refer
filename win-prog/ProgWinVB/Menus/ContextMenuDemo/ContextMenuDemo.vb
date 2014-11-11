'------------------------------------------------
' ContextMenuDemo.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ContextMenuDemo
    Inherits Form

    Private miColor As MenuItem

    Shared Sub Main()
        Application.Run(New ContextMenuDemo())
    End Sub

    Sub New()
        Text = "Context Menu Demo"

        Dim eh As EventHandler = AddressOf MenuColorOnClick
        Dim ami() As MenuItem = {New MenuItem("Black", eh), _
                                 New MenuItem("Blue", eh), _
                                 New MenuItem("Green", eh), _
                                 New MenuItem("Cyan", eh), _
                                 New MenuItem("Red", eh), _
                                 New MenuItem("Magenta", eh), _
                                 New MenuItem("Yellow", eh), _
                                 New MenuItem("White", eh)}
        Dim mi As MenuItem

        For Each mi In ami
            mi.RadioCheck = True
        Next mi

        miColor = ami(3)
        miColor.Checked = True
        BackColor = Color.FromName(miColor.Text)

        ContextMenu = New ContextMenu(ami)
    End Sub

    Private Sub MenuColorOnClick(ByVal obj As Object, _
                                 ByVal ea As EventArgs)
        miColor.Checked = False
        miColor = DirectCast(obj, MenuItem)
        miColor.Checked = True
        BackColor = Color.FromName(miColor.Text)
    End Sub
End Class
