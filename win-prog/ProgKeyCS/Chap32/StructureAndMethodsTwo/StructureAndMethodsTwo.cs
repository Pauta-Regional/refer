// -------------------------------------------------------------
// StructureAndMethodsTwo.cs from "Programming in the Key of C#"
// -------------------------------------------------------------
using System;

class StructureAndMethodsTwo
{
    static void Main()
    {
        Date dateMoonWalk = new Date();

        dateMoonWalk.iYear = 1969;
        dateMoonWalk.iMonth = 7;
        dateMoonWalk.iDay = 20;

        Console.WriteLine("Moon walk: {0}/{1}/{2} Day of Year: {3}", 
            dateMoonWalk.iMonth, dateMoonWalk.iDay, dateMoonWalk.iYear,
            Date.DayOfYear(dateMoonWalk));
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

    public static int DayOfYear(Date dateParam)
    {
        return aiCumulativeDays[dateParam.iMonth - 1] + dateParam.iDay +
            (dateParam.iMonth > 2 && IsLeapYear(dateParam.iYear) ? 1 : 0);
    }
}
