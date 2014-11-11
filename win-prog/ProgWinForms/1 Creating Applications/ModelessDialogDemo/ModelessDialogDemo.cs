//---------------------------------------------------
// ModelessDialogDemo.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ModelessDialogDemo: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ModelessDialogDemo());
    }
    public ModelessDialogDemo()
    {
        Text = "Modeless Dialog Demo";

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Change Color";
        btn.Location = new Point(16, 16);
        btn.AutoSize = true;
        btn.Click += ButtonOnClick;
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        ModelessDialogBox dlg = new ModelessDialogBox();

        dlg.Owner = this;
        dlg.Change += DialogOnChange;
        dlg.Show();
    }
    void DialogOnChange(object objSrc, EventArgs args)
    {
        ModelessDialogBox dlg = (ModelessDialogBox) objSrc;
        Random rnd = new Random();
        int iShade = rnd.Next(255);

        if (dlg.GrayShades)
            BackColor = Color.FromArgb(iShade, iShade, iShade);
        else
            BackColor = Color.FromArgb(iShade, rnd.Next(255), rnd.Next(255));
    }
}
