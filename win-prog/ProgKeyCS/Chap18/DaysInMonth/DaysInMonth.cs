// --------------------------------------------------
// DaysInMonth.cs from "Programming in the Key of C#"
// --------------------------------------------------
using System;

class DaysInMonth
{
    static void Main()
    {
        int[] aiDaysInMonth = { 31, 28, 31, 30, 31, 30, 
                                31, 31, 30, 31, 30, 31 };

        Console.Write("Enter the month (1 for January... 12 for December): ");
        int iMonth = Int32.Parse(Console.ReadLine());
        Console.WriteLine("That month has {0} days.", aiDaysInMonth[iMonth - 1]);
    }
}
