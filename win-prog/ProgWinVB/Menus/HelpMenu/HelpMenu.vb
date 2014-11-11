'-----------------------------------------
' HelpMenu.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HelpMenu
    Inherits Form

    Private bmHelp As Bitmap

    Shared Sub Main()
        Application.Run(New HelpMenu())
    End Sub

    Sub New()
        Text = "Help Menu"

        bmHelp = New Bitmap(Me.GetType(), "Bighelp.bmp")

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Help")

        Dim mi As New MenuItem("&Help")
        mi.OwnerDraw = True
        AddHandler mi.Click, AddressOf MenuHelpOnClick
        AddHandler mi.DrawItem, AddressOf MenuHelpOnDrawItem
        AddHandler mi.MeasureItem, AddressOf MenuHelpOnMeasureItem
        Menu.MenuItems(0).MenuItems.Add(mi)
    End Sub

    Private Sub MenuHelpOnMeasureItem(ByVal obj As Object, _
                                      ByVal miea As MeasureItemEventArgs)
        miea.ItemWidth = bmHelp.Width
        miea.ItemHeight = bmHelp.Height
    End Sub

    Private Sub MenuHelpOnDrawItem(ByVal obj As Object, _
                                   ByVal diea As DrawItemEventArgs)
        Dim rect As Rectangle = diea.Bounds
        rect.X += diea.Bounds.Width - bmHelp.Width
        rect.Width = bmHelp.Width

        diea.DrawBackground()
        diea.Graphics.DrawImage(bmHelp, rect)
    End Sub

    Private Sub MenuHelpOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        MessageBox.Show("Help not yet implemented!", Text)
    End Sub
End Class
