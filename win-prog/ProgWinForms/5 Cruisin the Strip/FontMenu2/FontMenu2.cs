//------------------------------------------
// FontMenu2.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FontMenu2 : Form
{
    Panel pnl;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FontMenu2());
    }
    public FontMenu2()
    {
        Text = "Font Menu #2";

        pnl = new Panel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;
        pnl.Font = new Font(pnl.Font.Name, 24);
        pnl.Paint += PanelOnPaint;

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        ToolStripMenuItem itemFormat = new ToolStripMenuItem("&Format");
        menu.Items.Add(itemFormat);

        ToolStripMenuItem itemFont = new ToolStripMenuItem("&Font");
        itemFormat.DropDownItems.Add(itemFont);

        Graphics grfx = CreateGraphics();

        foreach (FontFamily fntfam in FontFamily.GetFamilies(grfx))
        {
            if (fntfam.IsStyleAvailable(FontStyle.Regular))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(fntfam.Name);
                item.Click += FontOnClick;
                itemFont.DropDownItems.Add(item);

                if (fntfam.Name == Font.Name)
                    item.Checked = true;
            }
        }
        grfx.Dispose();
    }
    void PanelOnPaint(object objSrc, PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        grfx.DrawString(pnl.Font.Name, pnl.Font, new SolidBrush(ForeColor), 0, 0);
    }
    void FontOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem itemClick = (ToolStripMenuItem)objSrc;
        ToolStripMenuItem itemFont = (ToolStripMenuItem)
            ((ToolStripDropDownMenu) itemClick.Owner).OwnerItem;

        foreach (ToolStripMenuItem item in itemFont.DropDownItems)
            item.Checked = false;

        itemClick.Checked = true;
        pnl.Font = new Font(itemClick.Text, 24);
    }
}

