//--------------------------------------------------------
// DeviceIndependentButton.cs (c) 2005 by Charles Petzold
//--------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class DeviceIndependentButton: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DeviceIndependentButton());
    }
    public DeviceIndependentButton()
    {
        Text = "Device-Independent Button";
        int fntht = Font.Height;

        ClientSize = new Size(fntht * 30, fntht * 10);

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Lookin' good!";
        btn.Size = new Size(17 * fntht / 2, 7 * fntht / 4);
        btn.Location = new Point((ClientSize.Width - btn.Width) / 2,
                                 (ClientSize.Height - btn.Height) / 2);
    }
}
