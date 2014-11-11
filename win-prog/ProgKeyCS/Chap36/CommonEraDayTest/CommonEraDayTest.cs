// -------------------------------------------------------
// CommonEraDayTest.cs from "Programming in the Key of C#"
// -------------------------------------------------------
using System;

class CommonEraDayTest
{
    static void Main()
    {
        Console.Write("Enter the year of your birth: ");
        int iYear = Int32.Parse(Console.ReadLine());

        Console.Write("And the month: ");
        int iMonth = Int32.Parse(Console.ReadLine());

        Console.Write("And the day: ");
        int iDay = Int32.Parse(Console.ReadLine());

        ExtDate exdtBirthday = new ExtDate(iYear, iMonth, iDay);
        ExtDate exdtMoonWalk = new ExtDate(1969, 7, 20);

        int iElapsed = exdtMoonWalk.CommonEraDay - exdtBirthday.CommonEraDay;

        if (iElapsed > 0)
            Console.WriteLine("You were born {0} days before the moon walk.",
                              iElapsed);
        else if (iElapsed == 0)
            Console.WriteLine("You were born on the day of the moon walk.");

        else
            Console.WriteLine("You were born {0} days after the moon walk.",
                              -iElapsed);
    }
}

