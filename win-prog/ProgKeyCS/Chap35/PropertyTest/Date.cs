// -------------------------------------------
// Date.cs from "Programming in the Key of C#"
// -------------------------------------------
using System;

class Date
{
    // Private fields

    int iYear = 1;
    int iMonth = 1;
    int iDay = 1;

    // Public properties

    public int Year
    {
        set
        {
            if (!IsConsistent(value, Month, Day))
                throw new ArgumentOutOfRangeException("Year");

            iYear = value;
        }
        get
        {
            return iYear; 
        }
    }

    public int Month
    {
        set
        {
            if (!IsConsistent(Year, value, Day))
                throw new ArgumentOutOfRangeException("Month = " + value);

            iMonth = value;
        }
        get
        {
            return iMonth; 
        }
    }

    public int Day
    {
        set
        {
            if (!IsConsistent(Year, Month, value))
                throw new ArgumentOutOfRangeException("Day");

            iDay = value;
        }
        get
        {
            return iDay; 
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

    // Parameterless constructor

    public Date()
    {
    }

    // Parametered constructor

    public Date(int iYear, int iMonth, int iDay)
    {
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
