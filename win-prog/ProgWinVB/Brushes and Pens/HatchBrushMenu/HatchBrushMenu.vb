'-----------------------------------------------
' HatchBrushMenu.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class HatchBrushMenu
    Inherits Form

    Private hsmiChecked As HatchStyleMenuItem

    ' HatchStyle minimum and maximum values
    Const hsMin As HatchStyle = CType(0, HatchStyle)
    Const hsMax As HatchStyle = CType(52, HatchStyle)

    Shared Sub Main()
        Application.Run(New HatchBrushMenu())
    End Sub

    Sub New()
        Text = "Hatch Brush Menu"
        ResizeRedraw = True

        Menu = New MainMenu()
        Menu.MenuItems.Add("&Hatch-Style")

        Dim hs As HatchStyle
        For hs = hsMin To hsMax
            Dim hsmi As New HatchStyleMenuItem()
            hsmi.HatchStyle = hs
            AddHandler hsmi.Click, AddressOf MenuHatchStyleOnClick
            If hs Mod 8 = 0 Then hsmi.BarBreak = True
            Menu.MenuItems(0).MenuItems.Add(hsmi)
        Next hs
        hsmiChecked = DirectCast(Menu.MenuItems(0).MenuItems(0), _
                                 HatchStyleMenuItem)
        hsmiChecked.Checked = True
    End Sub

    Sub MenuHatchStyleOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        hsmiChecked.Checked = False
        hsmiChecked = DirectCast(obj, HatchStyleMenuItem)
        hsmiChecked.Checked = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim hbr As New HatchBrush(hsmiChecked.HatchStyle, _
                                  Color.White, Color.Black)
        grfx.FillEllipse(hbr, ClientRectangle)
    End Sub
End Class

Class HatchStyleMenuItem
    Inherits MenuItem

    Const cxImage As Integer = 32
    Const cyImage As Integer = 32
    Const iMargin As Integer = 2
    ReadOnly cxCheck, cyCheck As Integer
    Public HatchStyle As HatchStyle

    Sub New()
        OwnerDraw = True
        cxCheck = SystemInformation.MenuCheckSize.Width
        cyCheck = SystemInformation.MenuCheckSize.Height
    End Sub

    Protected Overrides Sub OnMeasureItem( _
            ByVal miea As MeasureItemEventArgs)
        miea.ItemWidth = 2 * cxImage + 3 * iMargin - cxCheck
        miea.ItemHeight = cyImage + 2 * iMargin
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal diea As DrawItemEventArgs)
        diea.DrawBackground()
        If (diea.State And DrawItemState.Checked) <> 0 Then
            ControlPaint.DrawMenuGlyph(diea.Graphics, _
                            diea.Bounds.Location.X + iMargin, _
                            diea.Bounds.Location.Y + iMargin, _
                            cxImage, cyImage, MenuGlyph.Checkmark)
        End If
        Dim hbr As New HatchBrush(HatchStyle, Color.White, Color.Black)
        diea.Graphics.FillRectangle(hbr, _
                        diea.Bounds.X + 2 * iMargin + cxImage, _
                        diea.Bounds.Y + iMargin, cxImage, cyImage)
    End Sub
End Class
