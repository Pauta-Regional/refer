//------------------------------------------
// AutoSizeDemo (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class AutoSizeDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new AutoSizeDemo());
    }
    public AutoSizeDemo()
    {
        Text = "AutoSize Demo";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        Button btn = new Button();
        btn.Parent = this;
        btn.AutoSize = true;
        btn.Text = "Look back on time with kindly eyes,\n" +
                   "He doubtless did his best;\n" +
                   "How softly sinks his trembling sun\n" +
                   "In human nature’s west!";
    }
}