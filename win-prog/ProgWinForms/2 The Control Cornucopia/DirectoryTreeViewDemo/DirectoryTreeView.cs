//--------------------------------------------------
// DirectoryTreeView.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class DirectoryTreeView : TreeView
{
    public DirectoryTreeView()
    {
        // Get the images used in the tree.
        ImageList = new ImageList();
        ImageList.Images.Add(new Icon(GetType(), "Resource.CLSDFOLD.ICO"));
        ImageList.Images.Add(new Icon(GetType(), "Resource.OPENFOLD.ICO"));
        ImageList.Images.Add(new Icon(GetType(), "Resource.35FLOPPY.ICO"));
        ImageList.Images.Add(new Icon(GetType(), "Resource.CDDRIVE.ICO"));
        ImageList.Images.Add(new Icon(GetType(), "Resource.DRIVENET.ICO"));
        ImageIndex = 0;
        SelectedImageIndex = 1;

        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            // Create the drive node.
            TreeNode nodeDrive = new TreeNode(drive.RootDirectory.Name);

            // Set the image index depending on the drive type.
            if (drive.DriveType == DriveType.Removable)
                nodeDrive.ImageIndex = nodeDrive.SelectedImageIndex = 2;

            else if (drive.DriveType == DriveType.CDRom)
                nodeDrive.ImageIndex = nodeDrive.SelectedImageIndex = 3;

            else
                nodeDrive.ImageIndex = nodeDrive.SelectedImageIndex = 4;

            // Add the node to the tree and add the subdirectories.
            Nodes.Add(nodeDrive);
            AddDirectories(nodeDrive);

            // Make drive C the selected node.
            if (drive.RootDirectory.Name[0] == 'C')
                SelectedNode = nodeDrive;
        }
    }

    void AddDirectories(TreeNode node)
    {
        node.Nodes.Clear();

        DirectoryInfo dirinfo = new DirectoryInfo(node.FullPath);
        DirectoryInfo[] adirinfo;

        try
        {
            adirinfo = dirinfo.GetDirectories();
        }
        catch
        {
            return;
        }
        // Add node for each subdirectory.
        foreach (DirectoryInfo dir in adirinfo)
        {
            TreeNode nodeDir = new TreeNode(dir.Name);
            node.Nodes.Add(nodeDir);
        }
    }
    protected override void OnBeforeExpand(TreeViewCancelEventArgs args)
    {
        base.OnBeforeExpand(args);

        BeginUpdate();

        // Add the subdirectories of each subnode about to be displayed.
        foreach (TreeNode node in args.Node.Nodes)
            AddDirectories(node);

        EndUpdate();
    }
}
