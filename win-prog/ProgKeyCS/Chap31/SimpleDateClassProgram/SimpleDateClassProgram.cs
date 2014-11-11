// -------------------------------------------------------------
// SimpleDateClassProgram.cs from "Programming in the Key of C#"
// -------------------------------------------------------------
using System;

class SimpleDateClassProgram
{
    static void Main()
    {
        Date dateMoonWalk = new Date();

        dateMoonWalk.iYear = 1969;
        dateMoonWalk.iMonth = 7;
        dateMoonWalk.iDay = 20;

        Console.WriteLine("Moon walk: {0}/{1}/{2}", 
            dateMoonWalk.iMonth, dateMoonWalk.iDay, dateMoonWalk.iYear);
    }
}

class Date
{
    public int iYear;
    public int iMonth;
    public int iDay;
}
