//-----------------------------------------------
// WhatClientSize.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class WhatClientSize: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new WhatClientSize());
    }
    public WhatClientSize()
    {
        Text = "What Client Size?";

        Label lbl = new Label();
        lbl.Parent = this;
        lbl.AutoSize = true;
        lbl.DataBindings.Add("Text", this, "ClientSize");
    }
}
