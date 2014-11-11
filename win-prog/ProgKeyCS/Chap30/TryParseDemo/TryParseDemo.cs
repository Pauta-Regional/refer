// ---------------------------------------------------
// TryParseDemo.cs from "Programming in the Key of C#"
// ---------------------------------------------------
using System;
using System.Globalization;

class TryParseDemo
{
    static void Main()
    {
        double dResult;

        Console.Write("Enter a double: ");
        string strInput = Console.ReadLine();
        bool bSuccess = Double.TryParse(strInput, NumberStyles.Any, 
                                        null, out dResult);

        if (bSuccess)
            Console.WriteLine("You typed " + dResult);
        else
            Console.WriteLine("The string was an invalid double.");
    }
}