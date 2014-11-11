//---------------------------------------------------
// SimpleDataGridView.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SimpleDataGridView : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimpleDataGridView());
    }
    public SimpleDataGridView()
    {
        Text = "Simple DataGridView";

        DataGridView grid = new DataGridView();
        grid.Parent = this;
        grid.AutoSize = true;
        grid.Dock = DockStyle.Fill;
        grid.ColumnCount = 3;
        grid.Columns[0].HeaderText = "First Name";
        grid.Columns[1].HeaderText = "Last Name";
        grid.Columns[2].HeaderText = "Email Address";
    }
}
 