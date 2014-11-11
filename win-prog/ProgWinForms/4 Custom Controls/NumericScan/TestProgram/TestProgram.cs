//--------------------------------------------
// TestProgram.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using Petzold.ProgrammingWindowsForms;
using System;
using System.Drawing;
using System.Windows.Forms;

class TestProgram : Form
{
    Label lbl;
    NumericScan numscan1, numscan2;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new TestProgram());
    }
    public TestProgram()
    {
        Text = "Test Program";

        FlowLayoutPanel pnl = new FlowLayoutPanel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;

        numscan1 = new NumericScan();
        numscan1.Parent = pnl;
        numscan1.AutoSize = true;
        numscan1.ValueChanged += NumericScanOnValueChanged;

        numscan2 = new NumericScan();
        numscan2.Parent = pnl;
        numscan2.AutoSize = true;
        numscan2.ValueChanged += NumericScanOnValueChanged;

        lbl = new Label();
        lbl.Parent = pnl;
        lbl.AutoSize = true;
    }
    void NumericScanOnValueChanged(object objSrc, EventArgs args)
    {
        lbl.Text = "First: " + numscan1.Value + ", Second: " + numscan2.Value;
    }
}

