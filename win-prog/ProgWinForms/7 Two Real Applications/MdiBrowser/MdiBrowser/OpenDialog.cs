//-------------------------------------------
// OpenDialog.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//-------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class OpenDialog : Form
{
    ComboBox combo;
    Button btnOk;

    // Public property.
    public string Url
    {
        get { return combo.Text; }
        set { combo.Text = value; }
    }
    // Constructor.
    public OpenDialog(MdiBrowserSettings settings)
    {
        // Set numerous properties.
        Text = "Open";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        ControlBox = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        Icon = ActiveForm.Icon;
        StartPosition = FormStartPosition.Manual;
        Location = ActiveForm.Location + SystemInformation.CaptionButtonSize +
                                         SystemInformation.FrameBorderSize;

        // Create table layout panel and controls.
        TableLayoutPanel table = new TableLayoutPanel();
        table.Parent = this;
        table.AutoSize = true;
        table.Padding = new Padding(Font.Height);
        table.RowCount = 3;
        table.ColumnCount = 4;

        Label lbl = new Label();
        lbl.Text = "Enter a URL or filename, or\r\n" +
                   "Select a previously visited site from the list, or\r\n" +
                   "Press Browse to select a file.";
        lbl.AutoSize = true;
        table.Controls.Add(lbl, 1, 0);
        table.SetColumnSpan(lbl, 3);

        lbl = new Label();
        lbl.Text = "Open: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;
        table.Controls.Add(lbl, 0, 1);

        combo = new ComboBox();
        combo.AutoSize = true;
        combo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        combo.TextChanged += ComboBoxOnTextChanged;
        combo.BeginUpdate();
        foreach (string str in settings.ManualUrls)
            combo.Items.Add(str);
        combo.EndUpdate();

        table.Controls.Add(combo, 1, 1);
        table.SetColumnSpan(combo, 3);

        btnOk = new Button();
        btnOk.Text = "OK";
        btnOk.AutoSize = true;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Enabled = false;
        btnOk.Margin = new Padding(Font.Height);
        table.Controls.Add(btnOk, 1, 2);
        AcceptButton = btnOk;

        Button btn = new Button();
        btn.Text = "Cancel";
        btn.AutoSize = true;
        btn.DialogResult = DialogResult.Cancel;
        btn.Margin = new Padding(Font.Height);
        table.Controls.Add(btn, 2, 2);
        CancelButton = btn;

        btn = new Button();
        btn.Text = "Browse...";
        btn.AutoSize = true;
        btn.Margin = new Padding(Font.Height);
        btn.Click += BrowseOnClick;
        table.Controls.Add(btn, 3, 2);
    }
    void ComboBoxOnTextChanged(object objSrc, EventArgs args)
    {
        ComboBox combo = objSrc as ComboBox;
        btnOk.Enabled = combo.Text != null && combo.Text.Length > 0;
    }
    void BrowseOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            Url = dlg.FileName;
        }
    }
}
