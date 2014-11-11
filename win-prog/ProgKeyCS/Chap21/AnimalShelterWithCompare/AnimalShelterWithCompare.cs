// ---------------------------------------------------------------
// AnimalShelterWithCompare.cs from "Programming in the Key of C#"
// ---------------------------------------------------------------
using System;

class AnimalShelterWithCompare
{
    static void Main()
    {
        bool bMale, bNeutered, bBlack, bWhite, bTan, bAcceptable;

        Console.Write("Is the cat male? Type true or false: ");
        bMale = Convert.ToBoolean(Console.ReadLine());

        Console.Write("Is the cat neutered? Type true or false: ");
        bNeutered = Convert.ToBoolean(Console.ReadLine());

        Console.Write("Enter cat color. Type black, white, tan, etc: ");
        string strColor = Console.ReadLine();

        bBlack = strColor == "black";
        bWhite = strColor == "white";
        bTan = strColor == "tan";

        bAcceptable = (bMale & bNeutered & (bWhite | bTan)) |
            (!bMale & bNeutered & !bWhite) |
            bBlack;

        Console.WriteLine("Acceptable: " + bAcceptable);
    }
}
