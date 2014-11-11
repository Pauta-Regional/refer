//-------------------------------------------------------
// NaiveDirectoryTreeView.cs (c) 2005 by Charles Petzold
//-------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class NaiveDirectoryTreeView : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new NaiveDirectoryTreeView());
    }
    public NaiveDirectoryTreeView()
    {
        Text = "Naive Directory TreeView";

        TreeView tree = new TreeView();
        tree.Parent = this;
        tree.Dock = DockStyle.Fill;

        TreeNode nodeDriveC = new TreeNode("C:\\");
        tree.Nodes.Add(nodeDriveC);

        AddDirectories(nodeDriveC);
    }
    void AddDirectories(TreeNode node)
    {
        string strPath = node.FullPath;
        DirectoryInfo dirinfo = new DirectoryInfo(strPath);
        DirectoryInfo[] adirinfo;

        try
        {
            adirinfo = dirinfo.GetDirectories();
        }
        catch
        {
            return;
        }

        foreach (DirectoryInfo di in adirinfo)
        {
            TreeNode nodeDir = new TreeNode(di.Name);
            node.Nodes.Add(nodeDir);
            AddDirectories(nodeDir);
        }
    }
}
