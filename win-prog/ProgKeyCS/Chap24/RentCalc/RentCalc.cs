// -----------------------------------------------
// RentCalc.cs from "Programming in the Key of C#"
// -----------------------------------------------
using System;

class RentCalc
{
    static void Main()
    {
        decimal mRent = 1200, mIncrease = 5.0m;

        Console.WriteLine("My rent in 2004 is {0}", mRent);
        Console.WriteLine("My rent in 2005 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2006 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2007 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2008 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2009 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2010 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2011 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2012 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2013 will be {0}", 
            mRent *= (1 + mIncrease / 100));
        Console.WriteLine("My rent in 2014 will be {0}", 
            mRent *= (1 + mIncrease / 100));
    }
}
