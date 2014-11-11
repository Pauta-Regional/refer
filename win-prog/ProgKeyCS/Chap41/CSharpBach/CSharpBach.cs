// -------------------------------------------------
// CSharpBach.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;
using System.IO;    // For File.Exists method
using System.Threading;

class CSharpBach
{
    static void Main()
    {
        string strPrelude = CheckIfFileExists("BachCSharpPrelude.txt");
        string strFugue = CheckIfFileExists("BachCSharpFugue.txt");

        if (strPrelude == null || strFugue == null)
        {
            Console.WriteLine("Cannot find music files.");
            return;
        }

        int iInstrument = GetInteger("Enter instrument 0 through 127\r\n" +
            "\t(0 = Grand Piano, 6 = Harpsichord, 20 = Reed Organ)\r\n" +
            "\t\tor press Enter for piano: ", 0, 127, 0);

        int iVolume = GetInteger("Enter volume 1 through 127" +
            " or press Enter for default of 127: ", 1, 127, 127);

        int iPreludeTempo = GetInteger("Enter tempo for prelude" +
            " (70 through 280 quarter notes per minute)\r\n" +
            "\tor press Enter for default of 140: ", 70, 280, 140); 

        int iFugueTempo = GetInteger("Enter tempo for fugue" +
            " (55 through 220 quarter notes per minute)\r\n" +
            "\tor press Enter for default of 110: ", 55, 220, 110);
        
        using (Midi midi = new Midi())
        {
            MidiPlayer mp = new MidiPlayer(midi, strPrelude, 
                                           iInstrument, iVolume);

            ManualResetEvent[] amre = mp.Play(iPreludeTempo);
            Console.Write("Playing the Prelude... ");
            ManualResetEvent.WaitAll(amre);
            mp.Stop();
            Console.WriteLine("");
            foreach (ManualResetEvent mre in amre)
                mre.Close();

            mp = new MidiPlayer(midi, strFugue, iInstrument, iVolume);

            amre = mp.Play(iFugueTempo);
            Console.Write("Playing the Fugue... ");
            ManualResetEvent.WaitAll(amre);
            mp.Stop();
            Console.WriteLine("");
            foreach (ManualResetEvent mre in amre)
                mre.Close();
        }
    }
    static string CheckIfFileExists(string strFilename)
    {
        if (File.Exists(strFilename))
            return strFilename;

        strFilename = "..\\..\\" + strFilename;

        if (File.Exists(strFilename))
            return strFilename;

        return null;
    }
    static int GetInteger(string strPrompt, int iMin, int iMax, int iDef)
    {
        Console.Write(strPrompt);
        int iReturn;

        try
        {
            iReturn = Int32.Parse(Console.ReadLine());

            if (iReturn < iMin || iReturn > iMax)
                iReturn = iDef;
        }
        catch
        {
            iReturn = iDef;
        }
        return iReturn;
    }
}