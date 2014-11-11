//---------------------------------------------------
// RichTextWithStatus.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class RichTextWithStatus: FormattingToolStrip
{
    ToolStripStatusLabel lblSelection, lblDateTime;

    [STAThread]
    public static new void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new RichTextWithStatus());
    }
    public RichTextWithStatus()
    {
        Text = "Rich Text with Status";

        txtbox.SelectionChanged += TextBoxOnSelectionChanged;

        StatusStrip stat = new StatusStrip();
        stat.Parent = this;

        lblSelection = new ToolStripStatusLabel();
        stat.Items.Add(lblSelection);

        lblDateTime = new ToolStripStatusLabel();
        lblDateTime.Alignment = ToolStripItemAlignment.Right;
        stat.Items.Add(lblDateTime);

        Timer tmr = new Timer();
        tmr.Interval = 1000;
        tmr.Enabled = true;
        tmr.Tick += TimerOnTick;
        
        // Initialize labels.
        TextBoxOnSelectionChanged(txtbox, EventArgs.Empty);
        TimerOnTick(tmr, EventArgs.Empty);
    }
    void TextBoxOnSelectionChanged(object objSrc, EventArgs args)
    {
        RichTextBox txtbox = (RichTextBox) objSrc;

        int iSelStart = txtbox.SelectionStart;
        int iSelLength = txtbox.SelectionLength;
        int iSelEnd = iSelStart + iSelLength;

        int iLine = txtbox.GetLineFromCharIndex(iSelStart);
        int iChar = iSelStart - txtbox.GetFirstCharIndexFromLine(iLine);
        lblSelection.Text = String.Format("Line {0} Character {1}", 
            iLine + 1, iChar + 1);

        if (iSelLength > 0)
        {
            iLine = txtbox.GetLineFromCharIndex(iSelEnd);
            iChar = iSelEnd - txtbox.GetFirstCharIndexFromLine(iLine);
            lblSelection.Text += String.Format(" - Line {0} Character {1}",
                iLine + 1, iChar + 1);
        }
    }
    void TimerOnTick(object objSrc, EventArgs args)
    {
        lblDateTime.Text = DateTime.Now.ToString("G");
    }
}
