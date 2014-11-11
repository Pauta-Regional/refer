//--------------------------------------------------
// ClickmatricButton.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Petzold.ProgrammingWindowsForms
{
    class ClickmaticButton : Button
    {
        Timer tmr = new Timer();

        int iDelay = 250 * (1 + SystemInformation.KeyboardDelay);
        int iSpeed = 405 - 12 * SystemInformation.KeyboardSpeed;

        protected override void OnMouseDown(MouseEventArgs args)
        {
            base.OnMouseDown(args);

            if ((args.Button & MouseButtons.Left) != 0)
            {
                tmr.Interval = iDelay;
                tmr.Tick += TimerOnTick;
                tmr.Start();
            }
        }
        void TimerOnTick(object objSrc, EventArgs args)
        {
            OnClick(EventArgs.Empty);
            tmr.Interval = iSpeed;
        }
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            tmr.Enabled = Capture & ClientRectangle.Contains(args.Location);
        }
        protected override void OnMouseUp(MouseEventArgs args)
        {
            base.OnMouseUp(args);
            tmr.Stop();
        }
    }
}


