//--------------------------------------------
// MatrixPanel.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using Petzold.ProgrammingWindowsForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class MatrixPanel: TableLayoutPanel
{
    public event EventHandler Change;

    NumericScan[] numscan = new NumericScan[6];

    public Matrix Matrix
    {
        set
        {
            for (int i = 0; i < 6; i++)
                numscan[i].Value = (decimal)value.Elements[i];
        }
        get
        {
            return new Matrix((float)numscan[0].Value, (float)numscan[1].Value,
                              (float)numscan[2].Value, (float)numscan[3].Value,
                              (float)numscan[4].Value, (float)numscan[5].Value);
        }
    }
    public MatrixPanel()
    {
        AutoSize = true;
        Padding = new Padding(Font.Height);
        ColumnCount = 2;

        SuspendLayout();

        for (int i = 0; i < 6; i++)
        {
            Label lbl = new Label();
            lbl.Parent = this;
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Left;
            lbl.Text = new string[] { "X Scale:", "Y Shear:", "X Shear:",
                                      "Y Scale:", "X Translate:",
                                      "Y Translate:" }[i];

            numscan[i] = new NumericScan();
            numscan[i].Parent = this;
            numscan[i].AutoSize = true;
            numscan[i].Anchor = AnchorStyles.Right;
            numscan[i].Minimum = -1000;
            numscan[i].Maximum = 1000;
            numscan[i].DecimalPlaces = 2;
            numscan[i].ValueChanged += NumericScanOnValueChanged;
        }
        ResumeLayout();

        Matrix = new Matrix();
    }
    void NumericScanOnValueChanged(object objSrc, EventArgs args)
    {
        OnChange(EventArgs.Empty);
    }
    protected virtual void OnChange(EventArgs args)
    {
        if (Change != null)
            Change(this, args);
    }
}

