//---------------------------------------------
// DisplayPanel.cs (c) 2005 by Charles Petzold
//---------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class DisplayPanel : Panel
{
    Matrix matx = new Matrix();

    public DisplayPanel()
    {
        ResizeRedraw = true;
    }
    public Matrix Transform
    {
        set
        {
            matx = value;
            Invalidate();
        }
        get
        {
            return matx;
        }
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        Brush brsh = new SolidBrush(ForeColor);

        try
        {
            grfx.Transform = matx;
            grfx.DrawString(Text, Font, brsh, Point.Empty);
        }
        catch (Exception exc)
        {
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = strfmt.LineAlignment = StringAlignment.Center;
            grfx.DrawString(exc.Message, Font, brsh, ClientRectangle, strfmt);
        }
        brsh.Dispose();
    }
}