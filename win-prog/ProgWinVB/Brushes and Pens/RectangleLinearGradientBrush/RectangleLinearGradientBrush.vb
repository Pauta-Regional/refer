'-------------------------------------------------------------
' RectangleLinearGradientBrush.vb (c) 2002 by Charles Petzold
'-------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class RectangleLinearGradientBrush
    Inherits PrintableForm

    Private miChecked As MenuItem

    Shared Shadows Sub Main()
        Application.Run(new RectangleLinearGradientBrush())
    End Sub

    Sub New()
        Text = "Rectangle Linear-Gradient Brush"

        Menu = new MainMenu()
        Menu.MenuItems.Add("&Gradient-Mode")

        Dim gm As LinearGradientMode
        For Each gm In System.Enum.GetValues(GetType(LinearGradientMode))
            Dim mi As New MenuItem()
            mi.Text = gm.ToString()
            AddHandler mi.Click, AddressOf MenuGradientModeOnClick
            Menu.MenuItems(0).MenuItems.Add(mi)
        Next gm

        miChecked = Menu.MenuItems(0).MenuItems(0)
        miChecked.Checked = True
    End Sub

    Sub MenuGradientModeOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        miChecked.Checked = False
        miChecked = DirectCast(obj, MenuItem)
        miChecked.Checked = True
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim rectBrush As New Rectangle(cx \ 4, cy \ 4, cx \ 2, cy \ 2)
        Dim lgbr As New LinearGradientBrush( _
                                rectBrush, Color.White, Color.Black, _
                                CType(miChecked.Index, LinearGradientMode))
        grfx.FillRectangle(lgbr, 0, 0, cx, cy)
        grfx.DrawRectangle(Pens.Black, rectBrush)
    End Sub
End Class
