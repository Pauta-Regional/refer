// ----------------------------------------------------
// AddingMachine.cs from "Programming in the Key of C#" 
// ----------------------------------------------------
using System;

class AddingMachine
{
    static void Main()
    {
        string strAnswer;

        do    
        {
            Console.Write("Enter first number: ");
            double d1 = Double.Parse(Console.ReadLine());
            Console.Write("Enter second number: ");
            double d2 = Double.Parse(Console.ReadLine());
            Console.WriteLine("The sum is {0}", d1 + d2);

            Console.Write("Do you want to do another? (y/n) ");
            strAnswer = Console.ReadLine().Trim();
        }
        while (strAnswer.Length > 0 && strAnswer.ToLower()[0] == 'y');
    }
}
