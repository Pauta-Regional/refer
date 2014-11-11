//-------------------------------------------
// RoundButton.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class RoundButton : Button
{
    public RoundButton()
    {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }
    public override Size GetPreferredSize(Size szProposed)
    {
        // Base size on text string to be displayed.
        Graphics grfx = CreateGraphics();
        SizeF szf = grfx.MeasureString(Text, Font);
        int iRadius = (int)Math.Sqrt(Math.Pow(szf.Width / 2, 2) + 
                                     Math.Pow(szf.Height / 2, 2));
        return new Size(2 * iRadius, 2 * iRadius);
    }
    protected override void OnResize(EventArgs args)
    {
        base.OnResize(args);

        // Circular region makes button non-rectangular.
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(ClientRectangle);
        Region = new Region(path);
    }
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;
        grfx.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = ClientRectangle;

        // Draw interior (darker if pressed).
        bool bPressed = Capture & ((MouseButtons & MouseButtons.Left) != 0) &
            ClientRectangle.Contains(PointToClient(MousePosition));

        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(rect);
        PathGradientBrush pgbr = new PathGradientBrush(path);
        int k = bPressed ? 2 : 1;
        pgbr.CenterPoint = new PointF(k * (rect.Left + rect.Right) / 3,
                                      k * (rect.Top + rect.Bottom) / 3);
        pgbr.CenterColor = bPressed ? Color.Blue : Color.White;
        pgbr.SurroundColors = new Color[] { Color.SkyBlue };
        grfx.FillRectangle(pgbr, rect);

        // Display border (thicker for default button)
        Brush br = new LinearGradientBrush(rect,
                        Color.FromArgb(0, 0, 255), Color.FromArgb(0, 0, 128),
                        LinearGradientMode.ForwardDiagonal);
        Pen pn = new Pen(br, (IsDefault ? 4 : 2) * grfx.DpiX / 72);
        grfx.DrawEllipse(pn, rect);

        // Draw the text centered in the rectangle (grayed if disabled).
        StringFormat strfmt = new StringFormat();
        strfmt.Alignment = strfmt.LineAlignment = StringAlignment.Center;
        br = Enabled ? SystemBrushes.WindowText : SystemBrushes.GrayText;
        grfx.DrawString(Text, Font, br, rect, strfmt);

        // Draw dotted line around text if button has input focus.
        if (Focused)
        {
            SizeF szf = grfx.MeasureString(Text, Font, PointF.Empty, 
                                           StringFormat.GenericTypographic);
            pn = new Pen(ForeColor);
            pn.DashStyle = DashStyle.Dash;
            grfx.DrawRectangle(pn, 
                rect.Left + rect.Width / 2 - szf.Width / 2, 
                rect.Top + rect.Height / 2 - szf.Height / 2, 
                szf.Width, szf.Height);
        }
    }
}
