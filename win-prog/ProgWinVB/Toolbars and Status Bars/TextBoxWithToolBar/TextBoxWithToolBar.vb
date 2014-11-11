'---------------------------------------------------
' TextBoxWithToolBar.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class TextBoxWithToolBar
    Inherits Form

    Private txtbox As TextBox
    Private miEditCut, miEditCopy, miEditPaste As MenuItem
    Private tbbCut, tbbCopy, tbbPaste As ToolBarButton

    Shared Sub Main()
        Application.Run(New TextBoxWithToolBar())
    End Sub

    Sub New()
        Text = "Text Box with Toolbar"

        ' Create TextBox.
        txtbox = New TextBox()
        txtbox.Parent = Me
        txtbox.Dock = DockStyle.Fill
        txtbox.Multiline = True
        txtbox.ScrollBars = ScrollBars.Both
        txtbox.AcceptsTab = True

        ' Create ImageList.
        Dim bm As New Bitmap(Me.GetType(), "StandardButtons.bmp")
        Dim imglst As New ImageList()
        imglst.Images.AddStrip(bm)
        imglst.TransparentColor = Color.Cyan

        ' Create ToolBar with ButtonClick event handler.
        Dim tbar As New ToolBar()
        tbar.Parent = Me
        tbar.ImageList = imglst
        tbar.ShowToolTips = True
        AddHandler tbar.ButtonClick, AddressOf ToolBarOnClick

        ' Create the Edit menu.
        Menu = New MainMenu()
        Dim mi As New MenuItem("&Edit")
        AddHandler mi.Popup, AddressOf MenuEditOnPopup
        Menu.MenuItems.Add(mi)

        ' Create the Edit Cut menu item.
        miEditCut = New MenuItem("Cu&t")
        AddHandler miEditCut.Click, AddressOf MenuEditCutOnClick
        miEditCut.Shortcut = Shortcut.CtrlX
        Menu.MenuItems(0).MenuItems.Add(miEditCut)

        ' And create the Cut toolbar button.
        tbbCut = New ToolBarButton()
        tbbCut.ImageIndex = 4
        tbbCut.ToolTipText = "Cut"
        tbbCut.Tag = miEditCut
        tbar.Buttons.Add(tbbCut)

        ' Create the Edit Copy menu item.
        miEditCopy = New MenuItem("&Copy")
        AddHandler miEditCopy.Click, AddressOf MenuEditCopyOnClick
        miEditCopy.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(0).MenuItems.Add(miEditCopy)

        ' And create the Copy toolbar button.
        tbbCopy = New ToolBarButton()
        tbbCopy.ImageIndex = 5
        tbbCopy.ToolTipText = "Copy"
        tbbCopy.Tag = miEditCopy
        tbar.Buttons.Add(tbbCopy)

        ' Create the Edit Paste menu item.
        miEditPaste = New MenuItem("&Paste")
        AddHandler miEditPaste.Click, AddressOf MenuEditPasteOnClick
        miEditPaste.Shortcut = Shortcut.CtrlV
        Menu.MenuItems(0).MenuItems.Add(miEditPaste)

        ' And create the Paste toolbar button.
        tbbPaste = New ToolBarButton()
        tbbPaste.ImageIndex = 6
        tbbPaste.ToolTipText = "Paste"
        tbbPaste.Tag = miEditPaste
        tbar.Buttons.Add(tbbPaste)

        ' Set Timer for enabling buttons.
        Dim tmr As New Timer()
        tmr.Interval = 250
        AddHandler tmr.Tick, AddressOf TimerOnTick
        tmr.Start()
    End Sub

    Private Sub MenuEditOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miEditCopy.Enabled = txtbox.SelectionLength > 0
        miEditCut.Enabled = miEditCopy.Enabled

        miEditPaste.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(GetType(String))
    End Sub

    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        tbbCopy.Enabled = txtbox.SelectionLength > 0
        tbbCut.Enabled = tbbCopy.Enabled

        tbbPaste.Enabled = _
            Clipboard.GetDataObject().GetDataPresent(GetType(String))
    End Sub

    Private Sub ToolBarOnClick(ByVal obj As Object, _
                               ByVal tbbcea As ToolBarButtonClickEventArgs)
        Dim tbb As ToolBarButton = tbbcea.Button
        Dim mi As MenuItem = DirectCast(tbb.Tag, MenuItem)
        mi.PerformClick()
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        txtbox.Cut()
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        txtbox.Copy()
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        txtbox.Paste()
    End Sub
End Class
