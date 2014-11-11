//----------------------------------------------------
// UnboundDataGridView.cs (c) 2005 by Charles Petzold
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class UnboundDataGridView : Form
{
    protected DataGridView grid;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new UnboundDataGridView());
    }
    public UnboundDataGridView()
    {
        Text = "Unbound DataGridView";
        Width *= 2;

        grid = new DataGridView();
        grid.Parent = this;
        grid.AutoSize = true;
        grid.Dock = DockStyle.Fill;

        DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();
        colCombo.Name = "Courtesy";
        colCombo.HeaderText = "Courtesy";
        colCombo.DataSource = Enum.GetValues(typeof(CourtesyTitle));
        colCombo.ValueType = typeof(CourtesyTitle);
        grid.Columns.Add(colCombo);

        DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
        colText.Name = "FirstName";
        colText.HeaderText = "First Name";
        grid.Columns.Add(colText);

        colText = new DataGridViewTextBoxColumn();
        colText.Name = "LastName";
        colText.HeaderText = "Last Name";
        grid.Columns.Add(colText);

        colText = new DataGridViewTextBoxColumn();
        colText.Name = "BirthDate";
        colText.HeaderText = "Birth Date";
        grid.Columns.Add(colText);

        DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
        colCheck.Name = "Enrolled";
        colCheck.HeaderText = "Enrolled?";
        grid.Columns.Add(colCheck);
    }
}
