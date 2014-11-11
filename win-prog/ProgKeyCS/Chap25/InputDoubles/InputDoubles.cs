// ---------------------------------------------------
// InputDoubles.cs from "Programming in the Key of C#"
// ---------------------------------------------------
using System;

class InputDoubles
{
    static void Main()
    {
        double dBase = GetDouble("Enter the base: ");
        double dExp = GetDouble("Enter the exponent: ");
        Console.WriteLine("{0} to the power of {1} is {2}",
                    dBase, dExp, Math.Pow(dBase, dExp));
    }
    static double GetDouble(string strPrompt)
    {
        double dValue = Double.NaN;

        do
        {
            Console.Write(strPrompt);

            try
            {
                dValue = Double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("You typed an invalid number!");
                Console.WriteLine("Please try again.");
                Console.WriteLine();
            }
        }
        while (Double.IsNaN(dValue));

        return dValue;
    }
}
            
