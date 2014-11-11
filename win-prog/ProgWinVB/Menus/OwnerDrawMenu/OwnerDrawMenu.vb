'----------------------------------------------
' OwnerDrawMenu.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text        ' For HotkeyPrefix enumeration
Imports System.Windows.Forms

Class OwnerDrawMenu
    Inherits Form

    Const iFontPointSize As Integer = 18    ' For menu items
    Private miFacename As MenuItem

    Shared Sub Main()
        Application.Run(New OwnerDrawMenu())
    End Sub

    Sub New()
        Text = "Owner-Draw Menu"

        ' Top-level items
        Menu = New MainMenu()
        Menu.MenuItems.Add("&Facename")

        ' Array of items on submenu
        Dim astrText() As String = {"&Times New Roman", _
                                     "&Arial", "&Courier New"}
        Dim ami(astrText.Length - 1) As MenuItem
        Dim ehOnClick As EventHandler = AddressOf MenuFacenameOnClick
        Dim ehOnMeasureItem As MeasureItemEventHandler = _
            New MeasureItemEventHandler(AddressOf MenuFacenameOnMeasureItem)
        Dim ehOnDrawItem As DrawItemEventHandler = _
            New DrawItemEventHandler(AddressOf MenuFacenameOnDrawItem)
        Dim i As Integer

        For i = 0 To ami.GetUpperBound(0)
            ami(i) = New MenuItem(astrText(i))
            ami(i).OwnerDraw = True
            ami(i).RadioCheck = True
            AddHandler ami(i).Click, ehOnClick
            AddHandler ami(i).MeasureItem, ehOnMeasureItem
            AddHandler ami(i).DrawItem, ehOnDrawItem
        Next i

        miFacename = ami(0)
        miFacename.Checked = True
        Menu.MenuItems(0).MenuItems.AddRange(ami)
    End Sub

    Private Sub MenuFacenameOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        miFacename.Checked = False
        miFacename = DirectCast(obj, MenuItem)
        miFacename.Checked = True
        Invalidate()
    End Sub

    Private Sub MenuFacenameOnMeasureItem(ByVal obj As Object, _
                                          ByVal miea As MeasureItemEventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        Dim fnt As New Font(mi.Text.Substring(1), iFontPointSize)
        Dim strfmt As New StringFormat()
        strfmt.HotkeyPrefix = HotkeyPrefix.Show
        Dim szf As SizeF = miea.Graphics.MeasureString(mi.Text, fnt, _
                                                       1000, strfmt)

        miea.ItemWidth = CInt(Math.Ceiling(szf.Width))
        miea.ItemHeight = CInt(Math.Ceiling(szf.Height))
        miea.ItemWidth += SystemInformation.MenuCheckSize.Width * _
                             miea.ItemHeight \ _
                                SystemInformation.MenuCheckSize.Height
        miea.ItemWidth -= SystemInformation.MenuCheckSize.Width
    End Sub

    Private Sub MenuFacenameOnDrawItem(ByVal obj As Object, _
                                       ByVal diea As DrawItemEventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        Dim grfx As Graphics = diea.Graphics
        Dim br As Brush

        ' Create the Font and StringFormat.
        Dim fnt As New Font(mi.Text.Substring(1), iFontPointSize)
        Dim strfmt As New StringFormat()
        strfmt.HotkeyPrefix = HotkeyPrefix.Show

        ' Calculate check mark and text rectangles.
        Dim rectCheck As Rectangle = diea.Bounds
        rectCheck.Width = SystemInformation.MenuCheckSize.Width * _
                             rectCheck.Height \ _
                                SystemInformation.MenuCheckSize.Height
        Dim rectText As Rectangle = diea.Bounds
        rectText.X += rectCheck.Width

        ' Do all the drawing.
        diea.DrawBackground()
        If (diea.State And DrawItemState.Checked) <> 0 Then
            ControlPaint.DrawMenuGlyph(grfx, rectCheck, MenuGlyph.Bullet)
        End If
        If (diea.State And DrawItemState.Selected) <> 0 Then
            br = SystemBrushes.HighlightText
        Else
            br = SystemBrushes.FromSystemColor(SystemColors.MenuText)
        End If
        grfx.DrawString(mi.Text, fnt, br, _
                        RectangleF.op_Implicit(rectText), strfmt)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim fnt As New Font(miFacename.Text.Substring(1), 12)
        Dim strfmt As New StringFormat()

        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center
        grfx.DrawString(Text, fnt, New SolidBrush(ForeColor), 0, 0)
    End Sub
End Class
