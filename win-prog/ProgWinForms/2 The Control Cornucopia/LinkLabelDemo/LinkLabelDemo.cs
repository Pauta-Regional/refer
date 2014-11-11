//----------------------------------------------
// LinkLabelDemo.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

class LinkLabelDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new LinkLabelDemo());
    }
    public LinkLabelDemo()
    {
        Text = "LinkLabel Demo";
        Font = new Font("Times New Roman", 14);

        LinkLabel lnklbl = new LinkLabel();
        lnklbl.Parent = this;
        lnklbl.Dock = DockStyle.Fill;
        lnklbl.LinkClicked += LinkLabelOnLinkClicked;

        lnklbl.Text = "Jane Austen Societies exist in North America, the " +
                        "United Kingdom, and Australia, among other places.";

        string str = "North America";
        lnklbl.Links.Add(lnklbl.Text.IndexOf(str), str.Length, 
                            "http://www.jasna.org");

        str = "United Kingdom";
        lnklbl.Links.Add(lnklbl.Text.IndexOf(str), str.Length,
                            "http://www.janeaustensoci.freeuk.com");

        str = "Australia";
        lnklbl.Links.Add(lnklbl.Text.IndexOf(str), str.Length,
                            "http://www.jasa.net.au");
    }
    void LinkLabelOnLinkClicked(object objSrc, LinkLabelLinkClickedEventArgs args)
    { 
        LinkLabel.Link lnk = args.Link;
        string strLink = lnk.LinkData as string;

        Process.Start(strLink);
    }
}
