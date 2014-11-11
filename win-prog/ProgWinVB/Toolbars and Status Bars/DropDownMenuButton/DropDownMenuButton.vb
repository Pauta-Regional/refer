'---------------------------------------------------
' DropDownMenuButton.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DropDownMenuButton
    Inherits ToggleButtons

    Shared Shadows Sub Main()
        Application.Run(New DropDownMenuButton())
    End Sub

    Sub New()
        Text = "Drop-Down Menu Button"
        strText = "Drop-Down"

        ' Create bitmap for new button and add it to ImageList.
        tbar.ImageList.Images.Add(CreateBitmapButton(clrText))

        ' Create the menu for the button.
        Dim menu As New ContextMenu()
        Dim ehOnClick As EventHandler = AddressOf MenuColorOnClick
        Dim ehOnMeasureItem As MeasureItemEventHandler = _
                                AddressOf MenuColorOnMeasureItem
        Dim ehOnDrawItem As DrawItemEventHandler = _
                                AddressOf MenuColorOnDrawItem
        Dim aclr() As Color = _
        { _
        Color.FromArgb(&H0, &H0, &H0), Color.FromArgb(&H0, &H0, &H80), _
        Color.FromArgb(&H0, &H80, &H0), Color.FromArgb(&H0, &H80, &H80), _
        Color.FromArgb(&H80, &H0, &H0), Color.FromArgb(&H80, &H0, &H80), _
        Color.FromArgb(&H80, &H80, &H0), Color.FromArgb(&H80, &H80, &H80), _
        Color.FromArgb(&HC0, &HC0, &HC0), Color.FromArgb(&H0, &H0, &HFF), _
        Color.FromArgb(&H0, &HFF, &H0), Color.FromArgb(&H0, &HFF, &HFF), _
        Color.FromArgb(&HFF, &H0, &H0), Color.FromArgb(&HFF, &H0, &HFF), _
        Color.FromArgb(&HFF, &HFF, &H0), Color.FromArgb(&HFF, &HFF, &HFF) _
        }
        Dim i As Integer
        For i = 0 To aclr.GetUpperBound(0)
            Dim mic As New MenuItemColor()
            mic.OwnerDraw = True
            mic.Color = aclr(i)
            AddHandler mic.Click, ehOnClick
            AddHandler mic.MeasureItem, ehOnMeasureItem
            AddHandler mic.DrawItem, ehOnDrawItem
            mic.Break = (i Mod 4 = 0)
            menu.MenuItems.Add(mic)
        Next i

        ' Finally, make the button itself.
        Dim tbb As New ToolBarButton()
        tbb.ImageIndex = 4
        tbb.Style = ToolBarButtonStyle.DropDownButton
        tbb.DropDownMenu = menu
        tbb.ToolTipText = "Color"
        tbar.Buttons.Add(tbb)
    End Sub

    Private Sub MenuColorOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        ' Set the new text color.
        Dim mic As MenuItemColor = DirectCast(obj, MenuItemColor)
        clrText = mic.Color
        pnl.Invalidate()

        ' Make a new button bitmap.
        tbar.ImageList.Images(4) = CreateBitmapButton(clrText)
        tbar.Invalidate()
    End Sub

    Private Sub MenuColorOnMeasureItem(ByVal obj As Object, _
                                       ByVal miea As MeasureItemEventArgs)
        miea.ItemHeight = 18
        miea.ItemWidth = 18
    End Sub

    Private Sub MenuColorOnDrawItem(ByVal obj As Object, _
                                    ByVal diea As DrawItemEventArgs)
        Dim mic As MenuItemColor = DirectCast(obj, MenuItemColor)
        Dim br As New SolidBrush(mic.Color)
        Dim rect As Rectangle = diea.Bounds

        rect.X += 1
        rect.Y += 1
        rect.Width -= 2
        rect.Height -= 2
        diea.Graphics.FillRectangle(br, rect)
    End Sub

    Private Function CreateBitmapButton(ByVal clr As Color) As Bitmap
        Dim bm As New Bitmap(16, 16)
        Dim grfx As Graphics = Graphics.FromImage(bm)
        Dim fnt As New Font("Arial", 10, FontStyle.Bold)
        Dim szf As SizeF = grfx.MeasureString("A", fnt)
        Dim fScale As Single = Math.Min(bm.Width / szf.Width, _
                                        bm.Height / szf.Height)

        fnt = New Font(fnt.Name, fScale * fnt.SizeInPoints, fnt.Style)

        Dim strfmt As New StringFormat()
        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        grfx.Clear(Color.White)
        grfx.DrawString("A", fnt, New SolidBrush(clr), _
                        bm.Width \ 2, bm.Height \ 2, strfmt)
        grfx.Dispose()
        Return bm
    End Function
End Class

Class MenuItemColor
    Inherits MenuItem

    Private clr As Color

    Property Color() As Color
        Set(ByVal Value As Color)
            clr = Value
        End Set
        Get
            Return clr
        End Get
    End Property
End Class
