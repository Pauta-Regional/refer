//----------------------------------------------
// ColorGridDemo.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorGridDemo : Form
{
    Label lbl;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ColorGridDemo());
    }
    public ColorGridDemo()
    {
        Text = "Custom Color Control";
        AutoSize = true;

        TableLayoutPanel table = new TableLayoutPanel();
        table.Parent = this;
        table.AutoSize = true;
        table.ColumnCount = 3;

        Button btn = new Button();
        btn.Parent = table;
        btn.AutoSize = true;
        btn.Text = "Button One";

        ColorGrid clrgrid = new ColorGrid();
        clrgrid.Parent = table;
        clrgrid.Click += ColorGridOnClick;

        btn = new Button();
        btn.Parent = table;
        btn.AutoSize = true;
        btn.Text = "Button Two";

        lbl = new Label();
        lbl.Parent = table;
        lbl.AutoSize = true;
        lbl.Font = new Font("Times New Roman", 24);
        lbl.Text = "Sample Text";

        table.SetColumnSpan(lbl, 3);
        clrgrid.SelectedColor = lbl.ForeColor;
    }
    void ColorGridOnClick(object objSrc, EventArgs args)
    {
        ColorGrid clrgrid = (ColorGrid) objSrc;
        lbl.ForeColor = clrgrid.SelectedColor;
    }
}
