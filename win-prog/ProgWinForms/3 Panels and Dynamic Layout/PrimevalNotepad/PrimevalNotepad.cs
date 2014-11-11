//------------------------------------------------
// PrimevalNotepad.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PrimevalNotepad : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new PrimevalNotepad());
    }
    public PrimevalNotepad()
    {
        Text = "Primeval Notepad";

        TextBox txtbox = new TextBox();
        txtbox.Parent = this;
        txtbox.Dock = DockStyle.Fill;
        txtbox.Multiline = true;

        TreeView tree = new TreeView();
        tree.Parent = this;
        tree.Dock = DockStyle.Left;
        tree.Nodes.Add("tree");

        StatusStrip stat = new StatusStrip();
        stat.Parent = this;
        stat.Items.Add("status");

        ToolStrip tool = new ToolStrip();
        tool.Parent = this;
        tool.Items.Add("tool");

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;
        menu.Items.Add("menu");
    }
}