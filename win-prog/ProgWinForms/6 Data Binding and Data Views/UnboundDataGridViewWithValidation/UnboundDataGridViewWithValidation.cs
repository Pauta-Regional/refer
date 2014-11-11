//------------------------------------------------------------------
// UnboundDataGridViewWithValidation.cs (c) 2005 by Charles Petzold
//------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class UnboundDataGridViewWithValidation: UnboundDataGridViewWithFileIO
{
    [STAThread]
    public new static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new UnboundDataGridViewWithValidation());
    }
    public UnboundDataGridViewWithValidation()
    {
        Text = "Unbound DataGridView with Validation";

        grid.DefaultValuesNeeded += OnDefaultValuesNeeded;
        grid.CellValidating += OnValidating;
    }
    void OnDefaultValuesNeeded(object objSrc, DataGridViewRowEventArgs args)
    {
        // Create Student object with default values.
        Student sdt = new Student();

        args.Row.Cells["Courtesy"].Value = sdt.Courtesy;
        args.Row.Cells["FirstName"].Value = sdt.FirstName;
        args.Row.Cells["LastName"].Value = sdt.LastName;
        args.Row.Cells["BirthDate"].Value = sdt.BirthDate.ToShortDateString();
        args.Row.Cells["Enrolled"].Value = sdt.Enrolled;
    }
    void OnValidating(object objSrc, DataGridViewCellValidatingEventArgs args)
    {
        DataGridView grid = objSrc as DataGridView;
        DateTime dtResult;

        grid.Rows[args.RowIndex].ErrorText = "";

        if (args.ColumnIndex != grid.Columns["BirthDate"].Index)
            return;

        // Check if the BirthDate value parses.
        if (!DateTime.TryParse(args.FormattedValue.ToString(), out dtResult))
        {
            args.Cancel = true;
            grid.Rows[args.RowIndex].ErrorText =
                "Enter the date like: September 4, 1985\r\n" +
                "or: 9/4/1985";
        }
    }
}
