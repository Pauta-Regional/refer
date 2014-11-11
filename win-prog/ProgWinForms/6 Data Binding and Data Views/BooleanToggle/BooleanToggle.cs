//----------------------------------------------
// BooleanToggle.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class BooleanToggle : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new BooleanToggle());
    }
    public BooleanToggle()
    {
        Text = "Boolean Toggle";

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.Dock = DockStyle.Fill;
        flow.FlowDirection = FlowDirection.TopDown;

        CheckBox chkbox = new CheckBox();
        chkbox.Parent = flow;
        chkbox.Text = "Minimize Box";
        chkbox.AutoSize = true;
        chkbox.DataBindings.Add("Checked", this, "MinimizeBox");
        chkbox.DataBindings[0].DataSourceUpdateMode = 
            DataSourceUpdateMode.OnPropertyChanged;

        chkbox = new CheckBox();
        chkbox.Parent = flow;
        chkbox.Text = "Maximize Box";
        chkbox.AutoSize = true;
        chkbox.DataBindings.Add("Checked", this, "MaximizeBox");
        chkbox.DataBindings[0].DataSourceUpdateMode =
            DataSourceUpdateMode.OnPropertyChanged;

        chkbox = new CheckBox();
        chkbox.Parent = flow;
        chkbox.Text = "Control Box";
        chkbox.AutoSize = true;
        chkbox.DataBindings.Add("Checked", this, "ControlBox");
        chkbox.DataBindings[0].DataSourceUpdateMode =
            DataSourceUpdateMode.OnPropertyChanged;
    }
}
