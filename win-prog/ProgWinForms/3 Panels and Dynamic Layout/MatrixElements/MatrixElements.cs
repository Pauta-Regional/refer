//-----------------------------------------------
// MatrixElements.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class MatrixElements : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MatrixElements());
    }
    public MatrixElements()
    {
        Text = "Matrix Elements";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        TableLayoutPanel table = new TableLayoutPanel();
        table.Parent = this;
        table.Padding = new Padding(Font.Height);
        table.AutoSize = true;
        table.ColumnCount = 2;

        table.SuspendLayout();

        for (int i = 0; i < 6; i++)
        {
            Label lbl = new Label();
            lbl.Parent = table;
            lbl.AutoSize = true;
            lbl.Text = new string[] { "X Scale:", "Y Shear:", "X Shear:",
                                      "Y Scale:", "X Translate",
                                      "Y Translate:" }[i];

            NumericUpDown updn = new NumericUpDown();
            updn.Parent = table;
            updn.AutoSize = true;
            updn.DecimalPlaces = 2;
        }

        Button btn = new Button();
        btn.Parent = table;
        btn.AutoSize = true;
        btn.Text = "Update";
 
        btn = new Button();
        btn.Parent = table;
        btn.AutoSize = true;
        btn.Text = "Methods...";

        table.ResumeLayout();
    }
}