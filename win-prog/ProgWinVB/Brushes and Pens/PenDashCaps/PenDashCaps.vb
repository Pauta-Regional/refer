'--------------------------------------------
' PenDashCaps.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class PenDashCaps
    Inherits PrintableForm

    Private miChecked As MenuItem

    Shared Shadows Sub Main()
        Application.Run(new PenDashCaps())
    End Sub

    Sub New()
        Text = "Pen Dash Caps: Flat, Round, Triangle"

        Menu = new MainMenu()
        Menu.MenuItems.Add("&Width")

        Dim aiWidth() As Integer = {1, 2, 5, 10, 15, 20, 25}
        Dim iWidth As Integer
        For Each iWidth In aiWidth
            Menu.MenuItems(0).MenuItems.Add(iWidth.ToString(), _
                                            AddressOf MenuWidthOnClick)
        Next iWidth
        miChecked = Menu.MenuItems(0).MenuItems(0)
        miChecked.Checked = True
    End Sub

    Sub MenuWidthOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        miChecked.Checked = False
        miChecked = DirectCast(obj, MenuItem)
        miChecked.Checked = True
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr, Convert.ToInt32(miChecked.Text))
        pn.DashStyle = DashStyle.DashDotDot
        Dim dc As DashCap

        For Each dc In System.Enum.GetValues(GetType(DashCap))
            pn.DashCap = dc
            grfx.DrawLine(pn, cx \ 8, cy \ 4, 7 * cx \ 8, cy \ 4)
            grfx.TranslateTransform(0, cy \ 4)
        Next dc
    End Sub
End Class
