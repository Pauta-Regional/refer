// --------------------------------------------------------------------
// CalculationWithSeparateMethod.cs from "Programming in the Key of C#"
// --------------------------------------------------------------------
using System;

class CalculationWithSeparateMethod
{
    static void Main()
    {
        Console.Write("Enter first side: ");
        double dSide1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter second side: ");
        double dSide2 = Convert.ToDouble(Console.ReadLine());

        double dResult = Hypotenuse(dSide1, dSide2);
        Console.WriteLine("The hypotenuse is " + dResult);
    }
    static double Hypotenuse(double d1, double d2)
    {
        double dResult = Math.Sqrt(d1 * d1 + d2 * d2);

        return dResult;
    }
}