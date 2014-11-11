// --------------------------------------------------
// Inheritance.cs from "Programming in the Key of C#"
// --------------------------------------------------
using System;

class Inheritance
{
    static void Main()
    {
        ExtDate dateMoonWalk = new ExtDate(1969, 7, 20);

        Console.WriteLine("Moon walk: {0}, Day of Year: {1}", 
            dateMoonWalk, dateMoonWalk.DayOfYear());
    }
}

class ExtDate: Date
{
    public ExtDate()
    {
    }
    public ExtDate(int iYear, int iMonth, int iDay): base(iYear, iMonth, iDay)
    {
    }
}
