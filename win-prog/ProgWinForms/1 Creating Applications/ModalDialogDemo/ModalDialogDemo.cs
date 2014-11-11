//------------------------------------------------
// ModalDialogDemo.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ModalDialogDemo: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ModalDialogDemo());
    }
    public ModalDialogDemo()
    {
        Text = "Modal Dialog Demo";

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Change Color";
        btn.Location = new Point(16, 16);
        btn.AutoSize = true;
        btn.Click += ButtonOnClick;
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        ModalDialogBox dlg = new ModalDialogBox();

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            Random rnd = new Random();
            int iShade = rnd.Next(255);

            if (dlg.GrayShades)
                BackColor = Color.FromArgb(iShade, iShade, iShade);
            else
                BackColor = Color.FromArgb(iShade, rnd.Next(255), rnd.Next(255));
        }
    }
}
