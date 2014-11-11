//----------------------------------------------------------------
// UnboundDataGridViewWithCalendar.cs (c) 2005 by Charles Petzold
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class UnboundDataGridViewWithCalendar: Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new UnboundDataGridViewWithCalendar());
    }
    public UnboundDataGridViewWithCalendar()
    {
        Text = "Unbound DataGridView with Calendar";
        Width *= 2;

        DataGridView grid = new DataGridView();
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

        CalendarColumn colBirth = new CalendarColumn();
        colBirth.Name = "BirthDate";
        colBirth.HeaderText = "Birth Date";
        grid.Columns.Add(colBirth);

        DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
        colCheck.Name = "Enrolled";
        colCheck.HeaderText = "Enrolled?";
        grid.Columns.Add(colCheck);
    }
}



