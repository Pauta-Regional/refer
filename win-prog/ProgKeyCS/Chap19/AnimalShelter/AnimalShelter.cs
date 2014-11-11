// ----------------------------------------------------
// AnimalShelter.cs from "Programming in the Key of C#"
// ----------------------------------------------------
using System;

class AnimalShelter
{
    static void Main()
    {
        bool bMale, bNeutered, bBlack, bWhite, bTan, bAcceptable;

        Console.Write("Is the cat male? Type true or false: ");
        bMale = Boolean.Parse(Console.ReadLine());

        Console.Write("Is the cat neutered? Type true or false: ");
        bNeutered = Boolean.Parse(Console.ReadLine());

        Console.Write("Is the cat black? Type true or false: ");
        bBlack = Boolean.Parse(Console.ReadLine());

        Console.Write("Is the cat white? Type true or false: ");
        bWhite = Boolean.Parse(Console.ReadLine());

        Console.Write("Is the cat tan? Type true or false: ");
        bTan = Boolean.Parse(Console.ReadLine());

        bAcceptable = (bMale & bNeutered & (bWhite | bTan)) |
                      (!bMale & bNeutered & !bWhite) |
                      bBlack;

        Console.WriteLine("Acceptable: " + bAcceptable);
    }
}
