// --------------------------------------------
// Trig2.cs from "Programming in the Key of C#"
// --------------------------------------------
using System;

class Trig2
{
    public const double PI = Math.PI;
    public const double Pi = PI;
    const double dFactor = PI / 180;

    static void Main()
    {
        Console.WriteLine("The Sin of 45 degrees is " + Trig2.Sin(45));
        Console.WriteLine("The Cos of 45 degrees is " + Trig2.Cos(45));
        Console.WriteLine("The Tan of 45 degrees is " + Trig2.Tan(45));
    }
    public static double Sin(double dAngle)
    {
        return Math.Sin(dFactor * dAngle);
    }
    public static double Cos(double dAngle)
    {
        return Math.Cos(dFactor * dAngle);
    }
    public static double Tan(double dAngle)
    {
        return Math.Tan(dFactor * dAngle);
    }
}