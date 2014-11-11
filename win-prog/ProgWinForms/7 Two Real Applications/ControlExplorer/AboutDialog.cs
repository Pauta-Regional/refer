//--------------------------------------------
// AboutDialog.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class AboutDialog : Form
{
    protected FlowLayoutPanel flow;
    protected Button btnOk;

    public AboutDialog(string strResource)
    {
        // Get current assembly.
        Assembly a = GetType().Assembly;

        // Get program title.
        AssemblyTitleAttribute asmblytitle = (AssemblyTitleAttribute) 
            a.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
        string strTitle = asmblytitle.Title;

        // Get program version.
        AssemblyFileVersionAttribute asmblyvers = (AssemblyFileVersionAttribute)
            a.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
        string strVersion = asmblyvers.Version.Substring(0, 3);

        // Get program copyright.
        AssemblyCopyrightAttribute asmblycopy = (AssemblyCopyrightAttribute)
            a.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
        string strCopyright = asmblycopy.Copyright;

        // Set multititudes of attributes.
        Text = "About " + strTitle;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        ControlBox = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        Icon = ActiveForm.Icon;
        StartPosition = FormStartPosition.Manual;
        Location = ActiveForm.Location + SystemInformation.CaptionButtonSize +
                                         SystemInformation.FrameBorderSize;
        // Create controls.
        flow = new FlowLayoutPanel();
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
        lbl.Text = strTitle + " Version " + strVersion;
        lbl.Font = new Font(FontFamily.GenericSerif, 24, FontStyle.Italic);

        lbl = new Label();
        lbl.Parent = flow;
        lbl.Text = "From the Microsoft Press book:";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.None;
        lbl.Margin = new Padding(Font.Height);
        lbl.Font = new Font(FontFamily.GenericSerif, 16);

        picbox = new PictureBox();
        picbox.Parent = flow;
        picbox.Image = new Bitmap(GetType(), strResource + ".BookCover.png");
        picbox.SizeMode = PictureBoxSizeMode.AutoSize;
        picbox.Anchor = AnchorStyles.None;

        LinkLabel lnk = new LinkLabel();
        lnk.Parent = flow;
        lnk.AutoSize = true;
        lnk.Anchor = AnchorStyles.None;
        lnk.Margin = new Padding(Font.Height);
        lnk.Text = "\x00A9 2005 by Charles Petzold";
        lnk.Font = lbl.Font; // new Font(FontFamily.GenericSerif, 16);
        lnk.LinkArea = new LinkArea(10, 15);
        lnk.LinkClicked += 
            delegate { Process.Start("http://www.charlespetzold.com"); };

        btnOk = new Button();
        btnOk.Text = "OK";
        btnOk.Parent = flow;
        btnOk.AutoSize = true;
        btnOk.Anchor = AnchorStyles.None;
        btnOk.Margin = new Padding(Font.Height);
        btnOk.DialogResult = DialogResult.OK;
    }
}
