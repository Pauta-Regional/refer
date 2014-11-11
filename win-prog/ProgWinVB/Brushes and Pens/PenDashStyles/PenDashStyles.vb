'----------------------------------------------
' PenDashStyles.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class PenDashStyles
    Inherits PrintableForm

    Private miChecked As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New PenDashStyles())
    End Sub

    Sub New()
        Text = "Pen Dash Styles"

        Menu = New MainMenu()
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
        Dim pn As New Pen(clr)
        pn.Width = Convert.ToInt32(miChecked.Text)
        Dim i As Integer

        For i = 0 To 4
            pn.DashStyle = CType(i, DashStyle)
            Dim y As Integer = (i + 1) * cy \ 6
            grfx.DrawLine(pn, cx \ 8, y, 7 * cx \ 8, y)
        Next i
    End Sub
End Class
