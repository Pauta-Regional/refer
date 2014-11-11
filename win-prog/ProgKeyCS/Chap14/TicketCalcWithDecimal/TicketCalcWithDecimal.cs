// ------------------------------------------------------------
// TicketCalcWithDecimal.cs from "Programming in the Key of C#"
// ------------------------------------------------------------
using System;

class TicketCalcWithDecimal
{
    static void Main()
    {
        const decimal AdultPrice = 12.50m, ChildPrice = 8.25m;

        Console.Write("Enter number of adult tickets: ");
        int AdultTkts = Int32.Parse(Console.ReadLine());

        Console.Write("Enter number of child tickets: ");
        int ChildTkts = Int32.Parse(Console.ReadLine());

        decimal TotalCost = AdultTkts * AdultPrice + ChildTkts * ChildPrice;
        Console.WriteLine("The total cost is $" + TotalCost);
    }
}
