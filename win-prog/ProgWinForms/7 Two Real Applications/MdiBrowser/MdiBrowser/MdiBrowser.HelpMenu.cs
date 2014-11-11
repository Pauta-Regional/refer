//----------------------------------------------------
// MdiBrowser.HelpMenu.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser : Form
{
    ToolStripMenuItem HelpMenu()
    {
        ToolStripMenuItem itemHelp = new ToolStripMenuItem("&Help");

        ToolStripMenuItem item = new ToolStripMenuItem("&Help");
        item.Click += HelpOnClick;
        itemHelp.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&About MDI Browser...");
        item.Click += AboutOnClick;
        itemHelp.DropDownItems.Add(item);

        return itemHelp;
    }
    void HelpOnClick(object objSrc, EventArgs args)
    {
        Help.ShowHelp(this, "MdiBrowser.chm");
    }
    void AboutOnClick(object objSrc, EventArgs args)
    {
        AboutDialog2 dlg = new AboutDialog2("MdiBrowser");
        dlg.ShowDialog();
    }
}
