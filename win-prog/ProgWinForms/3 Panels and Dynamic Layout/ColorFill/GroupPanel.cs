//-------------------------------------------
// GroupPanel.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class GroupPanel : FlowLayoutPanel
{
    int xDpi, yDpi;

    public GroupPanel()
    {
        FlowDirection = FlowDirection.TopDown;
        WrapContents = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        Graphics grfx = CreateGraphics();
        xDpi = (int)grfx.DpiX;
        yDpi = (int)grfx.DpiY;
        grfx.Dispose();

        Padding = new Padding(xDpi / 10, yDpi / 10 + Font.Height, 
                              xDpi / 10, yDpi / 10);
    }
    public string Check
    {
        set
        {
            RadioButton radio = Controls[value] as RadioButton;
            if (radio != null)
                radio.Checked = true;
        }
        get
        {
            foreach (Control ctrl in Controls)
            {
                RadioButton radio = ctrl as RadioButton;
                if (radio != null && radio.Checked)
                    return radio.Name;
            }
            return "";
        }
    }
    protected override void OnFontChanged(EventArgs args)
    {
        base.OnFontChanged(args);
        Padding = new Padding(Padding.Left, yDpi / 10 + Font.Height,
                              Padding.Right, Padding.Bottom);
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        int yIndent = yDpi / 25 + Font.Height / 2;
        int xIndent1 = xDpi / 10, xIndent2;

        if (Text != null && Text.Length > 0)
        {
            grfx.DrawString(" " + Text + " ", Font, 
                            new SolidBrush(ForeColor), xIndent1, yDpi / 25);
            xIndent2 = xIndent1 + (int) (grfx.MeasureString(" " + Text + " ", 
                                                            Font).Width);
        }
        else
        {
            xIndent2 = xIndent1;
        }

        Pen pnLight = new Pen(ControlPaint.Light(BackColor));
        Pen pnDark = new Pen(ControlPaint.Dark(BackColor));

        grfx.DrawLine(pnDark, xIndent1, yIndent, 0, yIndent);
        grfx.DrawLine(pnDark, 0, yIndent, 0, Height - 2);
        grfx.DrawLine(pnDark, 0, Height - 2, Width - 2, Height - 2);
        grfx.DrawLine(pnDark, Width - 2, Height - 2, Width - 2, yIndent);
        grfx.DrawLine(pnDark, Width - 2, yIndent, xIndent2, yIndent);
        
        grfx.DrawLine(pnLight, xIndent1, yIndent + 1, 1, yIndent + 1);
        grfx.DrawLine(pnLight, 1, yIndent + 1, 1, Height - 3);
        grfx.DrawLine(pnLight, 0, Height - 1, Width - 1, Height - 1);
        grfx.DrawLine(pnLight, Width - 1, Height - 1, Width - 1, yIndent);
        grfx.DrawLine(pnLight, Width - 3, yIndent + 1, xIndent2, yIndent + 1);
    }
}