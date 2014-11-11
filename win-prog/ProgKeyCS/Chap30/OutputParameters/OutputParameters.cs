// -------------------------------------------------------
// OutputParameters.cs from "Programming in the Key of C#"
// -------------------------------------------------------
using System;

class OutputParameters
{
    static void Main()
    {
        double dSide1 = 50, dSide2 = 60, dSide3 = 100;
        double dAngle1, dAngle2, dAngle3;

        TriangleAngles(dSide1, dSide2, dSide3, 
                       out dAngle1, out dAngle2, out dAngle3);

        Console.WriteLine("{0} + {1} + {2} = {3}", 
                          dAngle1, dAngle2, dAngle3, 
                          dAngle1 + dAngle2 + dAngle3);
    }
    static void TriangleAngles(double A, double B, double C,
        out double alpha, out double beta, out double gamma)
    {
        alpha = Math.Acos((B * B + C * C - A * A) / (2 * B * C));
        beta  = Math.Acos((A * A + C * C - B * B) / (2 * A * C));
        gamma = Math.Acos((A * A + B * B - C * C) / (2 * A * B));
    }
}
