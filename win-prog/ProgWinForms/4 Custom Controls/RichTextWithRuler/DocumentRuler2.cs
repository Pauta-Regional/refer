//-----------------------------------------------
// DocumentRuler2.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class DocumentRuler : Control
{
    // Public event.
    public event RulerEventHandler Change;

    // Private fields used during mouse dragging.
    RulerSlider rsDragging;
    Point ptDown;
    int xOriginal;
    int xLineOverTextBox;

    // OnPaint method handles virtually all drawing.
    protected override void OnPaint(PaintEventArgs args)
    {
        Graphics grfx = args.Graphics;

        Rectangle rect = new Rectangle(LeftMargin, 0, 
                                rsTextWidth.X - LeftMargin, Height - 4);
        grfx.FillRectangle(Brushes.White, rect);
        ControlPaint.DrawBorder3D(grfx, rect);

        for (int i = 1; i < 8 * PixelsToInches(Width); i++)
        {
            int x = LeftMargin + InchesToPixels(i / 8f);

            if (i % 8 == 0)
            {
                StringFormat strfmt = new StringFormat();
                strfmt.Alignment = strfmt.LineAlignment = StringAlignment.Center;
                grfx.DrawString((i / 8).ToString(), Font, 
                                Brushes.Black, x, 9, strfmt);
            }
            else if (i % 4 == 0)
            {
                grfx.DrawLine(Pens.Black, x, 7, x, 10);
            }
            else
            {
                grfx.DrawLine(Pens.Black, x, 8, x, 9);
            }
        }

        // Display all sliders.
        foreach (RulerSlider rs in rsCollection)
            rs.Draw(grfx);

        return;
    }

    // OnMouseDown for moving sliders and creating tabs.
    protected override void OnMouseDown(MouseEventArgs args)
    {
        // Ignore if it's not the left button.
        if ((args.Button & MouseButtons.Left) == 0)
            return;

        // Loop through existing sliders looking for positive hit test.
        foreach (RulerSlider rs in rsCollection)
            if (rs.HitTest(args.Location))
            {
                rsDragging = rs;
                ptDown = args.Location;
                xOriginal = rsDragging.X;

                if (rsDragging is TextWidth)
                    Cursor.Current = Cursors.SizeWE;

                DrawReversibleLine(xLineOverTextBox = args.X);
                return;
            }
        // If no hit, create a new tab.
        rsDragging = new Tab();
        rsCollection.Add(rsDragging);
        ptDown = args.Location;
        xOriginal = rsDragging.X = ptDown.X;

        Invalidate(rsDragging.Rectangle);
        DrawReversibleLine(xLineOverTextBox = args.X);
        return;
    }

    // OnMouseMove for moving sliders.
    protected override void OnMouseMove(MouseEventArgs args)
    {
        if (!Capture)   // i.e., mouse button not down.
        {
            // If over TextWidth end, change cursor.
            if (!rsRightIndent.HitTest(args.Location) && 
                 rsTextWidth.HitTest(args.Location))
                    Cursor.Current = Cursors.SizeWE;
            return;
        }

        // If rsDragging not null, we're in a drag operation.
        if (rsDragging != null)
        {
            if (rsDragging is TextWidth)
                Cursor.Current = Cursors.SizeWE;

            int xNow = xOriginal - ptDown.X + args.X;

            // Don't let the sliders go out of bounds!
            if (rsDragging is Tab && (xNow < LeftMargin || xNow > rsTextWidth.X))
                return;

            if ((rsDragging == rsLeftIndent || rsDragging == rsFirstIndent) &&
                    (xNow < LeftMargin || xNow > rsRightIndent.X))
                return;

            if (rsDragging == rsRightIndent && (xNow > rsTextWidth.X ||
                    xNow < rsLeftIndent.X || xNow < rsFirstIndent.X))
                return;

            if (rsDragging == rsTextWidth && xNow < rsRightIndent.X)
                return;

            if (rsDragging == rsTextWidth)
            {
                Invalidate(new Rectangle(Math.Min(rsDragging.X, xOriginal) - 1, 0,
                                Math.Abs(rsDragging.X - xOriginal) + 2, Height));
                rsDragging.X = xNow;
            }
            else
            {
                // Update the slider X property and invalidate old and new.
                Invalidate(rsDragging.Rectangle);
                rsDragging.X = xNow;
                Invalidate(rsDragging.Rectangle);
            }
            // Move line over text box.
            DrawReversibleLine(xLineOverTextBox);
            DrawReversibleLine(xLineOverTextBox = args.X);
        }
    }

    // OnMouseUp is new position of slider.
    protected override void OnMouseUp(MouseEventArgs args)
    {
        if (rsDragging != null)
        {
            // Calculate new Value properties and trigger the event.
            if (rsDragging == rsLeftIndent || rsDragging == rsFirstIndent)
            {
                rsLeftIndent.Value = PixelsToInches(rsLeftIndent.X - LeftMargin);
                rsFirstIndent.Value = 
                        PixelsToInches(rsFirstIndent.X - rsLeftIndent.X);
                OnChange(new RulerEventArgs(rsDragging.RulerProperty));
            }
            else if (rsDragging == rsRightIndent || rsDragging == rsTextWidth)
            {
                rsTextWidth.Value = PixelsToInches(rsTextWidth.X - LeftMargin);
                rsRightIndent.Value = 
                        PixelsToInches(rsTextWidth.X - rsRightIndent.X);
                OnChange(new RulerEventArgs(rsTextWidth.RulerProperty));
                OnChange(new RulerEventArgs(rsRightIndent.RulerProperty));
            }

            else if (rsDragging is Tab)
            {
                rsDragging.Value = PixelsToInches(rsDragging.X - LeftMargin);
                OnChange(new RulerEventArgs(rsDragging.RulerProperty));
            }
            // Cease drag operation.
            rsDragging = null;
            DrawReversibleLine(xLineOverTextBox);
        }
    }

    // Draw line down text box in screen coordinates.
    void DrawReversibleLine(int x)
    {
        if (ctrlDocument != null)
        {
            Point pt1 = ctrlDocument.PointToScreen(new Point(x, 0));
            Point pt2 = ctrlDocument.PointToScreen(
                                    new Point(x, ctrlDocument.Height));
            ControlPaint.DrawReversibleLine(pt1, pt2, ctrlDocument.BackColor);
        }
    }

    // OnChange method triggers Change event.
    protected virtual void OnChange(RulerEventArgs args)
    {
        if (Change != null)
            Change(this, args);
    }
}