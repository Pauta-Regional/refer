//------------------------------------------------
// AutoScaleButton.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class AutoScaleButton: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new AutoScaleButton());
    }
    public AutoScaleButton()
    {
        Text = "Auto-Scale Button";

        ClientSize = new Size(240, 80);

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Lookin' good!";
        btn.Size = new Size(17 * 4, 14);
        btn.Location = new Point((ClientSize.Width - btn.Width) / 2,
                                 (ClientSize.Height - btn.Height) / 2);

        AutoScaleDimensions = new Size(4, 8);
        AutoScaleMode = AutoScaleMode.Font;
    }
}
