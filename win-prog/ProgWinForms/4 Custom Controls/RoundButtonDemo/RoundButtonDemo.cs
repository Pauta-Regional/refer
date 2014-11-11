//-----------------------------------------------
// RoundButtonDemo.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class RoundButtonDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new RoundButtonDemo());
    }
    public RoundButtonDemo()
    {
        Text = "RoundButton Demonstration";
        Font = new Font("Times New Roman", 18);
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;

        FlowLayoutPanel flowTop = new FlowLayoutPanel();
        flowTop.Parent = flow;
        flowTop.AutoSize = true;
        flowTop.Anchor = AnchorStyles.None;

        Label lbl = new Label();
        lbl.Parent = flowTop;
        lbl.AutoSize = true;
        lbl.Text = "Enter some text:";
        lbl.Anchor = AnchorStyles.None;

        TextBox txtbox = new TextBox();
        txtbox.Parent = flowTop;
        txtbox.AutoSize = true;

        FlowLayoutPanel flowBottom = new FlowLayoutPanel();
        flowBottom.Parent = flow;
        flowBottom.AutoSize = true;
        flowBottom.Anchor = AnchorStyles.None;

        RoundButton btnOk = new RoundButton();
        btnOk.Parent = flowBottom;
        btnOk.Text = "OK";
        btnOk.Anchor = AnchorStyles.None;
        btnOk.DialogResult = DialogResult.OK;
        AcceptButton = btnOk;

        RoundButton btnCancel = new RoundButton();
        btnCancel.Parent = flowBottom;
        btnCancel.AutoSize = true;
        btnCancel.Text = "Cancel";
        btnCancel.Anchor = AnchorStyles.None;
        btnCancel.DialogResult = DialogResult.Cancel;
        CancelButton = btnCancel;

        btnOk.Size = btnCancel.Size;
    }
}
