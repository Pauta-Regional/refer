using System;

class FirstOutputWithUsing
{
    static void Main()
    {
        int ChildTkts, AdultTkts, TotalTkts;

        ChildTkts = 3;
        AdultTkts = 2; 
        TotalTkts = ChildTkts + AdultTkts;

        Console.WriteLine(TotalTkts);
    }
}
