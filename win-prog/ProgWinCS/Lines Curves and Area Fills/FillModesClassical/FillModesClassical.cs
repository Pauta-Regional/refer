//-------------------------------------------------
// FillModesClassical.cs � 2001 by Charles Petzold
//-------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class FillModesClassical: PrintableForm
{
     public new static void Main()
     {
          Application.Run(new FillModesClassical());
     }
     public FillModesClassical()
     {
          Text = "Alternate and Winding Fill Modes (The Classical Example)";
          ClientSize = new Size(2 * ClientSize.Height, ClientSize.Height);
     }
     protected override void DoPage(Graphics grfx, Color clr, int cx, int cy)
     {
          Brush   brush = new SolidBrush(clr);
          Point[] apt   = new Point[5];
          
          for (int i = 0; i < apt.Length; i++)
          {
               double dAngle = (i * 0.8 - 0.5) * Math.PI;
               apt[i] = new Point(
                              (int)(cx *(0.25 + 0.24 * Math.Cos(dAngle))),
                              (int)(cy *(0.50 + 0.48 * Math.Sin(dAngle))));
          }
          grfx.FillPolygon(brush, apt, FillMode.Alternate);

          for (int i = 0; i < apt.Length; i++) 
               apt[i].X += cx / 2;         

          grfx.FillPolygon(brush, apt, FillMode.Winding);
     }
}
