//-----------------------------------------------------
// PrimevalFileExplorer.cs (c) 2005 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PrimevalFileExplorer : Form
{
    DirectoryTreeView tree;
    DirectoryListView list;

    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new PrimevalFileExplorer());
    }
    public PrimevalFileExplorer()
    {
        Text = "Primeval File Explorer";

        tree = new DirectoryTreeView();
        tree.Parent = this;
        tree.Dock = DockStyle.Left;
        tree.AfterSelect += TreeViewOnSelect;

        list = new DirectoryListView();
        list.Parent = this;
        list.Dock = DockStyle.Right;

        Width *= 2;
    }
    protected override void OnResize(EventArgs args)
    {
        base.OnResize(args);
        tree.Width = list.Width = Width / 2;
    }
    void TreeViewOnSelect(object objSrc, TreeViewEventArgs args)
    {
        list.Directory = args.Node.FullPath;
    }
}
