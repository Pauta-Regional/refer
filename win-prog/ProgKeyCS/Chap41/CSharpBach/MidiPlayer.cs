// -------------------------------------------------
// MidiPlayer.cs from "Programming in the Key of C#" 
// -------------------------------------------------
using System;
using System.IO;
using System.Threading;
using System.Timers;    // Requires System.dll

class MidiPlayer
{
    Midi midi;
    NoteStreamArrayList nsal = new NoteStreamArrayList();
    System.Timers.Timer tmr = new System.Timers.Timer();
    ManualResetEvent[] amre;

    // Constructors 

    public MidiPlayer(Midi midi)
    {
        this.midi = midi;
    }
    public MidiPlayer(Midi midi, string strFileName, int iInstrument, 
                      int iVolume): this(midi)
    {
        StreamReader sr = new StreamReader(strFileName);
        string strLine, str = "";

        while (null != (strLine = sr.ReadLine()))
        {
            if (strLine.Length == 0 || strLine[0] == '/')
                continue;

            str += strLine + " ";
        }
        sr.Close();

        int iStart = 0;
        int iEnd = str.IndexOf('|', iStart);

        for (int i = 0; iEnd != -1; i++)
        {
            Add(i, str.Substring(iStart, iEnd - iStart + 1), iInstrument, 
                                 iVolume, false);
            iStart = iEnd + 1;
            iEnd = str.IndexOf('|', iStart);
        }
    }

    // Add method adds NoteStream objects to the composition.

    public void Add(int iChannel, string strNoteStream, int iInstrument, 
                    int iVolume, bool bMute)
    {
        if (!tmr.Enabled)
            nsal.Add(iChannel, strNoteStream, iInstrument, iVolume, bMute);
    }

    // Play method starts the composition playing.

    public ManualResetEvent[] Play(int iTempo)
    {
        // Initialize semaphores.
        amre = new ManualResetEvent[nsal.Count];

        for (int i = 0; i < amre.Length; i++)
            amre[i] = new ManualResetEvent(false);

        // Set instrument for each channel.
        foreach(NoteStream ns in nsal)
            midi.Instrument(ns.iChannel, ns.iInstrument);

        // Convert tempo to milliseconds per tick.
        tmr.Interval = 60000 / (16 * iTempo);    
        tmr.Elapsed += new ElapsedEventHandler(TimerCallback);
        tmr.Start();

        return amre;
    }

    // Stop method stops the timer.

    public void Stop()
    {
        tmr.Stop();
        tmr.Close();
    }

    // TimerCallback plays the next notes if they're ready to be played.

    void TimerCallback(object obj, ElapsedEventArgs eea)
    {
        lock(this)
        {
            for (int i = 0; i < nsal.Count; i++)
            {
                if (nsal[i].bStopped)
                    continue;

                if (nsal[i].iCountdown == 0)
                {
                    if (nsal[i].strNotePlaying != null)
                        if (!nsal[i].bMute)
                            midi.NoteOff(nsal[i].iChannel, 
                                nsal[i].strNotePlaying);

                    string strNote = nsal[i].GetNextNote();

                    if (strNote == null)
                    {
                        nsal[i].bStopped = true;
                        amre[i].Set();    // Signal the semaphore.
                        continue;
                    }

                    if (!nsal[i].bMute)
                        midi.NoteOn(nsal[i].iChannel, strNote, 
                                    nsal[i].iVolume);

                    nsal[i].strNotePlaying = strNote;
                    nsal[i].iCountdown = nsal[i].GetNextDuration();
                }
                nsal[i].iCountdown--;
            }
        }
    }
}
