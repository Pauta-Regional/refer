'-----------------------------------------------------
' ClippingCombinations.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class ClippingCombinations
    Inherits PrintableForm

    Private strCaption As String = "CombineMode = "
    Private miCombineMode As MenuItem

    Shared Shadows Sub Main()
        Application.Run(New ClippingCombinations())
    End Sub

    Sub New()
        Text = strCaption & CType(0, CombineMode).ToString()

        Menu = New MainMenu()
        Menu.MenuItems.Add("&CombineMode")
        Dim ehClick As EventHandler = AddressOf MenuCombineModeOnClick
        Dim i As Integer

        For i = 0 To 5
            Dim mi As New MenuItem("&" & CType(i, CombineMode).ToString())
            AddHandler mi.Click, ehClick
            mi.RadioCheck = True
            Menu.MenuItems(0).MenuItems.Add(mi)
        Next i
        miCombineMode = Menu.MenuItems(0).MenuItems(0)
        miCombineMode.Checked = True
    End Sub

    Private Sub MenuCombineModeOnClick(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        miCombineMode.Checked = False
        miCombineMode = DirectCast(obj, MenuItem)
        miCombineMode.Checked = True
        Text = strCaption & _
                    CType(miCombineMode.Index, CombineMode).ToString()
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, 2 * cx \ 3, cy)
        grfx.SetClip(path)
        path.Reset()
        path.AddEllipse(cx \ 3, 0, 2 * cx \ 3, cy)
        grfx.SetClip(path, CType(miCombineMode.Index, CombineMode))
        grfx.FillRectangle(Brushes.Red, 0, 0, cx, cy)
    End Sub
End Class
