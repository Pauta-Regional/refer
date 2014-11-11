//------------------------------------------
// LifeYears.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class LifeYears : Form
{
    DateTimePicker dtpBeg, dtpEnd;
    Label lblResult;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new LifeYears());
    }
    public LifeYears()
    {
        Text = "LifeYears";

        Label lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Begin Date: ";
        lbl.AutoSize = true;
        lbl.Location = new Point(Font.Height, Font.Height);

        int xDateTimePicker = lbl.Right;

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "End Date: ";
        lbl.AutoSize = true;
        lbl.Location = new Point(Font.Height, 3 * Font.Height);

        xDateTimePicker = Math.Max(xDateTimePicker, lbl.Right);

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Life Years:";
        lbl.AutoSize = true;
        lbl.Location = new Point(Font.Height, 5 * Font.Height);

        xDateTimePicker = Math.Max(xDateTimePicker, lbl.Right);

        dtpBeg = new DateTimePicker();
        dtpBeg.Parent = this;
        dtpBeg.AutoSize = true;
        dtpBeg.Location = new Point(xDateTimePicker, Font.Height);
        dtpBeg.ValueChanged += DateTimePickerValueChanged;

        dtpEnd = new DateTimePicker();
        dtpEnd.Parent = this;
        dtpEnd.AutoSize = true;
        dtpEnd.Location = new Point(xDateTimePicker, 3 * Font.Height);
        dtpEnd.ValueChanged += DateTimePickerValueChanged;

        lblResult = new Label();
        lblResult.Parent = this;
        lblResult.AutoSize = true;
        lblResult.Location = new Point(xDateTimePicker, 5 * Font.Height);

        ClientSize = new Size(dtpEnd.Right + Font.Height, 7 * Font.Height);
    }
    void DateTimePickerValueChanged(object objSrc, EventArgs args)
    {
        if (dtpBeg.Value >= dtpEnd.Value)
        {
            lblResult.Text = "";
        }
        else
        {
            DateTime dtBeg = dtpBeg.Value;
            DateTime dtEnd = dtpEnd.Value;

            int iYears = dtEnd.Year - dtBeg.Year;
            int iMonths = dtEnd.Month - dtBeg.Month;
            int iDays = dtEnd.Day - dtBeg.Day;

            if (iDays < 0)
            {
                iDays += DateTime.DaysInMonth(dtEnd.Year,
                                        1 + (dtEnd.Month + 10) % 12);
                iMonths -= 1;
            }
            if (iMonths < 0)
            {
                iMonths += 12;
                iYears -= 1;
            }
            lblResult.Text = String.Format("{0} year{1}, {2} month{3}, {4} day{5}",
                                            iYears, iYears == 1 ? "" : "s", 
                                            iMonths, iMonths == 1 ? "" : "s",
                                            iDays, iDays == 1 ? "" : "s");
        }
    }
}
