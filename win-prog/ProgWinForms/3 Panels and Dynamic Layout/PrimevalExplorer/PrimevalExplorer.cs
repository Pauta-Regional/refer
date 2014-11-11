//-------------------------------------------------
// PrimevalExplorer.cs (c) 2005 by Charles Petzold
//-------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PrimevalExplorer : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new PrimevalExplorer());
    }
    public PrimevalExplorer()
    {
        Text = "PrimevalExplorer";

        SplitContainer split = new SplitContainer();
        split.Parent = this;
        split.Dock = DockStyle.Fill;

        TreeView tree = new TreeView();
        tree.Parent = split.Panel1;
        tree.Dock = DockStyle.Fill;
        tree.Nodes.Add("tree");

        ListView list = new ListView();
        list.Parent = split.Panel2;
        list.Dock = DockStyle.Fill;
        list.Items.Add("list");
    }
}