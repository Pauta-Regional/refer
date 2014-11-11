// ----------------------------------------------------------
// FirstCommissionCalc.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System;

class FirstCommissionCalc
{
    static void Main()
    {
        decimal mSales, mCommission;

        Console.Write("Enter sales: ");
        mSales = Decimal.Parse(Console.ReadLine());
        mCommission = 0.125m * mSales;
        Console.WriteLine("The commission is " + mCommission);
    }
}
