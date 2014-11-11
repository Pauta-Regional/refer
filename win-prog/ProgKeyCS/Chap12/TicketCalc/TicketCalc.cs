// -------------------------------------------------
// TicketCalc.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class TicketCalc
{
    static void Main()
    {
        int AdultPrice = 10, ChildPrice = 7;

        Console.Write("Enter number of adult tickets: ");
        int AdultTkts = Int32.Parse(Console.ReadLine());

        Console.Write("Enter number of child tickets: ");
        int ChildTkts = Int32.Parse(Console.ReadLine());

        int TotalCost = AdultTkts * AdultPrice + ChildTkts * ChildPrice;
        Console.WriteLine("The total cost is $" + TotalCost);
    }
}
