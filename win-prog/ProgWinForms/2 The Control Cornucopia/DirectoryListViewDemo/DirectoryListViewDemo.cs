//------------------------------------------------------
// DirectoryListViewDemo.cs (c) 2005 by Charles Petzold
//------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class DirectoryListViewDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DirectoryListViewDemo());
    }
    public DirectoryListViewDemo()
    {
        Text = "DirectoryListView Demo";
        Width *= 2;

        DirectoryListView dirlv = new DirectoryListView();
        dirlv.Parent = this;
        dirlv.Dock = DockStyle.Fill;
        dirlv.View = View.Details;
        dirlv.Directory = "C:";
    }
}
