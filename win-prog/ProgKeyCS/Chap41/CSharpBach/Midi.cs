// -------------------------------------------
// Midi.cs from "Programming in the Key of C#"
// -------------------------------------------
using System;
using System.Runtime.InteropServices;

class Midi: IDisposable
{
    [DllImport("winmm.dll")]
    static extern int midiOutOpen(out int hMidiOut, int iDevice,
                         int uiCallback, int uiInstance, int uiFlags);

    [DllImport("winmm.dll")]
    static extern int midiOutClose(int hMidiOut);

    [DllImport("winmm.dll")]
    static extern int midiOutShortMsg(int hMidiOut, int iMsg);

    int hMidiOut;

    public Midi()
    {
        // Most midiOutOpen arguments are not used here.
        int iResult = midiOutOpen(out hMidiOut, -1, 0, 0, 0);

        if (iResult != 0)
            throw new Exception("midiOutOpen error number " + iResult);
    }
    ~Midi()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected void Dispose(bool bDisposing)
    {
        if (hMidiOut != 0)
        {
            midiOutClose(hMidiOut);
            hMidiOut = 0;
        }
    }
    void Close()
    {
        Dispose();
    }
    public void Message(int iStatus, int iData1, int iData2)
    {
        // Combine parameters into a single message.
        int iMsg = iStatus | iData1 << 8 | iData2 << 16;
        int uiResult = midiOutShortMsg(hMidiOut, iMsg);
    }
    public void Instrument(int iChannel, int iInstrument)
    {
        Message(0xC0 | iChannel, iInstrument, 0);
    }
    public void NoteOn(int iChannel, int iNote, int iVelocity)
    {
        Message(0x90 | iChannel, iNote, iVelocity);
    }
    public void NoteOff(int iChannel, int iNote)
    {
        NoteOn(iChannel, iNote, 0);
    }
    public void NoteOn(int iChannel, string strNote, int iVelocity)
    {
        if (strNote.ToUpper()[0] == 'R')    // Rest
            return;

        int iNote = "C D EF G A B".IndexOf(strNote.ToUpper()[0]);
        int i = 2;     // assumed index of octave number in string
        
        if (strNote[1] == '#')
            iNote++;

        else if (strNote[1] == 'b')
            iNote--;

        else
            i = 1;    // index of octave number in string 

        iNote += 12 * Int32.Parse(strNote.Substring(i));
        NoteOn(iChannel, iNote, iVelocity);
    }
    public void NoteOff(int iChannel, string strNote)
    {
        NoteOn(iChannel, strNote, 0);
    }
}