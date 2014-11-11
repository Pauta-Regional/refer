// -----------------------------------------------
// ZeeNames.cs from "Programming in the Key of C#"
// -----------------------------------------------
using System;

class ZeeNames
{
    static void Main()
    {
        string[] astrMyFriends = 
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
        int iNamesWithZees = 0;
        char[] achSearch = {'z', 'Z'};

        // First, count up the names with 'z' or 'Z' in them.

        for (int i = 0; i < astrMyFriends.Length; i++)
            if (astrMyFriends[i].IndexOfAny(achSearch) != -1)
                iNamesWithZees++;

        // Next, declare an array of that size

        string[] astrMyFriendsWithZees = new string[iNamesWithZees];

        // Transfer the names from one array to another

        for (int i = 0, j = 0; i < astrMyFriends.Length; i++)
            if (astrMyFriends[i].IndexOfAny(achSearch) != -1)
                astrMyFriendsWithZees[j++] = astrMyFriends[i];

        // Display some statistical information

        Console.WriteLine("Of my {0} friends, {1} " +
                          "have a name with 'Z' in it:",
                          astrMyFriends.Length, iNamesWithZees);

        // Display the new array

        for (int i = 0; i < astrMyFriendsWithZees.Length; i++)
            Console.WriteLine(astrMyFriendsWithZees[i]);
    }
}