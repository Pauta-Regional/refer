//-----------------------------------------
// FontMenu.cs (c) 2005 by Charles Petzold
//-----------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FontMenu : Form
{
    Panel pnl;
    ToolStripMenuItem itemSelectedFont;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FontMenu());
    }
    public FontMenu()
    {
        Text = "Font Menu";

        pnl = new Panel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;
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
                {
                    itemSelectedFont = item;
                    itemSelectedFont.Checked = true;
                }
            }
        }
        grfx.Dispose();
    }
    void PanelOnPaint(object objSrc, PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        Font fnt = new Font(itemSelectedFont.Text, 48);
        grfx.DrawString(fnt.Name, fnt, new SolidBrush(ForeColor), 0, 0);
    }
    void FontOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = (ToolStripMenuItem)objSrc;
        itemSelectedFont.Checked = false;
        itemSelectedFont = item;
        itemSelectedFont.Checked = true;
        pnl.Invalidate();
    }
}

