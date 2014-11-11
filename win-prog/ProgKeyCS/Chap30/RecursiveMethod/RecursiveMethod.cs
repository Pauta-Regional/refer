// ------------------------------------------------------
// RecursiveMethod.cs from "Programming in the Key of C#"
// ------------------------------------------------------
using System;

class RecursiveMethod
{
    static void Main()
    {
        Console.Write("Enter a long integer: ");
        long lInput = Int64.Parse(Console.ReadLine());
        
        Console.WriteLine("{0} factorial is {1}",
                          lInput, Factorial(lInput));
    }
    static long Factorial(long lNum)
    {
        if (lNum < 0)
            throw new ArgumentOutOfRangeException();

        else if (lNum < 2)
            return 1;

        else
            return checked(lNum * Factorial(lNum - 1));
    }
}