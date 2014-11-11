// ----------------------------------------------------------
// ConsistencyChecking.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System;

class ConsistencyChecking
{
    static void Main()
    {
        Date dateMoonWalk = new Date();

        Console.WriteLine("Moon walk: {0}, Day of Year: {1}", 
            dateMoonWalk, dateMoonWalk.DayOfYear());
    }
}

class Date
{
    public int iYear;
    public int iMonth;
    public int iDay;

    // Parameterless constructor

    public Date()
    {
        iYear = 1;
        iMonth = 1;
        iDay = 1;
    }

    // Parametered constructor

    public Date(int iYear, int iMonth, int iDay)
    {
        if (iYear < 1)
            throw new ArgumentOutOfRangeException("Year");

        if (iMonth < 1 || iMonth > 12)
            throw new ArgumentOutOfRangeException("Month");

        if (iDay < 1 || iDay > 31)
            throw new ArgumentOutOfRangeException("Day");

        if (iDay == 31 && (iMonth == 4 || iMonth == 6 || 
            iMonth == 9 || iMonth == 11))
            throw new ArgumentOutOfRangeException("Day");

        if (iMonth == 2 && iDay > 29)
            throw new ArgumentOutOfRangeException("Day");

        if (iMonth == 2 && iDay == 29 && !IsLeapYear(iYear))
            throw new ArgumentOutOfRangeException("Day");

        this.iYear = iYear;
        this.iMonth = iMonth;
        this.iDay = iDay;
    }

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
