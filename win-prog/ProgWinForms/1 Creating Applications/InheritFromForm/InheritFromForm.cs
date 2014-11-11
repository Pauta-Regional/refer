//------------------------------------------------
// InheritFromForm.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class InheritFromForm: Form
{
    public static void Main()
    {
        Application.Run(new InheritFromForm());
    }
    public InheritFromForm()
    {
        Text = "Inherit from Form";
        Width *= 2;
    }
    protected override void OnClick(EventArgs args)
    {
        MessageBox.Show("The form has been clicked!", "Click");
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        grfx.DrawString("Hello, Windows Forms", Font, 
                        SystemBrushes.ControlText, 0, 0);
    }
}
