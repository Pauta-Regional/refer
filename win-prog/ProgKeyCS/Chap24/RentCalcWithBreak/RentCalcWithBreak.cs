// --------------------------------------------------------
// RentCalcWithBreak.cs from "Programming in the Key of C#"
// --------------------------------------------------------
using System;

class RentCalcWithBreak
{
    static void Main()
    {
        int iYear = 2004, iFinalYear = 2014;
        decimal mRent = 1200, mIncrease = 5.0m;

        Console.WriteLine("Year          Rent");
        Console.WriteLine("----          ----");

        while (true)
        {
            Console.WriteLine("{0,-8}{1,10:C2}", iYear, mRent);

            if (iYear == iFinalYear)
                break;

            mRent *= (1 + mIncrease / 100);
            mRent = Decimal.Round(mRent, 2);
            iYear++;
        }
    }
}
