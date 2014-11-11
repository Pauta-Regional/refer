// --------------------------------------------------------------
// CommissionCalcWithBonus.cs from "Programming in the Key of C#"
// --------------------------------------------------------------
using System;

class CommissionCalcWithBonus
{
    static void Main()
    {
        decimal mSales, mCommission, mBonus;

        Console.Write("Enter sales: ");
        mSales = Decimal.Parse(Console.ReadLine());
        mCommission = 0.125m * mSales;
        mBonus = 1000 * Convert.ToInt32(mSales > 10000);
        mCommission += mBonus;
          
        Console.WriteLine("The commission is " + mCommission);
    }
}
