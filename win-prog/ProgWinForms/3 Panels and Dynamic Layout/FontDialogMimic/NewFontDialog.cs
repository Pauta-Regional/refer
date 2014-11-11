//----------------------------------------------
// NewFontDialog.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class NewFontDialog : Form
{
    ComboBox comboFont, comboSize;
    StyleComboBox comboStyle;
    CheckBox chkboxStrikeout, chkboxUnderline;
    ColorComboBox comboColor;
    Label lblColor, lblSample;

    public NewFontDialog()
    {
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = MaximizeBox = ShowInTaskbar = false;
        Text = "Font";
        AutoSize = true;

        TableLayoutPanel table = new TableLayoutPanel();
        table.Parent = this;
        table.AutoSize = true;
        table.ColumnCount = 4;
        table.RowCount = 3;
        table.Padding = new Padding(base.Font.Height);

        Label lbl = new Label();
        lbl.Text = "&Font:";
        lbl.AutoSize = true;
        table.Controls.Add(lbl, 0, 0);

        comboFont = new ComboBox();
        comboFont.DropDownStyle = ComboBoxStyle.Simple;
        comboFont.AutoSize = true;
        comboFont.TextChanged += OnComboBoxTextChanged;
        table.Controls.Add(comboFont, 0, 1);

        // Fill the comboFont box with the font families.
        foreach (FontFamily fntfam in FontFamily.Families)
            comboFont.Items.Add(fntfam.Name);

        lbl = new Label();
        lbl.Text = "&Font st&yle:";
        lbl.AutoSize = true;
        table.Controls.Add(lbl, 1, 0);

        comboStyle = new StyleComboBox();
        comboStyle.DropDownStyle = ComboBoxStyle.Simple;
        comboStyle.AutoSize = true;
        comboStyle.TextChanged += OnComboBoxTextChanged;
        table.Controls.Add(comboStyle, 1, 1);

        lbl = new Label();
        lbl.Text = "&Size:";
        lbl.AutoSize = true;
        table.Controls.Add(lbl, 2, 0);

        comboSize = new ComboBox();
        comboSize.DropDownStyle = ComboBoxStyle.Simple;
        comboSize.AutoSize = true;
        comboSize.TextChanged += OnComboBoxTextChanged;
        table.Controls.Add(comboSize, 2, 1);

        // Add the font sizes to the combo box.
        for (int i = 8; i < 12; i++)
            comboSize.Items.Add(i);

        for (int i = 12; i < 30; i += 2)
            comboSize.Items.Add(i);

        for (int i = 36; i <= 72; i += 12)
            comboSize.Items.Add(i);

        comboSize.SelectedIndex = 0;

        // Effects GroupPanel.
        GroupPanel grppnl = new GroupPanel();
        grppnl.Text = "Effects";
        table.Controls.Add(grppnl, 0, 2);

        // Strikeout CheckBox.
        chkboxStrikeout = new CheckBox();
        chkboxStrikeout.Text = "Stri&keout";
        chkboxStrikeout.AutoSize = true;
        chkboxStrikeout.Click += delegate { ShowNewFont(); };
        grppnl.Controls.Add(chkboxStrikeout);

        // Underline CheckBox.
        chkboxUnderline = new CheckBox();
        chkboxUnderline.Text = "&Underline";
        chkboxUnderline.AutoSize = true;
        chkboxUnderline.Click += delegate { ShowNewFont(); };
        grppnl.Controls.Add(chkboxUnderline);

        // Color Label.
        lblColor = new Label();
        lblColor.Text = "&Color:";
        lblColor.AutoSize = true;
        lblColor.Visible = false;
        grppnl.Controls.Add(lblColor);

        // Color ComboBox
        comboColor = new ColorComboBox();
        comboColor.Visible = false;
        comboColor.AutoSize = true;
        comboColor.TextChanged += OnComboBoxTextChanged;
        grppnl.Controls.Add(comboColor);

        // Flow panel for sample and script
        FlowLayoutPanel flow = new FlowLayoutPanel();
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;
        table.Controls.Add(flow, 1, 2);
        table.SetColumnSpan(flow, 2);

        // GroupPanel for sample.
        grppnl = new GroupPanel();
        grppnl.Text = "Sample";
        flow.Controls.Add(grppnl);

        // Sample label.
        lblSample = new Label();
        lblSample.Text = "AaBbYyZz";
        lblSample.Font = base.Font;
        lblSample.TextAlign = ContentAlignment.MiddleCenter;
        lblSample.BorderStyle = BorderStyle.Fixed3D;
        lblSample.Size = new Size(20 * base.Font.Height, 3 * base.Font.Height);
        grppnl.Controls.Add(lblSample);

        // Flow panel for buttons.
        flow = new FlowLayoutPanel();
        flow.AutoSize = true;
        flow.FlowDirection = FlowDirection.TopDown;
        table.Controls.Add(flow, 3, 1);

        // OK button.
        Button btn = new Button();
        btn.Text = "OK";
        btn.AutoSize = true;
        btn.DialogResult = DialogResult.OK;
        AcceptButton = btn;
        flow.Controls.Add(btn);

        // Cancel button.
        btn = new Button();
        btn.Text = "Cancel";
        btn.AutoSize = true;
        btn.DialogResult = DialogResult.Cancel;
        CancelButton = btn;
        flow.Controls.Add(btn);

        // Generate event for filling style box.
        comboFont.SelectedItem = base.Font.FontFamily.Name;
    }

    public new Font Font
    {
        set
        {
            lblSample.Font = value;
            comboFont.SelectedItem = value.FontFamily.Name;

            comboStyle.FamilyName = value.FontFamily.Name;
            comboStyle.FontStyle = value.Style;

            chkboxStrikeout.Checked = (value.Style & FontStyle.Strikeout) != 0;
            chkboxUnderline.Checked = (value.Style & FontStyle.Underline) != 0;

            comboSize.SelectedItem = value.SizeInPoints;
            comboSize.Text = value.SizeInPoints.ToString();
        }
        get
        {
            return lblSample.Font;
        }
    }
    public Color Color
    {
        set
        {
            comboColor.Color = value;
        }
        get
        {
            return comboColor.Color;
        }
    }
    public bool ShowColor
    {
        set
        {
            lblColor.Visible = comboColor.Visible = value;
        }
        get
        {
            return comboColor.Visible;
        }
    }
    void OnComboBoxTextChanged(object objSrc, EventArgs args)
    {
        ComboBox combo = (ComboBox)objSrc;

        if (combo == comboColor)
        {
            lblSample.ForeColor = comboColor.Color;
            return;
        }

        if (combo == comboFont)
        {
            int index = comboFont.FindStringExact(comboFont.Text);

            if (index != -1)
            {
                comboFont.SelectedIndex = index;
                comboStyle.FamilyName = comboFont.Text;
            }
        }
        ShowNewFont();
    }
    void ShowNewFont()
    {
        FontStyle fntstyle;

        try
        {
            fntstyle = comboStyle.FontStyle;
        }
        catch
        {
            return;
        }

        if (chkboxStrikeout.Checked)
            fntstyle |= FontStyle.Strikeout;

        if (chkboxUnderline.Checked)
            fntstyle |= FontStyle.Underline;

        try
        {
            Font fnt = new Font(comboFont.Text, float.Parse(comboSize.Text), fntstyle);
            lblSample.Font = fnt;
        }
        catch
        {
            ;   
        }
    }
}
