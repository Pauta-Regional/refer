// ------------------------------------------------------------
// StructureWithToString.cs from "Programming in the Key of C#"
// ------------------------------------------------------------
using System;

class StructureWithToString
{
    static void Main()
    {
        Date dateMoonWalk = new Date();

        dateMoonWalk.iYear = 1969;
        dateMoonWalk.iMonth = 7;
        dateMoonWalk.iDay = 20;

        Console.WriteLine("Moon walk: {0}, Day of Year: {1}", 
            dateMoonWalk, dateMoonWalk.DayOfYear());
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

    static string[] astrMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                                   "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

    public override string ToString()
    {
        return String.Format("{0} {1} {2}", iDay, 
            astrMonths[iMonth - 1], iYear);
    }
}
