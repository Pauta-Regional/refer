//--------------------------------------------------
// ColorRadioButtons.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorRadioButtons : Form
{
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ColorRadioButtons());
    }
    public ColorRadioButtons()
    {
        Text = "Color Radio Buttons";

        Color[] aclr = { Color.Red, Color.Orange, Color.Yellow, Color.Green,
                         Color.Blue, Color.Indigo, Color.Violet };

        int y = Font.Height;

        foreach (Color clr in aclr)
        {
            RadioButton radio = new RadioButton();
            radio.Parent = this;
            radio.Location = new Point(Font.Height, y);
            radio.Text = clr.Name;
            radio.AutoSize = true;
            radio.Tag = clr;
            radio.CheckedChanged += RadioButtonOnCheckedChanged;

            y += radio.Height;
        }
    }
    void RadioButtonOnCheckedChanged(object objSrc, EventArgs args)
    {
        RadioButton radio = objSrc as RadioButton;
        BackColor = (Color)radio.Tag;
    }
}
