// ----------------------------------------------------
// ForAndForEach.cs from "Programming in the Key of C#"
// ----------------------------------------------------
using System;

class ForAndForEach
{
    static void Main()
    {
        int iNamesWithZees = 0;
        char[] achSearch = {'z', 'Z'};

        Console.WriteLine("Looping through MyFriends using a for loop:");

        for (int i = 0; i < MyFriends().Length; i++)
            if (MyFriends()[i].IndexOfAny(achSearch) != -1)
                iNamesWithZees++;

        Console.WriteLine();
        Console.WriteLine("Names with Z's: " + iNamesWithZees);

        iNamesWithZees = 0;

        Console.WriteLine("Looping through MyFriends using foreach:");

        foreach (string str in MyFriends())
            if (str.IndexOfAny(achSearch) != -1)
                iNamesWithZees++;

        Console.WriteLine();
        Console.WriteLine("Names with Z's: " + iNamesWithZees);
    }
    static string[] MyFriends()
    {
        Console.Write("*");

        return new string[] 
        { 
            "Hazem Abolrous", "Wanida Benshoof", "Suzana De Canuto",
            "Terry Clayton", "Brenda Diaz", "Terri Lee Duffy",
            "Maciej Dusza", "Charles Fitzgerald", "Guy Gilbert",
            "Jossef Goldberg", "Greg Guzik", "Annette Hill",
            "George Jiang", "Tengiz Kharatishvili", 
            "Rebecca Laszlo", "Yan Li", "Jose Lugo", 
            "Sandra I. Martinez", "Ben Miller", "Zheng Mu",
            "Merav Netz", "Deborah Poe", "Amy Rusko",
            "Vadim Sazanovich", "David So", "Rachel B. Valdez",
            "Raja D. Venugopal", "Paul West", "Robert Zare",
            "Kimberly B. Zimmerman", "Karen Zimprich"
        };
    }
}
