//-----------------------------------------------------
// ContextMenuStripDemo.cs (c) 2005 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class ContextMenuStripDemo : Form
{
    ToolStripMenuItem itemChecked;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ContextMenuStripDemo());
    }
    public ContextMenuStripDemo()
    {
        Text = "ContextMenuStrip Demo";
        BackColor = Color.White;

        // Division of colors alphabetically.
        int[] iMenu = { 0,0,0,1,2,2,2,2,2,2,2,3,4,5,5,5,5,5,6,6,6,6,6,6,6,6 };

        // Create ContextMenuStrip and attach to this form via the
        //  ContextMenuStrip property of Control.
        ContextMenuStrip menu = new ContextMenuStrip();
        ContextMenuStrip = menu;

        // Top level items show a range of letters.
        for (int i = 0; i <= 6; i++)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            char chFirst = Convert.ToChar(Array.IndexOf(iMenu, i) + 'A');
            char chLast = Convert.ToChar(Array.LastIndexOf(iMenu, i) + 'A');
            item.Text = String.Format("Colors {0} to {1}", chFirst, chLast);
            ((ToolStripDropDownMenu)item.DropDown).ShowCheckMargin = true;
            menu.Items.Add(item);
        }

        // Obtain array of PropertyInfo objects with Color properties.
        PropertyInfo[] api = typeof(Color).GetProperties();

        // Make each color into a ToolStripMenuItem.
        foreach (PropertyInfo pi in api)
        {
            if (pi.CanRead && pi.PropertyType == typeof(Color))
            {
                Color clr = (Color)pi.GetValue(null, null);
                int i = iMenu[clr.Name[0] - 'A'];

                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = CamelSpaceOut(clr.Name);
                item.Name = clr.Name;
                item.Image = CreateBitmap(clr);
                item.Click += ColorOnClick;
                ((ToolStripMenuItem)menu.Items[i]).DropDownItems.Add(item);

                if (clr.Equals(BackColor))
                    (itemChecked = item).Checked = true;
            }
        }
    }
    void ColorOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = (ToolStripMenuItem) objSrc;
        itemChecked.Checked = false;
        (itemChecked = item).Checked = true;
        BackColor = Color.FromName(itemChecked.Name);
    }
    Bitmap CreateBitmap(Color clr)
    {
        Bitmap bm = new Bitmap(16, 16);
        Graphics grfx = Graphics.FromImage(bm);
        grfx.FillRectangle(new SolidBrush(clr), 0, 0, 16, 16);
        grfx.Dispose();
        return bm;
    }
    string CamelSpaceOut(string str)
    {
        for (int i = 1; i < str.Length; i++)
            if (Char.IsUpper(str[i]))
                str = str.Insert(i++, " ");
        return str;
    }
}