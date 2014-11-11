//-------------------------------------------
// SimpleMenu.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SimpleMenu: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimpleMenu());
    }
    public SimpleMenu()
    {
        Text = "Simple Menu";

        // Main menu.
        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        // File item.
        ToolStripMenuItem itemFile = new ToolStripMenuItem();
        itemFile.Text = "&File";
        menu.Items.Add(itemFile);

        // Open item.
        ToolStripMenuItem itemOpen = new ToolStripMenuItem();
        itemOpen.Text = "&Open...";
        itemOpen.ShortcutKeys = Keys.Control | Keys.O;
        itemOpen.Click += OpenOnClick;
        itemFile.DropDownItems.Add(itemOpen);

        // Separator.
        ToolStripSeparator itemSep = new ToolStripSeparator();
        itemFile.DropDownItems.Add(itemSep);

        // Exit item.
        ToolStripMenuItem itemExit = new ToolStripMenuItem();
        itemExit.Text = "E&xit";
        itemExit.Click += ExitOnClick;
        itemFile.DropDownItems.Add(itemExit);

        // Help item.
        ToolStripMenuItem itemHelp = new ToolStripMenuItem();
        itemHelp.Text = "&Help";
        menu.Items.Add(itemHelp);

        // About item.
        ToolStripMenuItem itemAbout = new ToolStripMenuItem();
        itemAbout.Text = "&About...";
        itemAbout.Click += AboutOnClick;
        itemHelp.DropDownItems.Add(itemAbout);
    }
    void OpenOnClick(object objSrc, EventArgs args)
    {
        MessageBox.Show("\"Open\" feature not yet implemented", Text);
    }
    void ExitOnClick(object objSrc, EventArgs args)
    {
        Close();
    }
    void AboutOnClick(object objSrc, EventArgs args)
    {
        MessageBox.Show("(c) 2005 by Charles Petzold", Text);
    }
}







