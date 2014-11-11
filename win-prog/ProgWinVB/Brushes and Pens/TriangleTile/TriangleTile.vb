'---------------------------------------------
' TriangleTile.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TriangleTile
    Inherits PrintableForm

    Const iSide As Integer = 50   ' Side of square for triangle
    Private miChecked As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New TriangleTile())
    End Sub

    Sub New()
        Text = "Triangle Tile"

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Wrap-Mode")

        Dim wm As WrapMode
        For Each wm In System.Enum.GetValues(GetType(WrapMode))
            Dim mi As New MenuItem()
            mi.Text = wm.ToString()
            AddHandler mi.Click, AddressOf MenuWrapModeOnClick
            Menu.MenuItems(0).MenuItems.Add(mi)
        Next wm

        miChecked = Menu.MenuItems(0).MenuItems(0)
        miChecked.Checked = True
    End Sub

    Sub MenuWrapModeOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        miChecked.Checked = False
        miChecked = DirectCast(obj, MenuItem)
        miChecked.Checked = True
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim apt() As Point = {New Point(0, 0), _
                              New Point(iSide, 0), _
                              New Point(0, iSide)}
        Dim pgbr As New PathGradientBrush(apt, _
                                          CType(miChecked.Index, WrapMode))
        grfx.FillRectangle(pgbr, 0, 0, cx, cy)
    End Sub
End Class
