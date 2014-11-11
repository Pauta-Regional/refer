//------------------------------------
// Rgb.cs (c) 2005 by Charles Petzold
//------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class Rgb
{
    public event EventHandler ColorChanged;
    int r, g, b;

    public int Red
    {
        get { return r; }
        set 
        { 
            r = value; 
            OnColorChanged(this, EventArgs.Empty);
        }
    }
    public int Green
    {
        get { return g; }
        set
        {
            g = value;
            OnColorChanged(this, EventArgs.Empty);
        }
    }
    public int Blue
    {
        get { return b; }
        set
        {
            b = value;
            OnColorChanged(this, EventArgs.Empty);
        }
    }
    public Color Color
    {
        get { return Color.FromArgb(Red, Green, Blue); }
        set
        {
            r = value.R;
            g = value.G;
            b = value.B;
            OnColorChanged(this, EventArgs.Empty);
        }
    }
    protected virtual void OnColorChanged(object objSrc, EventArgs args)
    {
        if (ColorChanged != null)
            ColorChanged(objSrc, args);
    }
}
