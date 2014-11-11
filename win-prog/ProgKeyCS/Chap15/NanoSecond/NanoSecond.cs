// -------------------------------------------------
// NanoSecond.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class NanoSecond
{
    static void Main()
    {
        const double CinMilesPerSecond = 186E3;
        const double FeetPerMile = 5280;
        const double InchesPerFeet = 12;
        const double SecondsPerNanoSecond = 1E-9;

        double InchesPerNanoSecond = 
            CinMilesPerSecond * FeetPerMile * 
            InchesPerFeet * SecondsPerNanoSecond;

        Console.WriteLine("In one nanosecond light travels " +
            InchesPerNanoSecond + " inches.");    
    }
}
