//--------------------------------------------
// RulerSlider.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

abstract class RulerSlider
{
    // Private fields.
    RulerProperty rlrprop;
    float fValue;
    int x, y;
    Bitmap bm;
    Region rgn;

    // Public properties.
    public RulerProperty RulerProperty
    {
        get { return rlrprop; }
        set { rlrprop = value; }
    }
    public float Value
    {
        get { return fValue; }
        set { fValue = value; }
    }
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public virtual Rectangle Rectangle
    {
        get
        {
            return new Rectangle(X - bm.Width / 2, Y, bm.Width, bm.Height);
        }
    }

    // Protected property.
    protected int Y
    {
        get { return y; }
        set { y = value; }
    }

    // Public methods.
    public virtual void Draw(Graphics grfx)
    {
        grfx.DrawImage(bm, X - bm.Width / 2, Y);
    }
    public virtual bool HitTest(Point pt)
    {
        return rgn.IsVisible(pt.X - X + bm.Width / 2, pt.Y - Y);
    }
    protected void CreateBitmap(int cx, int cy, Point[] apt)
    {
        bm = new Bitmap(cx, cy);

        GraphicsPath path = new GraphicsPath();
        path.AddLines(apt);
        rgn = new Region(path);

        Graphics grfx = Graphics.FromImage(bm);
        grfx.FillPolygon(Brushes.LightGray, apt);
        grfx.Clip = rgn;

        Shading(grfx, Pens.White, 1, apt);
        Shading(grfx, Pens.Gray, -1, apt);

        grfx.ResetClip();
        grfx.DrawPolygon(Pens.Black, apt);
        grfx.Dispose();
    }
    void Shading(Graphics grfx, Pen pn, int iOffset, Point[] apt)
    {
        grfx.TranslateTransform(iOffset, 0);
        grfx.DrawPolygon(pn, apt);
        grfx.TranslateTransform(-iOffset, iOffset);
        grfx.DrawPolygon(pn, apt);
        grfx.TranslateTransform(0, -iOffset);
    }
}
