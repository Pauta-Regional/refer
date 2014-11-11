//---------------------------------------------------
// FlowPanelAlignment.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FlowPanelAlignment : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FlowPanelAlignment());
    }
    public FlowPanelAlignment()
    {
        Text = "Flow Panel Alignment";

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.Dock = DockStyle.Fill;
        flow.Text = "Flow Panel";
        flow.Click += ClickHandler;

        Random rand = new Random(DateTime.Now.Millisecond);

        for (int i = 0; i < 20; i++)
        {
            Button btn = new Button();
            btn.Parent = flow;
            btn.Text = "Button " + (i + 1);
            btn.Click += ClickHandler;

            // Set a random size (but not too random)
            Size sz = btn.PreferredSize;
            sz.Width = (int)(sz.Width * (1 + 2 * rand.NextDouble()));
            sz.Height = (int)(sz.Height * (1 + 2 * rand.NextDouble()));
            btn.Size = sz;
        }
    }
    void ClickHandler(object objSrc, EventArgs args)
    {
        Control ctrl = (Control)objSrc;

        Form frm = new Form();
        frm.Text = ctrl.Text;
        frm.Owner = this;

        PropertyGrid prop = new PropertyGrid();
        prop.SelectedObject = objSrc;
        prop.Parent = frm;
        prop.Dock = DockStyle.Fill;

        frm.Show();
    }
}