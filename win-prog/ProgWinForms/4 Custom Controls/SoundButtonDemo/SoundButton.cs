//--------------------------------------------
// SoundButton.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

class SoundButton : Button
{
    SoundPlayer sndplay = new SoundPlayer();

    public string WaveFile
    {
        set
        {
            sndplay.SoundLocation = value;
            sndplay.LoadAsync();
        }
        get
        {
            return sndplay.SoundLocation;
        }
    }
    public Stream WaveStream
    {
        set 
        { 
            sndplay.Stream = value;
            sndplay.LoadAsync();
        }
        get 
        { 
            return sndplay.Stream; 
        }
    }
    protected override void OnClick(EventArgs args)
    {
        if (sndplay.IsLoadCompleted)
            sndplay.Play();

        base.OnClick(args);
    }
}
