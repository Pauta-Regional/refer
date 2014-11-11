//----------------------------------------------------
// FormattingToolStrip.cs (c) 2005 by Charles Petzold
//----------------------------------------------------
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class FormattingToolStrip : Form
{
    protected RichTextBox txtbox;
    ToolStripComboBox comboName, comboSize;
    ToolStripButton btnBold, btnItalic, btnUnderline, btnStrikeout;
    ToolStripButton btnLeft, btnRight, btnCenter, btnBullets;
    ToolStripTextBox txtLeftIndent, txtRightIndent, txtFirstLine;
    ColorDialog clrdlg = new ColorDialog();
    float xDpi;
    bool bSuspendSelectionChanged = false;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new FormattingToolStrip());
    }
    public FormattingToolStrip()
    {
        Text = "Formatting ToolStrip";
        Width = 800;

        // Obtain the horizontal resolution of the screen in dots per inch.
        Graphics grfx = CreateGraphics();
        xDpi = grfx.DpiX;
        grfx.Dispose();

        // Load in many bitmaps for the tool strip.
        ImageList imglst = new ImageList();
        imglst.TransparentColor = Color.Magenta;
        imglst.Images.Add("BackColor", new Bitmap(GetType(), "ChooseColor.bmp"));
        imglst.Images.Add("ForeColor", new Bitmap(GetType(), "Forecolor.bmp"));
        imglst.Images.Add("Left", 
            new Bitmap(GetType(), "AlignTableCellMiddleLeftJustHS.bmp"));
        imglst.Images.Add("Right", 
            new Bitmap(GetType(), "AlignTableCellMiddleRight.bmp"));
        imglst.Images.Add("Center", 
            new Bitmap(GetType(), "AlignTableCellMiddleCenter.bmp"));
        imglst.Images.Add("Bullets", new Bitmap(GetType(), "List_Bullets.bmp"));

        // Create a few more bitmaps.
        imglst.Images.Add("Bold", FontStyleBitmap("B", FontStyle.Bold));
        imglst.Images.Add("Italic", FontStyleBitmap("I", FontStyle.Italic));
        imglst.Images.Add("Underline", FontStyleBitmap("U", FontStyle.Underline));
        imglst.Images.Add("Strikeout", FontStyleBitmap("S", FontStyle.Strikeout));

        // Create the RichTextBox control that dominates the client area.
        txtbox = new RichTextBox();
        txtbox.Parent = this;
        txtbox.Dock = DockStyle.Fill;
        txtbox.SelectionChanged += TextBoxOnSelectionChanged;

        // Create the ToolStrip control.
        ToolStrip tool = new ToolStrip();
        tool.Parent = this;
        tool.ImageList = imglst;

        // Create and fill the font name combo box.
        comboName = new ToolStripComboBox();
        comboName.ToolTipText = "Font Name";
        comboName.SelectedIndexChanged += NameOnSelectionChanged;
        tool.Items.Add(comboName);

        foreach (FontFamily fntfam in FontFamily.Families)
            comboName.Items.Add(fntfam.Name);

        // Create and fill the font size combo box.
        comboSize = new ToolStripComboBox();
        comboSize.ToolTipText = "Font Size";
        comboSize.SelectedIndexChanged += SizeOnSelectionChanged;
        tool.Items.Add(comboSize);

        for (int i = 8; i <= 10; i++)
            comboSize.Items.Add(i.ToString());
        for (int i = 12; i <= 28; i += 2)
            comboSize.Items.Add(i.ToString());
        for (int i = 36; i <= 72; i += 12)
            comboSize.Items.Add(i.ToString());

        // Create the bold, italic, underline, and strikeout buttons.
        btnBold = new ToolStripButton();
        btnBold.ImageKey = "Bold";
        btnBold.ToolTipText = "Bold";
        btnBold.Tag = FontStyle.Bold;
        btnBold.CheckOnClick = true;
        btnBold.Click += FontStyleOnClick;
        tool.Items.Add(btnBold);

        btnItalic = new ToolStripButton();
        btnItalic.ImageKey = "Italic";
        btnItalic.ToolTipText = "Italic";
        btnItalic.Tag = FontStyle.Italic;
        btnItalic.CheckOnClick = true;
        btnItalic.Click += FontStyleOnClick;
        tool.Items.Add(btnItalic);

        btnUnderline = new ToolStripButton();
        btnUnderline.ImageKey = "Underline";
        btnUnderline.ToolTipText = "Underline";
        btnUnderline.Tag = FontStyle.Underline;
        btnUnderline.CheckOnClick = true;
        btnUnderline.Click += FontStyleOnClick;
        tool.Items.Add(btnUnderline);

        btnStrikeout = new ToolStripButton();
        btnStrikeout.ImageKey = "Strikeout";
        btnStrikeout.ToolTipText = "Strikeout";
        btnStrikeout.CheckOnClick = true;
        btnStrikeout.Tag = FontStyle.Strikeout;
        btnStrikeout.Click += FontStyleOnClick; 
        tool.Items.Add(btnStrikeout);
        tool.Items.Add(new ToolStripSeparator());

        // Create background color drop-down button.
        ToolStripSplitButton spltbtn = new ToolStripSplitButton();
        spltbtn.ImageKey = "BackColor";
        spltbtn.ToolTipText = "Background Color";
        spltbtn.ButtonClick += delegate 
        { 
            txtbox.SelectionBackColor = txtbox.BackColor; 
        };
        spltbtn.DropDownOpening += ColorOnDropDownOpening;
        tool.Items.Add(spltbtn);

        // Create PropertyInfo for background color.
        PropertyInfo pi = typeof(RichTextBox).GetProperty("SelectionBackColor");

        // Add Color Grid to drop down.
        ToolStripColorGrid clrgrid = new ToolStripColorGrid();
        clrgrid.Name = "ColorGrid";
        clrgrid.Tag = pi;
        clrgrid.Click += ColorGridOnClick;
        spltbtn.DropDownItems.Add(clrgrid);
        spltbtn.DropDownItems.Add(new ToolStripSeparator());

        // Add "More Colors" item to drop down.
        ToolStripMenuItem item = new ToolStripMenuItem("More colors...");
        item.Tag = pi;
        item.Click += MoreColorsOnClick;
        spltbtn.DropDownItems.Add(item);

        // Create foreground color button likewise.
        spltbtn = new ToolStripSplitButton();
        spltbtn.ImageKey = "ForeColor";
        spltbtn.ToolTipText = "Font Color";
        spltbtn.ButtonClick += delegate
        {
            txtbox.SelectionColor = txtbox.ForeColor;
        };
        spltbtn.DropDownOpening += ColorOnDropDownOpening;
        tool.Items.Add(spltbtn);

        // Create PropertyInfo for foreground color.
        pi = typeof(RichTextBox).GetProperty("SelectionColor");

        // Add Color Grid and "More Colors" items.
        clrgrid = new ToolStripColorGrid();
        clrgrid.Name = "ColorGrid";
        clrgrid.Tag = pi;
        clrgrid.Click += ColorGridOnClick;
        spltbtn.DropDownItems.Add(clrgrid);
        spltbtn.DropDownItems.Add(new ToolStripSeparator());

        item = new ToolStripMenuItem("More colors...");
        item.Tag = pi;
        item.Click += MoreColorsOnClick;
        spltbtn.DropDownItems.Add(item);

        tool.Items.Add(new ToolStripSeparator());
        
        // Create buttons for left, right, and center alignment.
        btnLeft = new ToolStripButton();
        btnLeft.ImageKey = "Left";
        btnLeft.ToolTipText = "Align Left";
        btnLeft.Tag = HorizontalAlignment.Left;
        btnLeft.Checked = true;
        btnLeft.Click += AlignOnClick;
        tool.Items.Add(btnLeft);

        btnRight = new ToolStripButton();
        btnRight.ImageKey = "Right";
        btnLeft.ToolTipText = "Align Right";
        btnRight.Tag = HorizontalAlignment.Right;
        btnRight.Click += AlignOnClick;
        tool.Items.Add(btnRight);

        btnCenter = new ToolStripButton();
        btnCenter.ImageKey = "Center";
        btnLeft.ToolTipText = "Align Center";
        btnCenter.Tag = HorizontalAlignment.Center;
        btnCenter.Click += AlignOnClick;
        tool.Items.Add(btnCenter);

        // Create button for bullets.
        btnBullets = new ToolStripButton();
        btnBullets.ImageKey = "Bullets";
        btnBullets.ToolTipText = "Bullets";
        btnBullets.CheckOnClick = true;
        btnBullets.Click += BulletsOnClick;
        tool.Items.Add(btnBullets);
        tool.Items.Add(new ToolStripSeparator());

        // Create labels and text boxes for indentation.
        ToolStripLabel lbl = new ToolStripLabel("Left:");
        tool.Items.Add(lbl);

        txtLeftIndent = new ToolStripTextBox();
        txtLeftIndent.ToolTipText = "Left Indentation in Inches";
        txtLeftIndent.TextChanged += IndentOnTextChanged;
        tool.Items.Add(txtLeftIndent);

        lbl = new ToolStripLabel("Right:");
        tool.Items.Add(lbl);

        txtRightIndent = new ToolStripTextBox();
        txtRightIndent.ToolTipText = "Right Indentation in Inches";
        txtRightIndent.TextChanged += IndentOnTextChanged;
        tool.Items.Add(txtRightIndent);

        lbl = new ToolStripLabel("First line:");
        tool.Items.Add(lbl);

        txtFirstLine = new ToolStripTextBox();
        txtFirstLine.ToolTipText = "First Line Indentation in Inches";
        txtFirstLine.TextChanged += IndentOnTextChanged;
        tool.Items.Add(txtFirstLine);

        // Initialize the ToolStrip.
        TextBoxOnSelectionChanged(txtbox, EventArgs.Empty);
    }

    // Creates buttons for Bold, Italic, Underline, and Strikeout
    Bitmap FontStyleBitmap(string str, FontStyle fntstyle)
    {
        Bitmap bm = new Bitmap(16, 16);
        Font fnt = new Font("Times New Roman", 14, fntstyle, GraphicsUnit.Pixel);
        StringFormat strfmt = new StringFormat();
        strfmt.Alignment = StringAlignment.Center;

        Graphics grfx = Graphics.FromImage(bm);
        grfx.DrawString(str, fnt, Brushes.Black, 8, 0, strfmt);
        grfx.Dispose();
        return bm;
    }

    // whenever the RichTextBox selection changes, alter the ToolStrip items.
    void TextBoxOnSelectionChanged(object objSrc, EventArgs args)
    {
        if (bSuspendSelectionChanged)
            return;

        Font fnt = txtbox.SelectionFont;

        if (fnt != null)
        {
            comboName.SelectedItem = fnt.Name;
            comboSize.Text = fnt.Size.ToString();

            btnBold.Checked = (fnt.Style & FontStyle.Bold) != 0;
            btnItalic.Checked = (fnt.Style & FontStyle.Italic) != 0;
            btnUnderline.Checked = (fnt.Style & FontStyle.Underline) != 0;
            btnStrikeout.Checked = (fnt.Style & FontStyle.Strikeout) != 0;
        }
        else
        {
            comboName.SelectedItem = null;
            comboSize.SelectedItem = null;

            btnBold.CheckState = CheckState.Unchecked;
            btnItalic.CheckState = CheckState.Unchecked;
            btnUnderline.CheckState = CheckState.Unchecked;
            btnStrikeout.CheckState = CheckState.Unchecked;
        }

        HorizontalAlignment hAlign = txtbox.SelectionAlignment;
        btnLeft.Checked = hAlign == HorizontalAlignment.Left;
        btnRight.Checked = hAlign == HorizontalAlignment.Right;
        btnCenter.Checked = hAlign == HorizontalAlignment.Center;
        btnBullets.Checked = txtbox.SelectionBullet;

        txtLeftIndent.Text = ((txtbox.SelectionIndent + 
                        txtbox.SelectionHangingIndent) / xDpi).ToString();
        txtRightIndent.Text = (txtbox.SelectionRightIndent / xDpi).ToString();
        txtFirstLine.Text = (-txtbox.SelectionHangingIndent / xDpi).ToString();
    }

    // Change the font family name.
    void NameOnSelectionChanged(object objSrc, EventArgs args)
    {
        ChangeFont(comboName.Text, 0, 0, false);
    }

    // Change the font size.
    void SizeOnSelectionChanged(object objSrc, EventArgs args)
    {
        float fSize = float.Parse(comboSize.Text);
        ChangeFont(null, fSize, 0, false);
    }

    // Change the font style.
    void FontStyleOnClick(object objSrc, EventArgs args)
    {
        ToolStripButton btn = (ToolStripButton)objSrc;
        FontStyle fntstyle = (FontStyle)btn.Tag;
        ChangeFont(null, 0, (FontStyle)btn.Tag, btn.Checked);
    }

    // Master method to change the font.
    void ChangeFont(string strName, float fSize, FontStyle fntsty, bool bAdd)
    {
        bSuspendSelectionChanged = true;

        int iSelStart = txtbox.SelectionStart;
        int iSelLength = txtbox.SelectionLength;

        for (int iStart1 = iSelStart; iStart1 < iSelStart + iSelLength; )
        {
            txtbox.Select(iStart1, 1);
            Font fnt = txtbox.SelectionFont;

            for (int iStart2 = iStart1 + 1; iStart2 <= iSelStart + iSelLength; iStart2++)
            {
                txtbox.Select(iStart2, 1);
                Font fntNext = txtbox.SelectionFont;

                if (iStart2 == iSelStart + iSelLength || !fnt.Equals(fntNext))
                {
                    txtbox.Select(iStart1, iStart2 - iStart1);

                    if (strName != null)
                        txtbox.SelectionFont = new Font(strName, fnt.Size, fnt.Style);
                    else if (fSize != 0)
                        txtbox.SelectionFont = new Font(fnt.Name, fSize, fnt.Style);
                    else if (bAdd)
                        txtbox.SelectionFont = new Font(fnt, fnt.Style | fntsty);
                    else
                        txtbox.SelectionFont = new Font(fnt, fnt.Style & ~fntsty);

                    iStart1 = iStart2;
                    break;
                }
            }
        }
        bSuspendSelectionChanged = false;
        txtbox.Select(iSelStart, iSelLength);
    }

    // Initialize the color grid when the drop down is opening.
    void ColorOnDropDownOpening(object objSrc, EventArgs args)
    {
        ToolStripSplitButton btn = (ToolStripSplitButton)objSrc;
        ToolStripColorGrid clrgrid = (ToolStripColorGrid)btn.DropDownItems["ColorGrid"];
        PropertyInfo pi = (PropertyInfo) clrgrid.Tag;
        clrgrid.SelectedColor = (Color)pi.GetValue(txtbox, null);
    }

    // Obtain a new color from the color grid.
    void ColorGridOnClick(object objSrc, EventArgs args)
    {
        ToolStripColorGrid clrgrid = (ToolStripColorGrid)objSrc;
        PropertyInfo pi = (PropertyInfo)clrgrid.Tag;
        pi.SetValue(txtbox, clrgrid.SelectedColor, null);
    }

    // Display the standard color dialog.
    void MoreColorsOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = (ToolStripMenuItem)objSrc;
        PropertyInfo pi = (PropertyInfo)item.Tag;
        clrdlg.Color = (Color)pi.GetValue(txtbox, null);

        if (clrdlg.ShowDialog() == DialogResult.OK)
            pi.SetValue(txtbox, clrdlg.Color, null);
    }

    // Change the alignment based on the button pressed.
    void AlignOnClick(object objSrc, EventArgs args)
    {
        ToolStripButton btn = (ToolStripButton)objSrc;
        btnLeft.Checked = btnRight.Checked = btnCenter.Checked = false;
        btn.Checked = true;
        txtbox.SelectionAlignment = (HorizontalAlignment) btn.Tag;
    }

    // Change the bullet formatting.
    void BulletsOnClick(object objSrc, EventArgs args)
    {
        ToolStripButton btn = (ToolStripButton)objSrc;
        txtbox.SelectionBullet = btn.Checked;
    }

    // Change the indents based on the number in the text boxes.
    void IndentOnTextChanged(object objSrc, EventArgs args)
    {
        try
        {
            int iLeftIndent = (int)(xDpi * float.Parse(txtLeftIndent.Text));
            int iRightIndent = (int)(xDpi * float.Parse(txtRightIndent.Text));
            int iFirstLine = (int)(xDpi * float.Parse(txtFirstLine.Text));

            txtbox.SelectionIndent = iLeftIndent + iFirstLine;
            txtbox.SelectionHangingIndent = -iFirstLine;
            txtbox.SelectionRightIndent = iRightIndent;
        }
        catch
        {
        }
    }
}