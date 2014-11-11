//------------------------------------------
// ColorFill.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorFill : Form
{
    Color clrEllipse = Color.Salmon;
    bool bFillEllipse = false;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ColorFill());
    }
    public ColorFill()
    {
        Text = "Color Fill";
        ResizeRedraw = true;
        BackColor = Color.PowderBlue;

        Menu = new MainMenu();
        Menu.MenuItems.Add("&Options");
        Menu.MenuItems[0].MenuItems.Add("&Color...", MenuColorOnClick);
    }
    void MenuColorOnClick(object objSrc, EventArgs args)
    {
        ColorFillDialog dlg = new ColorFillDialog();
        dlg.Color = clrEllipse;
        dlg.Fill = bFillEllipse;
        dlg.Background = BackColor;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            clrEllipse = dlg.Color;
            bFillEllipse = dlg.Fill;
            BackColor = dlg.Background;
            Invalidate();
        }
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        Rectangle rect = new Rectangle(0, 0, ClientSize.Width - 1,
                                             ClientSize.Height - 1);
        if (bFillEllipse)
            grfx.FillEllipse(new SolidBrush(clrEllipse), rect);
        else
            grfx.DrawEllipse(new Pen(clrEllipse), rect);
    }
}