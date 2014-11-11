//------------------------------------------------
// KnownColorClass.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;

class KnownColorClass
{
    KnownColor kc;

    public KnownColorClass(KnownColor kc)
    {
        this.kc = kc;
    }
    public Color Color
    {
        get { return Color.FromKnownColor(kc); }
    }
    public string Name
    {
        get
        {
            string str = Enum.GetName(typeof(KnownColor), kc);

            for (int i = 1; i < str.Length; i++)
                if (Char.IsUpper(str[i]))
                    str = str.Insert(i++, " ");

            return str;
        }
    }
    public static KnownColorClass[] KnownColorArray
    {
        get
        {
            // Create an array of KnownColorClass objects.
            KnownColor[] akc = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColorClass[] akcc = new KnownColorClass[akc.Length];

            for (int i = 0; i < akc.Length; i++)
                akcc[i] = new KnownColorClass(akc[i]);

            return akcc;
        }
    }
}