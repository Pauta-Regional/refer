// ----------------------------------------------------------
// NoteStreamArrayList.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System.Collections;

class NoteStreamArrayList: ArrayList
{
    public void Add(int iChannel, string strNotes, int iInstrument, 
                    int iVolume, bool bMute)
    {
        Add(new NoteStream(iChannel, strNotes, iInstrument, iVolume, bMute));
    }
    public new NoteStream this[int i]
    {
        get
        {
            return (NoteStream) base[i];
        }
    }
}