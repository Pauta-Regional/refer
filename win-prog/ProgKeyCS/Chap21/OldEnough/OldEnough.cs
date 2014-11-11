// ------------------------------------------------
// OldEnough.cs from "Programming in the Key of C#"
// ------------------------------------------------
using System;

class OldEnough
{
    static void Main()
    {
        Console.Write("Enter your age: ");
        int iAge = Int32.Parse(Console.ReadLine());
        bool bOldEnoughToDrive = iAge >= 16;
        Console.WriteLine("It is " + bOldEnoughToDrive +
            " that you are old enough to drive.");
    }
}
