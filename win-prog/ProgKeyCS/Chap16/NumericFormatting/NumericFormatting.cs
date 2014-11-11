// --------------------------------------------------------
// NumericFormatting.cs from "Programming in the Key of C#"
// --------------------------------------------------------
using System;

class NumericFormatting
{
    static void Main()
    {
        decimal m = 12345.345m;

        Console.WriteLine("Currency formatting: " + m.ToString("C2"));
        Console.WriteLine("Exponential formatting: " + m.ToString("E2"));
        Console.WriteLine("Fixed-point formatting: " + m.ToString("F2"));
        Console.WriteLine("General formatting: " + m.ToString("G2"));
        Console.WriteLine("Number formatting: " + m.ToString("N2"));
        Console.WriteLine("Percent formatting: " + m.ToString("P2"));
    }
}