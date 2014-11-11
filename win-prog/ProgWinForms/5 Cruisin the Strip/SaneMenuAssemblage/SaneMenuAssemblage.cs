//---------------------------------------------------
// SaneMenuAssemblage.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SaneMenuAssemblage : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SaneMenuAssemblage());
    }
    public SaneMenuAssemblage()
    {
        Text = "Sane Menu Assemblage";

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        // Assemble File menu.
        ToolStripMenuItem itemFile = new ToolStripMenuItem("&File");
        menu.Items.Add(itemFile);

        ToolStripMenuItem item = new ToolStripMenuItem("&Open...");
        item.ShortcutKeys = Keys.Control | Keys.O;
        item.Click += DefaultOnClick;
        itemFile.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Save");
        item.ShortcutKeys = Keys.Control | Keys.S;
        item.Click += DefaultOnClick;
        itemFile.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Close");
        item.Click += DefaultOnClick;
        itemFile.DropDownItems.Add(item);

        // Assemble Edit menu.
        ToolStripMenuItem itemEdit = new ToolStripMenuItem("&Edit");
        menu.Items.Add(itemEdit);
        
        item = new ToolStripMenuItem("Cu&t");
        item.ShortcutKeys = Keys.Control | Keys.X;
        item.Click += DefaultOnClick;
        itemEdit.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Copy");
        item.ShortcutKeys = Keys.Control | Keys.C;
        item.Click += DefaultOnClick;
        itemEdit.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Paste");
        item.ShortcutKeys = Keys.Control | Keys.V;
        item.Click += DefaultOnClick;
        itemEdit.DropDownItems.Add(item);

        // Assemble Help menu.
        ToolStripMenuItem itemHelp = new ToolStripMenuItem("&Help");
        menu.Items.Add(itemHelp);
        
        item = new ToolStripMenuItem("&Help");
        item.ShortcutKeys = Keys.F1;
        item.Click += DefaultOnClick;
        itemHelp.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&About...");
        item.Click += DefaultOnClick;
        itemHelp.DropDownItems.Add(item);
    }
    void DefaultOnClick(object obj, EventArgs args)
    {
        MessageBox.Show("Menu item not yet implemented", Text);
    }
}