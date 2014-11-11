// ----------------------------------------------------
// DateStructure.cs from "Programming in the Key of C#"
// ----------------------------------------------------
using System;

struct DateStructure
{
    // Private fields

    int iYear;
    int iMonth;
    int iDay;

    // Public properties

    public int Year
    {
        set
        {
            if (!IsConsistent(value - 1, Month, Day))
                throw new ArgumentOutOfRangeException("Year");

            iYear = value - 1;
        }
        get
        {
            return iYear + 1; 
        }
    }

    public int Month
    {
        set
        {
            if (!IsConsistent(Year, value - 1, Day))
                throw new ArgumentOutOfRangeException("Month");

            iMonth = value - 1;
        }
        get
        {
            return iMonth + 1; 
        }
    }

    public int Day
    {
        set
        {
            if (!IsConsistent(Year, Month, value - 1))
                throw new ArgumentOutOfRangeException("Day");

            iDay = value - 1;
        }
        get
        {
            return iDay + 1; 
        }
    }

    // Private method used by the properties

    static bool IsConsistent(int iYear, int iMonth, int iDay)
    {
        if (iYear < 1)
            return false;
        
        if (iMonth < 1 || iMonth > 12)
            return false;

        if (iDay < 1 || iDay > 31)
            return false;

        if (iDay == 31 && (iMonth == 4 || iMonth == 6 || 
                           iMonth == 9 || iMonth == 11))
            return false;

        if (iMonth == 2 && iDay > 29)
            return false;

        if (iMonth == 2 && iDay == 29 && !IsLeapYear(iYear))
            return false;

        return true;
    }

    // Parametered constructor

    public DateStructure(int iYear, int iMonth, int iDay)
    {
        this.iYear = 0;
        this.iMonth = 0;
        this.iDay = 0;

        Year = iYear;
        Month = iMonth;
        Day = iDay;
    }

    public static bool IsLeapYear(int iYear)
    {
        return iYear % 4 == 0 && (iYear % 100 != 0 || iYear % 400 == 0);
    }

    static int[] aiCumulativeDays = { 0, 31, 59, 90, 120, 151,
                                      181, 212, 243, 273, 304, 334 };

    public int DayOfYear()
    {
        return aiCumulativeDays[Month - 1] + Day +
            (Month > 2 && IsLeapYear(Year) ? 1 : 0);
    }

    static string[] astrMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                                   "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

    public override string ToString()
    {
        return String.Format("{0} {1} {2}", Day, 
            astrMonths[Month - 1], Year);
    }
}
