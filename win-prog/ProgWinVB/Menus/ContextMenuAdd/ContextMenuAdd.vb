'-----------------------------------------------
' ContextMenuAdd.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ContextMenuAdd
    Inherits Form

    Private miColor As MenuItem

    Shared Sub Main()
        Application.Run(New ContextMenuAdd())
    End Sub

    Sub New()
        Text = "Context Menu Using Add"

        Dim cm As New ContextMenu()
        Dim eh As EventHandler = AddressOf MenuColorOnClick
        Dim mi As MenuItem

        cm.MenuItems.Add("Black", eh)
        cm.MenuItems.Add("Blue", eh)
        cm.MenuItems.Add("Green", eh)
        cm.MenuItems.Add("Cyan", eh)
        cm.MenuItems.Add("Red", eh)
        cm.MenuItems.Add("Magenta", eh)
        cm.MenuItems.Add("Yellow", eh)
        cm.MenuItems.Add("White", eh)

        For Each mi In cm.MenuItems
            mi.RadioCheck = True
        Next mi

        miColor = cm.MenuItems(3)
        miColor.Checked = True
        BackColor = Color.FromName(miColor.Text)

        ContextMenu = cm
    End Sub

    Private Sub MenuColorOnClick(ByVal obj As Object, _
                                 ByVal ea As EventArgs)
        miColor.Checked = False
        miColor = DirectCast(obj, MenuItem)
        miColor.Checked = True
        BackColor = Color.FromName(miColor.Text)
    End Sub
End Class
