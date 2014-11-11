//-------------------------------------------
// FormEvents.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FormEvents
{
    public static void Main()
    {
        Form frm = new Form();

        frm.Text = "My Events Program";
        frm.Width *= 2;
        frm.Click += MyClicker;
        frm.Paint += MyPainter;

        Application.Run(frm);
    }
    static void MyClicker(object objSrc, EventArgs args)
    {
        MessageBox.Show("The form has been clicked!", "Click");
    }
    static void MyPainter(object objSrc, PaintEventArgs args)
    {
        Form frm = (Form)objSrc;
        Graphics grfx = args.Graphics;
        grfx.DrawString("Hello, Windows Forms", frm.Font, 
                        SystemBrushes.ControlText, 0, 0);
    }
}
