// ------------------------------------------------------------------
// CommissionCalcWithIfAndElse.cs from "Programming in the Key of C#"
// ------------------------------------------------------------------
using System;

class CommissionCalcWithIfAndElse
{
    static void Main()
    {
        decimal mSales, mCommission, mBonus;

        Console.Write("Enter sales: ");
        mSales = Decimal.Parse(Console.ReadLine());
        mCommission = 0.125m * mSales;

        if (mSales > 10000)
        {
            mBonus = 1000;
        }
        else
        {
            mBonus = 0;
        }
        mCommission += mBonus;

        Console.WriteLine("The commission is " + mCommission);
    }
}
