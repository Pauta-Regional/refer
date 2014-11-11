// ---------------------------------------------------
// OperatorTest.cs from "Programming in the Key of C#"
// ---------------------------------------------------
using System;

class OperatorTest
{
    static void Main()
    {
        Console.Write("Enter the year of your birth: ");
        int iYear = Int32.Parse(Console.ReadLine());

        Console.Write("And the month: ");
        int iMonth = Int32.Parse(Console.ReadLine());

        Console.Write("And the day: ");
        int iDay = Int32.Parse(Console.ReadLine());

        SuperDate sdBirthday = new SuperDate(iYear, iMonth, iDay);
        SuperDate sdMoonWalk = new SuperDate(1969, 7, 20);

        if (sdBirthday > sdMoonWalk)
            Console.WriteLine("You were born {0} days after the moon walk.",
                              sdBirthday - sdMoonWalk);

        else if (sdBirthday == sdMoonWalk)
            Console.WriteLine("You were born on the day of the moon walk.");

        else
            Console.WriteLine("You were born {0} days before the moon walk.",
                              sdMoonWalk - sdBirthday);
    }
}

