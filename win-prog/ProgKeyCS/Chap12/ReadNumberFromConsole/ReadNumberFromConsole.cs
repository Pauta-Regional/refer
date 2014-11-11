// ------------------------------------------------------------
// ReadNumberFromConsole.cs from "Programming in the Key of C#"
// ------------------------------------------------------------
using System;

class ReadNumberFromConsole
{
    static void Main()
    {
        string S;
        int I;

        Console.Write("Enter a number: ");
        S = Console.ReadLine();
        I = Int32.Parse(S);
        Console.WriteLine("You entered the number " + I);
    }
}
