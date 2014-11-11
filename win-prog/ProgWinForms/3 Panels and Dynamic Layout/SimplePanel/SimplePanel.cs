//--------------------------------------------
// SimplePanel.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SimplePanel : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimplePanel());
    }
    public SimplePanel()
    {
        Text = "Simple Panel";

        Panel pnl = new Panel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;
        pnl.AutoScroll = true;

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

        Label lbl = new Label();
        lbl.Parent = pnl;
        lbl.AutoSize = true;
        lbl.Text = "Label control at top of panel";

        lbl = new Label();
        lbl.Parent = pnl;
        lbl.AutoSize = true;
        lbl.Text = "Label control at bottom of panel";
        lbl.Location = new Point(300, 300);
    }
}