// ------------------------------------------------------------
// ReadConsoleAndDisplay.cs from "Programming in the Key of C#"
// ------------------------------------------------------------
using System;

class ReadConsoleAndDisplay
{
    static void Main()
    {
        Console.Write("Type something and press Enter: ");
        string Input = Console.ReadLine();
        Console.WriteLine("Here's what you typed: " + Input);
    }
}
