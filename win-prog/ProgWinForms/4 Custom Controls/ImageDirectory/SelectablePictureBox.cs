//-----------------------------------------------------
// SelectablePictureBox.cs (c) 2005 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SelectablePictureBox : PictureBox
{
    public SelectablePictureBox()
    {
        SetStyle(ControlStyles.Selectable, true);
        TabStop = true;
    }
    protected override void OnMouseDown(MouseEventArgs args)
    {
        base.OnMouseDown(args);
        Focus();
    }
    protected override void OnKeyPress(KeyPressEventArgs args)
    {
        if (args.KeyChar == '\r')
            OnClick(EventArgs.Empty);
        else
            base.OnKeyPress(args);
    }
    protected override void OnEnter(EventArgs args)
    {
        base.OnEnter(args);
        Invalidate();
    }
    protected override void OnLeave(EventArgs e)
    {
        base.OnLeave(e);
        Invalidate();
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        base.OnPaint(args);

        if (Focused)
        {
            Graphics grfx = args.Graphics;
            grfx.DrawRectangle(new Pen(Brushes.Black, grfx.DpiX / 12),
                ClientRectangle);
        }
    }
}

