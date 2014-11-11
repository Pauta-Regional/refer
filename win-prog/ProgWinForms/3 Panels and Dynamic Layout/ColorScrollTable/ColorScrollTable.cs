//-------------------------------------------------
// ColorScrollTable.cs (c) 2005 by Charles Petzold
//-------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorScrollTable : Form
{
    Panel pnlColor;
    Label[] alblValue = new Label[3];

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ColorScrollTable());
    }
    public ColorScrollTable()
    {
        Text = "Color Scroll with TableLayoutPanel";

        // Create a SplitContainer that fills the client area.
        SplitContainer splt = new SplitContainer();
        splt.Parent = this;
        splt.Dock = DockStyle.Fill;
        splt.SplitterDistance = ClientSize.Width / 2;

        // The TableLayoutPanel is on the left of the splitter.
        TableLayoutPanel table = new TableLayoutPanel();
        table.Parent = splt.Panel1;
        table.Dock = DockStyle.Fill;
        table.BackColor = Color.White;
        table.ColumnCount = 3;
        table.RowCount = 3;

        // Save the right SplitterPanel as a field.
        pnlColor = splt.Panel2;

        // Two arrays for color names and initial values.
        string[] astrColors = { "Red", "Green", "Blue" };
        int[] aiPanelColor = new int[3] { pnlColor.BackColor.R, 
                                          pnlColor.BackColor.G, 
                                          pnlColor.BackColor.B };

        // Loop through the three columns (red, green, blue).
        for (int col = 0; col < 3; col++)
        {
            // Label at the top identifies red, green, or blue.
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.None;
            lbl.Text = astrColors[col];
            lbl.ForeColor = Color.FromName(astrColors[col]);
            table.Controls.Add(lbl, col, 0);

            // Scrollbar to set new values.
            VScrollBar vscrl = new VScrollBar();
            vscrl.Parent = table;
            vscrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            vscrl.TabStop = true;
            vscrl.LargeChange = 16;
            vscrl.Maximum = 255 + vscrl.LargeChange - 1;
            vscrl.Value = aiPanelColor[col];
            vscrl.ValueChanged += OnScrollValueChanged;
            table.Controls.Add(vscrl, col, 1);

            // Label showing color value is bound to scrollbar.
            alblValue[col] = new Label();
            alblValue[col].AutoSize = true;
            alblValue[col].Anchor = AnchorStyles.None;
            alblValue[col].ForeColor = Color.FromName(astrColors[col]);
            alblValue[col].DataBindings.Add("Text", vscrl, "Value");
            table.Controls.Add(alblValue[col], col, 2);

            // ColumnStyles allocate three columns equally.
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
        }

        // RowStyles let the middle row be as large as possible.
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    }
    // When a scrollbar value changes, change the panel background color.
    void OnScrollValueChanged(object objSrc, EventArgs args)
    {
        pnlColor.BackColor = Color.FromArgb(int.Parse(alblValue[0].Text), 
                                            int.Parse(alblValue[1].Text), 
                                            int.Parse(alblValue[2].Text));
    }
}