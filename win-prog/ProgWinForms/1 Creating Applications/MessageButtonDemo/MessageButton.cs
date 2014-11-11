//----------------------------------------------
// MessageButton.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class MessageButton: Button
{
    string strMessageBoxText;

    public MessageButton()
    {
        Enabled = false;
    }
    public string MessageBoxText
    {
        set
        {
            strMessageBoxText = value;
            Enabled = value != null && value.Length > 0;
        }
        get
        {
            return strMessageBoxText;
        }
    }
    protected override void OnClick(EventArgs args)
    {
        MessageBox.Show(MessageBoxText, Text);
    }
}