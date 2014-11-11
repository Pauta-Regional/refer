//------------------------------------------------
// ColorFillDialog.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class ColorFillDialog : Form
{
    protected GroupPanel grppnl1, grppnl2;
    protected CheckBox chkbox;

    public ColorFillDialog()
    {
        Text = "Color/Fill Select";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        ControlBox = MinimizeBox = MaximizeBox = ShowInTaskbar = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;

        FlowLayoutPanel flow2 = new FlowLayoutPanel();
        flow2.Parent = flow;
        flow2.AutoSize = true;
        flow2.Anchor = AnchorStyles.None;

        grppnl1 = new GroupPanel();
        grppnl1.Parent = flow2;
        grppnl1.AutoSize = true;
        grppnl1.Text = "Color";

        grppnl2 = new GroupPanel();
        grppnl2.Parent = flow2;
        grppnl2.AutoSize = true;
        grppnl2.Text = "Background";
        
        grppnl1.SuspendLayout();
        grppnl2.SuspendLayout();

        // Get property information for SystemInformation class.
        Type type = typeof(Color);
        PropertyInfo[] apropinfo = type.GetProperties();

        // Loop through the property information.
        foreach (PropertyInfo pi in apropinfo)
        {
            if (pi.CanRead && pi.GetGetMethod().IsStatic)
            {
                // Get the property names and values.
                if (pi.Name[0] == 'S' || pi.Name[0] == 'P')
                {
                    RadioButton radio = new RadioButton();
                    radio.Parent = pi.Name[0] == 'S' ? grppnl1 : grppnl2;
                    radio.AutoSize = true;
                    radio.Text = radio.Name = pi.Name;
                }
            }
        }
        grppnl1.ResumeLayout();
        grppnl2.ResumeLayout();

        chkbox = new CheckBox();
        chkbox.Parent = flow;
        chkbox.AutoSize = true;
        chkbox.Text = "Fill Ellipse";
        chkbox.Anchor = AnchorStyles.None;

        FlowLayoutPanel flow3 = new FlowLayoutPanel();
        flow3.Parent = flow;
        flow3.AutoSize = true;
        flow3.Anchor = AnchorStyles.None;

        Button btn = new Button();
        btn.Parent = flow3;
        btn.AutoSize = true;
        btn.Text = "OK";
        btn.DialogResult = DialogResult.OK;
        AcceptButton = btn;

        btn = new Button();
        btn.Parent = flow3;
        btn.AutoSize = true;
        btn.Text = "Cancel";
        btn.DialogResult = DialogResult.Cancel;
        CancelButton = btn;
    }
    public Color Color
    {
        set { grppnl1.Check = value.Name; }
        get { return Color.FromName(grppnl1.Check); }
    }
    public Color Background
    {
        set { grppnl2.Check = value.Name; }
        get { return Color.FromName(grppnl2.Check); }
    }
    public bool Fill
    {
        set { chkbox.Checked = value; }
        get { return chkbox.Checked; }
    }
}