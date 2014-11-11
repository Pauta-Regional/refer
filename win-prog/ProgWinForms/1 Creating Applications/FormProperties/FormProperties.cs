//-----------------------------------------------
// FormProperties.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FormProperties
{
    public static void Main()
    {
        Form frm = new Form();

        frm.Text = "My WinForms Program";
        frm.Width *= 2;

        Application.Run(frm);
    }
}
