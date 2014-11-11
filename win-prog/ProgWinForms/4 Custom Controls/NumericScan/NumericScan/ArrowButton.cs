//--------------------------------------------
// ArrowButton.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Petzold.ProgrammingWindowsForms
{
    class ArrowButton : ClickmaticButton
    {
        ScrollButton scrbtn = ScrollButton.Right;

        public ArrowButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
        public ScrollButton ScrollButton
        {
            set
            {
                scrbtn = value;
                Invalidate();
            }
            get { return scrbtn; }
        }
        protected override void OnPaint(PaintEventArgs args)
        {
            Graphics grfx = args.Graphics;
            ControlPaint.DrawScrollButton(grfx, ClientRectangle, scrbtn,
                !Enabled ? ButtonState.Inactive :
                    (Capture & ClientRectangle.Contains(
                                        PointToClient(MousePosition))) ?
                ButtonState.Pushed : ButtonState.Normal);
        }
        protected override void OnMouseCaptureChanged(EventArgs args)
        {
            base.OnMouseCaptureChanged(args);
            Invalidate();
        }
    }
}
