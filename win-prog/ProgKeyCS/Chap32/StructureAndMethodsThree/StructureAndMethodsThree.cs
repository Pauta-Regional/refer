// ---------------------------------------------------------------
// StructureAndMethodsThree.cs from "Programming in the Key of C#"
// ---------------------------------------------------------------
using System;

class StructureAndMethodsThree
{
    static void Main()
    {
        Date dateMoonWalk = new Date();

        dateMoonWalk.iYear = 1969;
        dateMoonWalk.iMonth = 7;
        dateMoonWalk.iDay = 20;

        Console.WriteLine("Moon walk: {0}/{1}/{2} Day of Year: {3}", 
            dateMoonWalk.iMonth, dateMoonWalk.iDay, dateMoonWalk.iYear,
            dateMoonWalk.DayOfYear());
    }
}

struct Date
{
    public int iYear;
    public int iMonth;
    public int iDay;

    public static bool IsLeapYear(int iYear)
    {
        return iYear % 4 == 0 && (iYear % 100 != 0 || iYear % 400 == 0);
    }

    static int[] aiCumulativeDays = { 0, 31, 59, 90, 120, 151,
                                        181, 212, 243, 273, 304, 334 };

    public int DayOfYear()
    {
        return aiCumulativeDays[iMonth - 1] + iDay +
            (iMonth > 2 && IsLeapYear(iYear) ? 1 : 0);
    }
}