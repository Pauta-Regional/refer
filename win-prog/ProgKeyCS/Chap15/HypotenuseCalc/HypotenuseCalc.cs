// -----------------------------------------------------
// HypotenuseCalc.cs from "Programming in the Key of C#"
// -----------------------------------------------------
using System;

class HypotenuseCalc
{
    static void Main()
    {
        Console.Write("Enter first side: ");
        double dSide1 = Double.Parse(Console.ReadLine());

        Console.Write("Enter second side: ");
        double dSide2 = Double.Parse(Console.ReadLine());

        double dResult = Math.Sqrt(Math.Pow(dSide1, 2) + Math.Pow(dSide2, 2));
        Console.WriteLine("The hypotenuse is {0}", dResult);
    }
}
