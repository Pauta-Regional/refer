//-----------------------------------------------
// BeepButtonDemo.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class BeepButtonDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new BeepButtonDemo());
    }
    public BeepButtonDemo()
    {
        Text = "BeepButton Demonstration";

        BeepButton btn = new BeepButton();
        btn.Parent = this;
        btn.Location = new Point(100, 100);
        btn.AutoSize = true;
        btn.Text = "Click the BeepButton";
        btn.Click += ButtonOnClick;
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        SilentMsgBox.Show("The BeepButton has been clicked", Text);
    }
}
