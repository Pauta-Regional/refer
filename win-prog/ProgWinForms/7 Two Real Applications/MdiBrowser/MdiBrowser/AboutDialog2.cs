//--------------------------------------------
// AboutDialog2.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//--------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class AboutDialog2 : AboutDialog
{
    public AboutDialog2(string strResource): base(strResource)
    {
        // Add a Label control.
        Label lbl = new Label();
        lbl.Parent = flow;
        lbl.Font = new Font(FontFamily.GenericSerif, 14);
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.None;
        lbl.Text = "Using Microsoft Internet Explorer " + 
            new WebBrowser().Version.ToString();

        // Make OK button the last control.
        btnOk.SendToBack();
    }
}
