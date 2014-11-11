'--------------------------------------------------
' DirectoryTreeView.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class DirectoryTreeView
    Inherits TreeView

    Sub New()
        ' Make a little more room for long directory names.
        Width *= 2

        ' Get images for tree.
        ImageList = New ImageList()
        ImageList.Images.Add(New Bitmap(Me.GetType(), "35FLOPPY.BMP"))
        ImageList.Images.Add(New Bitmap(Me.GetType(), "CLSDFOLD.BMP"))
        ImageList.Images.Add(New Bitmap(Me.GetType(), "OPENFOLD.BMP"))

        ' Construct tree.
        RefreshTree()
    End Sub

    Sub RefreshTree()
        ' Turn off visual updating and clear tree.
        BeginUpdate()
        Nodes.Clear()

        ' Make disk drives the root nodes. 
        Dim astrDrives() As String = Directory.GetLogicalDrives()
        Dim str As String
        For Each str In astrDrives
            Dim tnDrive As New TreeNode(str, 0, 0)
            Nodes.Add(tnDrive)
            AddDirectories(tnDrive)
            If str = "C:\" Then
                SelectedNode = tnDrive
            End If
        Next str
        EndUpdate()
    End Sub

    Private Sub AddDirectories(ByVal tn As TreeNode)
        tn.Nodes.Clear()
        Dim strPath As String = tn.FullPath
        Dim dirinfo As New DirectoryInfo(strPath)
        Dim adirinfo() As DirectoryInfo

        ' Avoid message box reporting drive A has no diskette!
        If Not dirinfo.Exists Then Return

        Try
            adirinfo = dirinfo.GetDirectories()
        Catch
            Return
        End Try

        Dim di As DirectoryInfo
        For Each di In adirinfo
            Dim tnDir As New TreeNode(di.Name, 1, 2)
            tn.Nodes.Add(tnDir)

            ' We could now fill up the whole tree with this statement:
            '         AddDirectories(tnDir)
            ' But it would be too slow. Try it!

        Next di
    End Sub

    Protected Overrides Sub OnBeforeExpand(ByVal tvcea As TreeViewCancelEventArgs)
        MyBase.OnBeforeExpand(tvcea)
        BeginUpdate()

        Dim tn As TreeNode
        For Each tn In tvcea.Node.Nodes
            AddDirectories(tn)
        Next tn
        EndUpdate()
    End Sub
End Class
