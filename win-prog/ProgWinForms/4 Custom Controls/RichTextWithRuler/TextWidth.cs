//------------------------------------------
// TextWidth.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class TextWidth : RulerSlider
{
    public TextWidth()
    {
        RulerProperty = RulerProperty.TextWidth;
    }
    public override void Draw(Graphics grfx)
    {
    }
    public override bool HitTest(Point pt)
    {
        return (pt.X >= X - 2) && (pt.X <= X + 2);
    }
    public override Rectangle Rectangle
    {
        get { return Rectangle.Empty; }
    }
}
