// --------------------------------------------
// Haiku.cs from "Programming in the Key of C#"
// --------------------------------------------
using System;

class Haiku
{
    static void Main()
    {
        string[,] astr = {
            { "scent", "bliss", "strut", "foot", "shade" },
            { "ox", "nerd", "pie", "skull", "bike" },
            { "transcends", "wrestles", "merges", 
                "sidesteps", "exalts" },
            { "poetry", "restlessness", "ruthlessness",
                "itchiness", "comedy" },
            { "life", "death", "pain", "joy", "code" },
            { "graceful", "ugly", "dancing", "hefty", "furry" },
            { "dawn", "end", "blast", "lunch", "crunch" }};

        Random rand = new Random();
        int iMax = astr.GetLength(1);

        Console.WriteLine("The {0} of the {1}",
                          astr[0, rand.Next(iMax)],
                          astr[1, rand.Next(iMax)]);
        Console.WriteLine("{0} {1} of {2}",
                          astr[2, rand.Next(iMax)],
                          astr[3, rand.Next(iMax)],
                          astr[4, rand.Next(iMax)]);
        Console.WriteLine("'til the {0} {1}.",
                          astr[5, rand.Next(iMax)],
                          astr[6, rand.Next(iMax)]);
    }
}
                          


