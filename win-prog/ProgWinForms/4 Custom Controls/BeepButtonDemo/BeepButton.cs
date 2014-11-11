//-------------------------------------------
// BeepButton.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

class BeepButton : Button
{
    protected override void OnClick(EventArgs args)
    {
        SystemSounds.Exclamation.Play();
        base.OnClick(args);
    }
}
