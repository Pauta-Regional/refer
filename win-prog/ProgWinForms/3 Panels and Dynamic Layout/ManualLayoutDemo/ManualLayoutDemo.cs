//-------------------------------------------------
// ManualLayoutDemo.cs (c) 2005 by Charles Petzold
//-------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ManualLayoutDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ManualLayoutDemo());
    }
    public ManualLayoutDemo()
    {
        Text = "Manual Layout Demo";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        Button btn = new Button();
        btn.Parent = this;
        btn.AutoSize = true;
        btn.Text = "Look back on time with kindly eyes,\n" +
                   "He doubtless did his best;\n" +
                   "How softly sinks his trembling sun\n" +
                   "In human nature’s west!";

        Button btn2 = new Button();
        btn2.Parent = this;
        btn2.AutoSize = true;
        btn2.Location = new Point(btn.Right, 0);
        btn2.Text = "He ate and drank the precious words,\n" +
                    "His spirit grew robust;\n" +
                    "He knew no more that he was poor,\n" +
                    "Nor that his frame was dust.\n" +
                    "He danced along the dingy days,\n" +
                    "And this bequest of wings\n" +
                    "Was but a book. What liberty\n" +
                    "A loosened spirit brings!";
    }
}