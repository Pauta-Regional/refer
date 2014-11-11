//-------------------------------------------
// LeftIndent.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class LeftIndent : RulerSlider
{
    public LeftIndent()
    {
        RulerProperty = RulerProperty.LeftIndent;
        Y = 9;
        CreateBitmap(9, 14, new Point[] 
        { 
            new Point(0, 7), new Point(0, 4), new Point(4, 0), new Point(8, 4),
            new Point(8, 7), new Point(0, 7), new Point(0, 13), new Point(8, 13),
            new Point(8, 7), new Point(0, 7) 
        });
    }
}
