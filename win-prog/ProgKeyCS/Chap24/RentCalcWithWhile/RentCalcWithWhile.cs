// --------------------------------------------------------
// RentCalcWithWhile.cs from "Programming in the Key of C#"
// --------------------------------------------------------
using System;

class RentCalcWithWhile
{
    static void Main()
    {
        int iYear = 2004, iFinalYear = 2014;
        decimal mRent = 1200, mIncrease = 5.0m;

        Console.WriteLine("Year          Rent");
        Console.WriteLine("----          ----");

        while (iYear <= iFinalYear)
        {
            Console.WriteLine("{0,-8}{1,10:C2}", iYear, mRent);
            mRent *= (1 + mIncrease / 100);
            mRent = Decimal.Round(mRent, 2);
            iYear++;
        }
    }
}
