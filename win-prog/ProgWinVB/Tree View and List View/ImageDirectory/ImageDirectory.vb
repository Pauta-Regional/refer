'-----------------------------------------------
' ImageDirectory.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ImageDirectory
    Inherits Form

    Private picbox As PictureBoxPlus
    Private dirtree As DirectoryTreeView
    Private imgpnl As ImagePanel
    Private split As Splitter
    Private tnSelect As TreeNode
    Private ctrlClicked As Control
    Private ptPanelAutoScroll As Point

    Shared Sub Main()
        Application.Run(New ImageDirectory())
    End Sub

    Sub New()
        Text = "Image Directory"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create (invisible) control for displaying large image.
        picbox = New PictureBoxPlus()
        picbox.Parent = Me
        picbox.Visible = False
        picbox.Dock = DockStyle.Fill
        picbox.SizeMode = PictureBoxSizeMode.StretchImage
        picbox.NoDistort = True
        AddHandler picbox.MouseDown, AddressOf PictureBoxOnMouseDown

        ' Create controls for displaying thumbnails.
        imgpnl = New ImagePanel()
        imgpnl.Parent = Me
        imgpnl.Dock = DockStyle.Fill
        AddHandler imgpnl.ImageClicked, AddressOf ImagePanelOnImageClicked

        split = New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left
        split.BackColor = SystemColors.Control

        dirtree = New DirectoryTreeView()
        dirtree.Parent = Me
        dirtree.Dock = DockStyle.Left
        AddHandler dirtree.AfterSelect, _
                                    AddressOf DirectoryTreeViewOnAfterSelect

        ' Create menu with one item (Refresh).
        Menu = New MainMenu()
        Menu.MenuItems.Add("&View")
        Dim mi As New MenuItem("&Refresh", _
                               AddressOf MenuOnRefresh, Shortcut.F5)
        Menu.MenuItems(0).MenuItems.Add(mi)
    End Sub

    Private Sub DirectoryTreeViewOnAfterSelect(ByVal obj As Object, _
                                        ByVal tvea As TreeViewEventArgs)
        tnSelect = tvea.Node
        imgpnl.ShowImages(tnSelect.FullPath)
    End Sub

    Private Sub MenuOnRefresh(ByVal obj As Object, ByVal ea As EventArgs)
        dirtree.RefreshTree()
    End Sub

    Private Sub ImagePanelOnImageClicked(ByVal obj As Object, _
                                         ByVal ea As EventArgs)
        ' Get clicked control and image.
        ctrlClicked = imgpnl.ClickedControl
        picbox.Image = imgpnl.ClickedImage

        ' Save auto-scroll position.
        ptPanelAutoScroll = imgpnl.AutoScrollPosition
        ptPanelAutoScroll.X *= -1
        ptPanelAutoScroll.Y *= -1

        ' Hide and disable the normal controls.
        imgpnl.Visible = False
        imgpnl.Enabled = False
        imgpnl.AutoScrollPosition = Point.Empty
        split.Visible = False
        split.Enabled = False
        dirtree.Visible = False
        dirtree.Enabled = False

        ' Make the picture box visible.
        picbox.Visible = True
    End Sub

    ' Event handlers and method involved with restoring controls
    Private Sub PictureBoxOnMouseDown(ByVal obj As Object, _
                                      ByVal mea As MouseEventArgs)
        RestoreControls()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        If kea.KeyCode = Keys.Escape Then
            RestoreControls()
        End If
    End Sub

    Private Sub RestoreControls()
        picbox.Visible = False
        dirtree.Visible = True
        dirtree.Enabled = True
        split.Enabled = True
        split.Visible = True
        imgpnl.AutoScrollPosition = ptPanelAutoScroll
        imgpnl.Visible = True
        imgpnl.Enabled = True
        ctrlClicked.Focus()
    End Sub
End Class
