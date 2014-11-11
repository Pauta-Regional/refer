// -------------------------------------------------
// Calculator.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class Calculator
{
    static void Main()
    {
        double dNum1, dNum2, dResult = 0;
        bool bGotOperation;

        Console.Write("Enter first number: ");
        dNum1 = Double.Parse(Console.ReadLine());

        Console.Write("Enter second number: ");
        dNum2 = Double.Parse(Console.ReadLine());

        do
        {
            Console.Write("Enter the operation (+, -, *, /, %): ");
            string strOp = Console.ReadLine().Trim();
            bGotOperation = true;

            switch (strOp)
            {
            case "+":
                dResult = dNum1 + dNum2;
                break;

            case "-":
                dResult = dNum1 - dNum2;
                break;

            case "*":
                dResult = dNum1 * dNum2;
                break;

            case "/":
                dResult = dNum1 / dNum2;
                break;

            case "%":
                dResult = dNum1 % dNum2;
                break;

            default:
                Console.WriteLine("Operation {0} is not valid", strOp);
                bGotOperation = false;
                break;
            }
        }       
        while (!bGotOperation);

        Console.WriteLine("The result is " + dResult);
    }
}