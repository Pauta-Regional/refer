// ---------------------------------------------------
// PropertyTest.cs from "Programming in the Key of C#"
// ---------------------------------------------------
using System;

class PropertyTest
{
    static void Main()
    {
        Date dateMoonWalk = new Date();
        
        dateMoonWalk.Year = 1969;
        dateMoonWalk.Month = 7;
        dateMoonWalk.Day = 20;

        Console.WriteLine("Moon walk: {0}, Day of Year: {1}", 
            dateMoonWalk, dateMoonWalk.DayOfYear());
    }
}
