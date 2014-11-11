// -----------------------------------------------------
// AnotherProgram.cs from "Programming in the Key of C#"
// -----------------------------------------------------
using System;

class AnotherProgram
{
    static void Main()
    {
        Console.Write("Enter first side: ");
        double dSide1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter second side: ");
        double dSide2 = Convert.ToDouble(Console.ReadLine());

        double dResult = HandyCalcs.Hypotenuse(dSide1, dSide2);
        Console.WriteLine("The hypotenuse is " + dResult);
    }
}
