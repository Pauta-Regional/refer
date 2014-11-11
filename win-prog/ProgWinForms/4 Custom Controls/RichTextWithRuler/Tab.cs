//------------------------------------
// Tab.cs (c) 2005 by Charles Petzold
//------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class Tab : RulerSlider
{
    public Tab()
    {
        RulerProperty = RulerProperty.Tabs;
        Y = 9;
    }
    public override void Draw(Graphics grfx)
    {
        Pen pn = new Pen(Color.Black, 2);

        grfx.DrawLine(pn, X, Y, X, Y + 4);
        grfx.DrawLine(pn, X, Y + 4, X + 4, Y + 4);
    }
    public override bool HitTest(Point pt)
    {
        return pt.X >= X - 1 && pt.X <= X + 1 && pt.Y >= Y - 1 && pt.Y <= Y + 6;
    }
    public override Rectangle Rectangle
    {
        get
        {
            return new Rectangle(X - 1, Y - 1, 6, 6);
        }
    }
}
