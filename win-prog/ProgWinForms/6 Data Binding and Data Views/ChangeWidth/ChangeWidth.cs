//--------------------------------------------
// ChangeWidth.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ChangeWidth: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ChangeWidth());
    }
    public ChangeWidth()
    {
        Text = "Change Width";

        VScrollBar scrl = new VScrollBar();
        scrl.Parent = this;
        scrl.Dock = DockStyle.Left;
        scrl.Minimum = SystemInformation.MinimumWindowSize.Width;
        scrl.Maximum = SystemInformation.MaxWindowTrackSize.Width;
        scrl.Value = Width;

        // Changes in Size.Width change scrollbar Value.
        scrl.DataBindings.Add("Value", this, "Size.Width");
        scrl.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.Never;

        // Changes in scrollbar Value change Width.
        DataBindings.Add("Width", scrl, "Value");
        DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.Never;
    }
}
