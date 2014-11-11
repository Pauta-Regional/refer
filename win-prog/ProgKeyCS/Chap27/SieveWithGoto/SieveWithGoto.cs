// ----------------------------------------------------------
// SieveOfEratosthenes.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System;

class SieveOfEratosthenes
{
    static void Main()
    {
        Console.Write("Enter a maximum integer to check: ");
        int iMax = Int32.Parse(Console.ReadLine());
        bool[] abIsPrime = new bool[iMax + 1];
        int i, j;

        // Initialize the array to true.

        i = 0;
        Initialize:
            abIsPrime[i] = true;
        i++;
        if (i <= iMax) goto Initialize;

        // Perform the sieve.

        i = 2;
        NextBase:
            if (!abIsPrime[i]) goto SkipLoop;
            j = 2;
        NextMultiplier:
            abIsPrime[i * j] = false;
        j++;
        if (j <= iMax / i) goto NextMultiplier;
        SkipLoop:
            i++;
        if (i * i < iMax) goto NextBase;

        // Display the prime numbers.

        i = 2;
        Display:
            if (!abIsPrime[i]) goto SkipDisplay;
        Console.Write("{0} ", i);
        SkipDisplay:
            i++;
        if (i <= iMax) goto Display;
    }
}