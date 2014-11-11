//------------------------------------------------
// FirstLineIndent.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class FirstLineIndent : RulerSlider
{
    public FirstLineIndent()
    {
        RulerProperty = RulerProperty.FirstLineIndent;
        Y = 1;
        CreateBitmap(9, 8, new Point[] 
        { 
            new Point(0, 0), new Point(8, 0), new Point(8, 3), 
            new Point(4, 7), new Point(0, 3), new Point(0, 0) 
        });
    }
}
