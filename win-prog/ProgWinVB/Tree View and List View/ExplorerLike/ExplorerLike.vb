'---------------------------------------------
' ExplorerLike.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ExplorerLike
    Inherits Form

    Private filelist As FileListView
    Private dirtree As DirectoryTreeView
    Private mivChecked As MenuItemView

    Shared Sub Main()
        Application.Run(New ExplorerLike())
    End Sub

    Sub New()
        Text = "Windows Explorer-Like Program"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create controls.
        filelist = New FileListView()
        filelist.Parent = Me
        filelist.Dock = DockStyle.Fill

        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left
        split.BackColor = SystemColors.Control

        dirtree = New DirectoryTreeView()
        dirtree.Parent = Me
        dirtree.Dock = DockStyle.Left
        AddHandler dirtree.AfterSelect, AddressOf DirectoryTreeViewOnAfterSelect

        ' Create View menu.
        Menu = New MainMenu()
        Menu.MenuItems.Add("&View")

        Dim astrView() As String = {"Lar&ge Icons", "S&mall Icons", _
                                    "&List", "&Details"}
        Dim aview() As View = {View.LargeIcon, View.SmallIcon, _
                               View.List, View.Details}
        Dim eh As EventHandler = AddressOf MenuOnView
        Dim i As Integer

        For i = 0 To 3
            Dim miv As New MenuItemView()
            miv.Text = astrView(i)
            miv.vu = aview(i)
            miv.RadioCheck = True
            AddHandler miv.Click, eh
            If i = 3 Then
                mivChecked = miv
                mivChecked.Checked = True
                filelist.View = mivChecked.vu
            End If
            Menu.MenuItems(0).MenuItems.Add(miv)
        Next i

        Menu.MenuItems(0).MenuItems.Add("-")

        ' View Refresh menu item
        Dim mi As New MenuItem("&Refresh", _
                        AddressOf MenuOnRefresh, Shortcut.F5)
        Menu.MenuItems(0).MenuItems.Add(mi)
    End Sub

    Private Sub DirectoryTreeViewOnAfterSelect(ByVal obj As Object, ByVal tvea As TreeViewEventArgs)
        filelist.ShowFiles(tvea.Node.FullPath)
    End Sub

    Private Sub MenuOnView(ByVal obj As Object, ByVal ea As EventArgs)
        mivChecked.Checked = False
        mivChecked = DirectCast(obj, MenuItemView)
        mivChecked.Checked = True
        filelist.View = mivChecked.vu
    End Sub

    Private Sub MenuOnRefresh(ByVal obj As Object, ByVal ea As EventArgs)
        dirtree.RefreshTree()
    End Sub
End Class

Class MenuItemView
    Inherits MenuItem

    Public vu As View
End Class
