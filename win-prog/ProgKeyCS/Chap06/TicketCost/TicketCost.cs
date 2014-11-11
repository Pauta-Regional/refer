using System;

class TicketCost
{
    static void Main()
    {
        int ChildTkts = 3;
        int AdultTkts = 2;
        int ChildPrice = 7;
        int AdultPrice = 10;

        int TotalCost = ChildTkts * ChildPrice + AdultTkts * AdultPrice;

        Console.WriteLine(TotalCost);
    }
}
