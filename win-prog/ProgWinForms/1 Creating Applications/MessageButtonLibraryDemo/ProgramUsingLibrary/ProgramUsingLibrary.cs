//----------------------------------------------------
// ProgramUsingLibrary.cs (c) 2005 by Charles Petzold
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Petzold.ProgrammingWindowsForms;

class ProgramUsingLibrary: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ProgramUsingLibrary());
    }
    public ProgramUsingLibrary()
    {
        Text = "Program Using Library";

        MessageButton msgbtn = new MessageButton();
        msgbtn.Parent = this;
        msgbtn.Text = "Calculate 10,000,000 digits of PI";
        msgbtn.MessageBoxText = "This button is not yet implemented!";
        msgbtn.Location = new Point(50, 50);
        msgbtn.AutoSize = true;
    }
}