//---------------------------------------------
// AnchorFields.cs (c) 2005 by Charles Petzold
//---------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class AnchorFields : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new AnchorFields());
    }
    public AnchorFields()
    {
        Text = "Anchor Fields";
        int iSpace = Font.Height;
        int y = iSpace;

        for (int i = 0; i < 4; i++)
        {
            Label lbl = new Label();
            lbl.Parent = this;
            lbl.AutoSize = true;
            lbl.Text = (new string[] { "Name:", "Address:", "Job:", 
                                       "Very personal information:" })[i];
            lbl.Location = new Point(iSpace, y);

            TextBox txtbox = new TextBox();
            txtbox.Parent = this;
            txtbox.Location = new Point(lbl.Right + iSpace, y);
            txtbox.Size = new Size(ClientSize.Width - iSpace - txtbox.Left, 
                                   txtbox.Height);
            txtbox.Anchor |= AnchorStyles.Right;

            y = txtbox.Bottom + iSpace;
        }
    }
}