// --------------------------------------------
// Trig1.cs from "Programming in the Key of C#"
// --------------------------------------------
using System;

class Trig1
{
    static void Main()
    {
        Console.WriteLine("The Sin of 45 degrees is " + Trig1.Sin(45));
        Console.WriteLine("The Cos of 45 degrees is " + Trig1.Cos(45));
        Console.WriteLine("The Tan of 45 degrees is " + Trig1.Tan(45));
    }
    public static double Sin(double dAngle)
    {
        return Math.Sin(Math.PI * dAngle / 180);
    }
    public static double Cos(double dAngle)
    {
        return Math.Cos(Math.PI * dAngle / 180);
    }
    public static double Tan(double dAngle)
    {
        return Math.Tan(Math.PI * dAngle / 180);
    }
}