//------------------------------------------------
// CustomColorMenu.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class CustomColorMenu : Form
{
    ToolStripColorGrid clrgrd;
    ColorDialog clrdlg = new ColorDialog();

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new CustomColorMenu());
    }
    public CustomColorMenu()
    {
        Text = "Custom Color Menu";

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        ToolStripMenuItem itemFormat = new ToolStripMenuItem("&Format");
        itemFormat.DropDownOpening += FormatOnDropDownOpening;
        menu.Items.Add(itemFormat);

        ToolStripMenuItem itemBackground = new ToolStripMenuItem("&Background Color");
        itemFormat.DropDownItems.Add(itemBackground);

        clrgrd = new ToolStripColorGrid();
        clrgrd.Click += ColorGridOnClick;
        itemBackground.DropDownItems.Add(clrgrd);

        itemBackground.DropDownItems.Add(new ToolStripSeparator());

        ToolStripMenuItem item = new ToolStripMenuItem("&More Colors...");
        item.Click += MoreColorsOnClick;
        itemBackground.DropDownItems.Add(item);
    }
    void FormatOnDropDownOpening(object objSrc, EventArgs args)
    {
        clrgrd.SelectedColor = BackColor;
    }
    void ColorGridOnClick(object objSrc, EventArgs args)
    {
        BackColor = clrgrd.SelectedColor;
    }
    void MoreColorsOnClick(object objSrc, EventArgs args)
    {
        clrdlg.Color = BackColor;

        if (clrdlg.ShowDialog() == DialogResult.OK)
            BackColor = clrdlg.Color;
    }
}