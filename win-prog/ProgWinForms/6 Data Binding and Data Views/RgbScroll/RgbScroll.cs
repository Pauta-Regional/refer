//------------------------------------------
// RgbScroll.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorScrollTable : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ColorScrollTable());
    }
    public ColorScrollTable()
    {
        Text = "Rgb Scroll with TableLayoutPanel";

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

        // Create the right SplitterPanel for displaying the color.
        Panel pnlColor = splt.Panel2;

        // Create an Rgb object and give it the pnlColor color.
        Rgb rgb = new Rgb();
        rgb.Color = pnlColor.BackColor;

        // Bind the background color to rgb.
        pnlColor.DataBindings.Add("BackColor", rgb, "Color");

        // Array for color names.
        string[] astrColors = { "Red", "Green", "Blue" };

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

            // Scrollbar to set new values is bound to rgb.
            VScrollBar vscrl = new VScrollBar();
            vscrl.Parent = table;
            vscrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            vscrl.TabStop = true;
            vscrl.LargeChange = 16;
            vscrl.Maximum = 255 + vscrl.LargeChange - 1;
            vscrl.DataBindings.Add("Value", rgb, astrColors[col]);
            vscrl.DataBindings[0].DataSourceUpdateMode = 
                                    DataSourceUpdateMode.OnPropertyChanged;
            table.Controls.Add(vscrl, col, 1);

            // Label showing color value is bound to scrollbar.
            Label lblValue = new Label();
            lblValue.AutoSize = true;
            lblValue.Anchor = AnchorStyles.None;
            lblValue.ForeColor = Color.FromName(astrColors[col]);
            lblValue.DataBindings.Add("Text", vscrl, "Value");
            table.Controls.Add(lblValue, col, 2);

            // ColumnStyles allocate three columns equally.
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
        }

        // RowStyles let the middle row be as large as possible.
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    }
}