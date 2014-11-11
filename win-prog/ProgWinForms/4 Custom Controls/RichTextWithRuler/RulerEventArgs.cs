//-----------------------------------------------
// RulerEventArgs.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;

public class RulerEventArgs : EventArgs
{
    RulerProperty rlrprop;

    public RulerEventArgs(RulerProperty rlrprop)
    {
        this.rlrprop = rlrprop;
    }
    public RulerProperty RulerChange
    {
        get { return rlrprop; }
        set { rlrprop = value; }
    }
}

