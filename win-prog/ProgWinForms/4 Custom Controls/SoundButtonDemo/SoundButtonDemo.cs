//------------------------------------------------
// SoundButtonDemo.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class SoundButtonDemo : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SoundButtonDemo());
    }
    public SoundButtonDemo()
    {
        Text = "SoundButton Demonstration";

        SoundButton btn = new SoundButton();
        btn.Parent = this;
        btn.Location = new Point(50, 25);
        btn.AutoSize = true;
        btn.Text = "SoundButton with File";
        btn.Click += ButtonOnClick;
        btn.WaveFile = Path.Combine(
            Environment.GetEnvironmentVariable("windir"), 
            "Media\\tada.wav");

        btn = new SoundButton();
        btn.Parent = this;
        btn.Location = new Point(50, 125);
        btn.AutoSize = true;
        btn.Text = "SoundButton with URI";
        btn.Click += ButtonOnClick;
        btn.WaveFile = "http://www.oaklandzoo.org/atoz/azlinsnd.wav";

        btn = new SoundButton();
        btn.Parent = this;
        btn.Location = new Point(50, 225);
        btn.AutoSize = true;
        btn.Text = "SoundButton with Resource";
        btn.Click += ButtonOnClick;
        btn.WaveStream = GetType().Assembly.GetManifestResourceStream(
                "SoundButtonDemo.MakeItSo.wav");
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        Button btn = objSrc as Button;
        SilentMsgBox.Show("The SoundButton has been clicked", btn.Text);
    }
}
