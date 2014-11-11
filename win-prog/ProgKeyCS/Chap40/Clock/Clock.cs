// --------------------------------------------
// Clock.cs from "Programming in the Key of C#"
// --------------------------------------------
using System;
using System.Timers;    // Requires System.dll

class Clock
{
    static int iStringLength;

    static void Main()
    {
        Console.WriteLine("Press Enter to end program");
        Console.WriteLine();

        Timer tmr = new Timer();
        tmr.Elapsed += new ElapsedEventHandler(TimerHandler);
        tmr.Interval = 1000;
        tmr.Start();
        
        Console.ReadLine();
        tmr.Stop();
    }
    static void TimerHandler(object obj, ElapsedEventArgs eea)
    {
        Console.Write(new String('\b', iStringLength));

        string str = String.Format("{0} {1} ", 
                                eea.SignalTime.ToLongDateString(), 
                                eea.SignalTime.ToLongTimeString());
        iStringLength = str.Length;

        Console.Write(str);
    }
}