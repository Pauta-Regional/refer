// -----------------------------------------------
// DatePack.cs from "Programming in the Key of C#"
// -----------------------------------------------
using System;

class DatePack
{
    static void Main()
    {
        Console.Write("Enter the year: ");
        int iYear = Int32.Parse(Console.ReadLine());

        Console.Write("Enter the month: ");
        int iMonth = Int32.Parse(Console.ReadLine());

        Console.Write("Enter the day: ");
        int iDay = Int32.Parse(Console.ReadLine());

        // Pack the date.

        int iDate = (iYear << 9) | (iMonth << 5) | iDay;

        Console.WriteLine("Packed date: {0:X}", iDate);

        // Unpack the date.

        iYear = iDate >> 9;
        iMonth = (iDate >> 5) & 0xF;
        iDay = iDate & 0x1F;

        Console.WriteLine("Year: {0} Month: {1} Day: {2}",
                          iYear, iMonth, iDay);
    }
}