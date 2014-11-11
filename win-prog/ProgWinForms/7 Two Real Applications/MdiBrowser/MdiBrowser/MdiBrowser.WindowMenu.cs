//------------------------------------------------------
// MdiBrowser.WindowMenu.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser : Form
{
    ToolStripMenuItem WindowMenu()
    {
        ToolStripMenuItem itemWindow = new ToolStripMenuItem("&Window");
        menu.MdiWindowListItem = itemWindow;

        ToolStripMenuItem item = new ToolStripMenuItem("&Cascade");
        item.Tag = MdiLayout.Cascade;
        item.Click += WindowArrangeOnClick;
        itemWindow.DropDownItems.Add(item);

        item = new ToolStripMenuItem("Tile &Horizontal");
        item.Tag = MdiLayout.TileHorizontal;
        item.Click += WindowArrangeOnClick;
        itemWindow.DropDownItems.Add(item);

        item = new ToolStripMenuItem("Tile &Vertical");
        item.Tag = MdiLayout.TileVertical;
        item.Click += WindowArrangeOnClick;
        itemWindow.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Arrange Icons");
        item.Tag = MdiLayout.ArrangeIcons;
        item.Click += WindowArrangeOnClick;
        itemWindow.DropDownItems.Add(item);

        itemWindow.DropDownItems.Add(new ToolStripSeparator());

        item = new ToolStripMenuItem("C&lose All Windows");
        item.Click += CloseAllOnClick;
        itemWindow.DropDownItems.Add(item);

        itemWindow.DropDownItems.Add(new ToolStripSeparator());

        return itemWindow;
    }
    // Two small Click handlers.
    void WindowArrangeOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = objSrc as ToolStripMenuItem;
        LayoutMdi((MdiLayout)item.Tag);
    }
    void CloseAllOnClick(object objSrc, EventArgs args)
    {
        while (MdiChildren.Length > 0)
            MdiChildren[0].Close();
    }
}
