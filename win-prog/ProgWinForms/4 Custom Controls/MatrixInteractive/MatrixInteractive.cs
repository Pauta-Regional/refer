//--------------------------------------------------
// MatrixInteractive.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class MatrixInteractive : Form
{
    MatrixPanel matxpnl;
    DisplayPanel disppnl;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MatrixInteractive());
    }
    public MatrixInteractive()
    {
        Text = "Matrix Interactive";

        TableLayoutPanel pnl = new TableLayoutPanel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;
        pnl.ColumnCount = 2;

        matxpnl = new MatrixPanel();
        matxpnl.Parent = pnl;
        matxpnl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        matxpnl.Change += MatrixPanelOnChange;

        disppnl = new DisplayPanel();
        disppnl.Parent = pnl;
        disppnl.Dock = DockStyle.Fill;
        disppnl.BackColor = Color.White;
        disppnl.ForeColor = Color.Black;
        disppnl.Text = "Sample Text";
        disppnl.Font = new Font(FontFamily.GenericSerif, 24);

        Width = 3 * matxpnl.Width;
        Height = 3 * matxpnl.Height / 2;
    }
    void MatrixPanelOnChange(object objSrc, EventArgs args)
    {
        disppnl.Transform = matxpnl.Matrix;
    }
}