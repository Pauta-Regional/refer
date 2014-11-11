// -------------------------------------------------
// NoteStream.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class NoteStream
{
    // Public fields

    public int iChannel;
    public int iInstrument;
    public int iVolume;
    public bool bMute;
    public int iCountdown;
    public string strNotePlaying;
    public bool bStopped;

    // Private fields

    string strNotes;
    int iIndex;
    int iLastDuration;

    // Constructor

    public NoteStream(int iChannel, string strNotes, int iInstrument, 
                      int iVolume, bool bMute)
    {
        this.iChannel = iChannel;
        this.strNotes = strNotes;
        this.iInstrument = iInstrument;
        this.iVolume = iVolume;
        this.bMute = bMute;

        Reset();
    }

    // Methods

    public void Reset()
    {
        iIndex = 0;
        bStopped = false;
        strNotePlaying = null;
    }
    public string GetNextNote()
    {
        int iNoteIndex = SkipWhiteSpace(strNotes, iIndex);

        if (strNotes[iNoteIndex] == '|')
            return null;

        iIndex = SkipNonWhiteSpace(strNotes, iNoteIndex);

        return strNotes.Substring(iNoteIndex, iIndex - iNoteIndex);
    }
    public int GetNextDuration()
    {
        int iDurationIndex = SkipWhiteSpace(strNotes, iIndex);

        if (!Char.IsDigit(strNotes, iDurationIndex))
            return iLastDuration;

        iIndex = SkipNonWhiteSpace(strNotes, iDurationIndex);

        int iDuration = Int32.Parse(
            strNotes.Substring(iDurationIndex, iIndex - iDurationIndex));

        iLastDuration = iDuration;
        return iDuration;
    }
    static int SkipWhiteSpace(string str, int i)
    {
        while (i < str.Length && Char.IsWhiteSpace(str, i))
            i++;

        return i;
    }
    static int SkipNonWhiteSpace(string str, int i)
    {
        while (i < str.Length && !Char.IsWhiteSpace(str, i))
            i++;

        return i;
    }
}
