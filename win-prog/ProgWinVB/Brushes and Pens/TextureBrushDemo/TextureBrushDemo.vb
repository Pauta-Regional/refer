'-------------------------------------------------
' TextureBrushDemo.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class TextureBrushDemo
    Inherits PrintableForm

    Private miChecked As MenuItem
    Private tbr As TextureBrush

    Shared Shadows Sub Main()
        Application.Run(New TextureBrushDemo())
    End Sub

    Sub New()
        Text = "Texture Brush Demo"

        Dim img As Image = Image.FromFile( _
                "..\..\..\Images and Bitmaps\Apollo11FullColor.jpg")
        tbr = New TextureBrush(img, New Rectangle(95, 0, 50, 55))

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

        tbr.WrapMode = CType(miChecked.Index, WrapMode)
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.FillEllipse(tbr, 0, 0, 2 * cx \ 3, 2 * cy \ 3)
        grfx.FillEllipse(tbr, cx \ 3, cy \ 3, 2 * cx \ 3, 2 * cy \ 3)
    End Sub
End Class
