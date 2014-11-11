//------------------------------------------------
// FontDialogMimic.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FontDialogMimic : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FontDialogMimic());
    }
    public FontDialogMimic()
    {
        Text = "FontDialog Mimic";
        ResizeRedraw = true;
        ForeColor = Color.Red;

        Menu = new MainMenu();
        Menu.MenuItems.Add("&Format");
        Menu.MenuItems[0].MenuItems.Add("&Old Font Dialog...", OnOldFont);
        Menu.MenuItems[0].MenuItems.Add("&New Font Dialog...", OnNewFont);
    }
    void OnOldFont(object objSrc, EventArgs args)
    {
        FontDialog fntdlg = new FontDialog();
        fntdlg.Font = Font;
        fntdlg.Color = ForeColor;
        fntdlg.ShowColor = true;

        if (fntdlg.ShowDialog() == DialogResult.OK)
        {
            Font = fntdlg.Font;
            ForeColor = fntdlg.Color;
            Invalidate();
        }
    }
    void OnNewFont(object objSrc, EventArgs args)
    {
        NewFontDialog fntdlg = new NewFontDialog();
        fntdlg.Font = Font;
        fntdlg.Color = ForeColor;
        fntdlg.ShowColor = true;

        if (fntdlg.ShowDialog() == DialogResult.OK)
        {
            Font = fntdlg.Font;
            ForeColor = fntdlg.Color;
            Invalidate();
        }
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        StringFormat strfmt = new StringFormat();
        strfmt.LineAlignment = strfmt.Alignment = StringAlignment.Center;
        grfx.DrawString(Font.ToString(), Font, new SolidBrush(ForeColor), ClientRectangle, strfmt);
    }
}



