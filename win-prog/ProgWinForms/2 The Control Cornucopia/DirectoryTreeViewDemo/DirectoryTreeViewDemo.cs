//------------------------------------------------------
// DirectoryTreeViewDemo.cs (c) 2005 by Charles Petzold
//------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class DirectoryTreeViewDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DirectoryTreeViewDemo());
    }
    public DirectoryTreeViewDemo()
    {
        Text = "DirectoryTreeView Demo";

        DirectoryTreeView tree = new DirectoryTreeView();
        tree.Parent = this;
        tree.Dock = DockStyle.Fill;
    }
}
