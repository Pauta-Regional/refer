// ---------------------------------------------------------
// DaysAndNameOfMonth.cs from "Programming in the Key of C#"
// ---------------------------------------------------------
using System;

class DaysAndNameOfMonth
{
    static void Main()
    {
        string[] astrMonthName = { "January", "February", "March",
                                   "April", "May", "June", "July",
                                   "August", "September", "October",
                                   "November", "December" };

        int[] aiDaysInMonth = { 31, 28, 31, 30, 31, 30, 
                                31, 31, 30, 31, 30, 31 };

        Console.Write("Enter the month (1 for January... 12 for December): ");
        int iMonth = Int32.Parse(Console.ReadLine());
        Console.WriteLine("{0} has {1} days.", astrMonthName[iMonth - 1],
                                               aiDaysInMonth[iMonth - 1]);
    }
}
