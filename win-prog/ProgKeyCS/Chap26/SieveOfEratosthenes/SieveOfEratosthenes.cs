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

        // Initialize the array to true.

        for (int i = 0; i <= iMax; i++)
            abIsPrime[i] = true;

        // Perform the sieve.

        for (int i = 2; i * i <= iMax; i++)
            if (abIsPrime[i])
                for (int j = 2; j <= iMax / i; j++) 
                    abIsPrime[i * j] = false;

        // Display the prime numbers.

        for (int i = 2; i <= iMax; i++)
            if (abIsPrime[i])
                Console.Write("{0} ", i);
    }
}