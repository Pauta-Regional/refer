//------------------------------------------
// ColorGrid.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorGrid : Control
{
    // Number of colors horizontally and vertically.
    const int xNum = 8;
    const int yNum = 5;

    // The colors.
    Color[,] aclr = new Color[yNum, xNum] 
    {
        { Color.Black, Color.Brown, Color.DarkGreen, Color.MidnightBlue,
            Color.Navy, Color.DarkBlue, Color.Indigo, Color.DimGray },

        { Color.DarkRed, Color.OrangeRed, Color.Olive, Color.Green, 
            Color.Teal, Color.Blue, Color.SlateGray, Color.Gray },

        { Color.Red, Color.Orange, Color.YellowGreen, Color.SeaGreen, 
            Color.Aqua, Color.LightBlue, Color.Violet, Color.DarkGray },

        { Color.Pink, Color.Gold, Color.Yellow, Color.Lime, 
            Color.Turquoise, Color.SkyBlue, Color.Plum, Color.LightGray },

        { Color.LightPink, Color.Tan, Color.LightYellow, Color.LightGreen, 
            Color.LightCyan, Color.LightSkyBlue, Color.Lavender, Color.White }
    };

    // Selected color as a private field.
    Color clrSelected = Color.Black;

    // Rectangles for displaying colors and borders.
    Rectangle rectTotal, rectGray, rectBorder, rectColor;

    // The coordinate currently highlighted by keyboard or mouse.
    int xHighlight = -1;
    int yHighlight = -1;

    // Constructor.
    public ColorGrid()
    {
        AutoSize = true;

        // Obtain the resolution of the screen
        Graphics grfx = CreateGraphics();
        int xDpi = (int)grfx.DpiX;
        int yDpi = (int)grfx.DpiY;
        grfx.Dispose();

        // Calculate rectangles for color displays
        rectTotal = new Rectangle(0, 0, xDpi / 5, yDpi / 5);
        rectGray = Rectangle.Inflate(rectTotal, -xDpi / 72, -yDpi / 72);
        rectBorder = Rectangle.Inflate(rectGray, -xDpi / 48, -yDpi / 48);
        rectColor = Rectangle.Inflate(rectBorder, -xDpi / 72, -yDpi / 72);
    }

    // SelectedColor property -- access to clrSelected field
    public Color SelectedColor
    {
        get
        {
            return clrSelected;
        }
        set
        {
            clrSelected = value;
            Invalidate();
        }
    }

    // Required for autosizing.
    public override Size GetPreferredSize(Size sz)
    {
        return new Size(xNum * rectTotal.Width, yNum * rectTotal.Height);
    }

    // Draw all colors in the grid.
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;

        for (int y = 0; y < yNum; y++)
            for (int x = 0; x < xNum; x++)
                DrawColor(grfx, x, y, false);
    }

    // Draw an individual color. (grfx can be null)
    void DrawColor(Graphics grfx, int x, int y, bool bHighlight)
    {
        bool bDisposeGraphics = false;

        if (x < 0 || y < 0 || x >= xNum || y >= yNum)
            return;

        if (grfx == null)
        {
            grfx = CreateGraphics();
            bDisposeGraphics = true;
        }

        // Determine if the color is currently selected.
        bool bSelect = aclr[y, x].ToArgb() == SelectedColor.ToArgb();

        Brush br = (bHighlight | bSelect) ? SystemBrushes.HotTrack :
                        SystemBrushes.Menu;

        // Start draw rectangles.
        Rectangle rect = rectTotal;
        rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        grfx.FillRectangle(br, rect);

        if (bHighlight || bSelect)
        {
            br = bHighlight ? SystemBrushes.ControlDark :
                                SystemBrushes.ControlLight;
            rect = rectGray;
            rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
            grfx.FillRectangle(br, rect);
        }

        rect = rectBorder;
        rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        grfx.FillRectangle(SystemBrushes.ControlDark, rect);

        rect = rectColor;
        rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        grfx.FillRectangle(new SolidBrush(aclr[y, x]), rect);

        if (bDisposeGraphics)
            grfx.Dispose();
    }

    // Methods for mouse movement and clicks.
    protected override void OnMouseEnter(EventArgs args)
    {
        xHighlight = -1;
        yHighlight = -1;
    }
    protected override void OnMouseMove(MouseEventArgs args)
    {
        int x = args.X / rectTotal.Width;
        int y = args.Y / rectTotal.Height;

        if (x != xHighlight || y != yHighlight)
        {
            DrawColor(null, xHighlight, yHighlight, false);
            DrawColor(null, x, y, true);

            xHighlight = x;
            yHighlight = y;
        }
    }
    protected override void OnMouseLeave(EventArgs args)
    {
        DrawColor(null, xHighlight, yHighlight, false);

        xHighlight = -1;
        yHighlight = -1;
    }
    protected override void OnMouseDown(MouseEventArgs args)
    {
        int x = args.X / rectTotal.Width;
        int y = args.Y / rectTotal.Height;
        SelectedColor = aclr[y, x];
        base.OnMouseDown(args);         // Generates Click event.
        Focus();
    }

    // Methods for keyboard interface.
    protected override void OnEnter(EventArgs args)
    {
        if (xHighlight < 0 || yHighlight < 0)
            for (yHighlight = 0; yHighlight < yNum; yHighlight++)
            {
                for (xHighlight = 0; xHighlight < xNum; xHighlight++)
                {
                    if (aclr[yHighlight, xHighlight].ToArgb() == 
                                    SelectedColor.ToArgb())
                        break;
                }
                if (xHighlight < xNum)
                    break;
            }

        if (xHighlight == xNum && yHighlight == yNum)
            xHighlight = yHighlight = 0;

        DrawColor(null, xHighlight, yHighlight, true);
    }
    protected override void OnLeave(EventArgs args)
    {
        DrawColor(null, xHighlight, yHighlight, false);
        xHighlight = yHighlight = -1;
    }
    protected override bool IsInputKey(Keys keyData)
    {
        return keyData == Keys.Home || keyData == Keys.End || 
               keyData == Keys.Up || keyData == Keys.Down || 
               keyData == Keys.Left || keyData == Keys.Right;
    }
    protected override void OnKeyDown(KeyEventArgs args)
    {
        DrawColor(null, xHighlight, yHighlight, false);
        int x = xHighlight, y = yHighlight;

        switch (args.KeyCode)
        {
            case Keys.Home:
                x = y = 0;
                break;

            case Keys.End:
                x = xNum - 1;
                y = yNum - 1;
                break;

            case Keys.Right:
                if (++x == xNum)
                {
                    x = 0;
                    if (++y == yNum)
                    {
                        Parent.GetNextControl(this, true).Focus();
                    }
                }
                break;

            case Keys.Left:
                if (--x == -1)
                {
                    x = xNum - 1;
                    if (--y == -1)
                    {
                        Parent.GetNextControl(this, false).Focus();
                    }
                }
                break;

            case Keys.Down:
                if (++y == yNum)
                {
                    y = 0;
                    if (++x == xNum)
                    {
                        Parent.GetNextControl(this, true).Focus();
                    }
                }
                break;

            case Keys.Up:
                if (--y == -1)
                {
                    y = 0;
                    if (--x == -1)
                    {
                        Parent.GetNextControl(this, false).Focus();
                    }
                }
                break;

            case Keys.Enter:
            case Keys.Space:
                SelectedColor = aclr[y, x];
                OnClick(EventArgs.Empty);
                break;

            default:
                base.OnKeyDown(args);
                return;
        }
        DrawColor(null, x, y, true);

        xHighlight = x;
        yHighlight = y;
    }
}