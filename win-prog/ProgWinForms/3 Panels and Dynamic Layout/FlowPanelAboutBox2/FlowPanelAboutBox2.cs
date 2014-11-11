//---------------------------------------------------
// FlowPanelAboutBox2.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class FlowPanelAboutBox2 : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FlowPanelAboutBox2());
    }
    public FlowPanelAboutBox2()
    {
        Text = "Flow Panel About Box #2";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = new Icon(GetType(), "FlowPanelAboutBox2.AforAbout.ico");

        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.Parent = this;
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;

        FlowLayoutPanel flow2 = new FlowLayoutPanel();
        flow2.Parent = flow;
        flow2.AutoSize = true;
        flow2.Margin = new Padding(Font.Height);

        PictureBox picbox = new PictureBox();
        picbox.Parent = flow2;
        picbox.Image = Icon.ToBitmap();
        picbox.SizeMode = PictureBoxSizeMode.AutoSize;
        picbox.Anchor = AnchorStyles.None;

        Label lbl = new Label();
        lbl.Parent = flow2;
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.None;
        lbl.Text = "AboutBox Version 2.0";
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