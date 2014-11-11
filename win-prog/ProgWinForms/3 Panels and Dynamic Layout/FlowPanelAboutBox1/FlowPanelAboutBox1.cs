//---------------------------------------------------
// FlowPanelAboutBox1.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class FlowPanelAboutBox1 : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FlowPanelAboutBox1());
    }
    public FlowPanelAboutBox1()
    {
        Text = "Flow Panel About Box #1";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FormBorderStyle = FormBorderStyle.FixedDialog;

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;

        Label lbl = new Label();
        lbl.Parent = flow;
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.None;
        lbl.Margin = new Padding(Font.Height);
        lbl.Text = "AboutBox Version 1.0";
        lbl.Font = new Font(FontFamily.GenericSerif, 24, FontStyle.Italic);

        LinkLabel lnk = new LinkLabel();
        lnk.Parent = flow;
        lnk.AutoSize = true;
        lnk.Anchor = AnchorStyles.None;
        lnk.Margin = new Padding(Font.Height);
        lnk.Text = "\x00A9 2005 by Charles Petzold";
        lnk.Font = new Font(FontFamily.GenericSerif, 16);
        lnk.LinkArea = new LinkArea(10, 15);
        lnk.LinkClicked += 
            delegate { Process.Start("http://www.charlespetzold.com"); };

        Button btn = new Button();
        btn.Parent = flow;
        btn.AutoSize = true;
        btn.Anchor = AnchorStyles.None;
        btn.Margin = new Padding(Font.Height);
        btn.Text = "OK";
    }
}