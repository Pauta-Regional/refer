//-----------------------------------------------
// FormWithButton.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FormWithButton: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FormWithButton());
    }
    public FormWithButton()
    {
        Text = "Form with Button";

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Click me!";
        btn.Location = new Point(50, 25);
        btn.AutoSize = true;
        btn.Click += ButtonOnClick;
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        MessageBox.Show("The button was clicked!", "Button");
    }
}