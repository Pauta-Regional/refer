// ----------------------------------------------------------
// ReferenceParameters.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System;

class ReferenceParameters
{
    static void Main()
    {
        int iMyAge = 50;

        Console.Write("Enter your age: ");
        int iYourAge = Int32.Parse(Console.ReadLine());

        Console.WriteLine("My age: {0} Your age: {1}", iMyAge, iYourAge);
        Swap(ref iMyAge, ref iYourAge);
        Console.WriteLine("My age: {0} Your age: {1}", iMyAge, iYourAge);
    }
    static void Swap(ref int i1, ref int i2)
    {
        int iTemp = i1;
        i1 = i2;
        i2 = iTemp;
    }
}
