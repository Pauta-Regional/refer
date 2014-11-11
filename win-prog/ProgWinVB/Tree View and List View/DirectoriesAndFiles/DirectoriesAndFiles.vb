'----------------------------------------------------
' DirectoriesAndFiles.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class DirectoriesAndFiles
    Inherits Form

    Private dirtree As DirectoryTreeView
    Private pnl As Panel
    Private tnSelect As TreeNode

    Shared Sub Main()
        Application.Run(New DirectoriesAndFiles())
    End Sub

    Sub New()
        Text = "Directories and Files"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        pnl = New Panel()
        pnl.Parent = Me
        pnl.Dock = DockStyle.Fill
        AddHandler pnl.Paint, AddressOf PanelOnPaint

        Dim split As New Splitter()
        split.Parent = Me
        split.Dock = DockStyle.Left
        split.BackColor = SystemColors.Control

        dirtree = New DirectoryTreeView()
        dirtree.Parent = Me
        dirtree.Dock = DockStyle.Left
        AddHandler dirtree.AfterSelect, _
                                AddressOf DirectoryTreeViewOnAfterSelect

        ' Create menu with one item.
        Menu = New MainMenu()
        Menu.MenuItems.Add("View")
        Dim mi As New MenuItem("Refresh", _
                        AddressOf MenuOnRefresh, Shortcut.F5)
        Menu.MenuItems(0).MenuItems.Add(mi)
    End Sub

    Private Sub DirectoryTreeViewOnAfterSelect(ByVal obj As Object, _
                                    ByVal tvea As TreeViewEventArgs)
        tnSelect = tvea.Node
        pnl.Invalidate()
    End Sub

    Private Sub PanelOnPaint(ByVal obj As Object, _
                             ByVal pea As PaintEventArgs)
        If tnSelect Is Nothing Then Return

        Dim pnl As Panel = DirectCast(obj, Panel)
        Dim grfx As Graphics = pea.Graphics
        Dim dirinfo As New DirectoryInfo(tnSelect.FullPath)
        Dim fi, afi() As FileInfo
        Dim br As New SolidBrush(pnl.ForeColor)
        Dim y As Integer = 0

        Try
            afi = dirinfo.GetFiles()
        Catch
            Return
        End Try

        For Each fi In afi
            grfx.DrawString(fi.Name, Font, br, 0, y)
            y += Font.Height
        Next fi
    End Sub

    Private Sub MenuOnRefresh(ByVal obj As Object, ByVal ea As EventArgs)
        dirtree.RefreshTree()
    End Sub
End Class
