// ----------------------------------------------------------
// ArrayValueParameter.cs from "Programming in the Key of C#"
// ----------------------------------------------------------
using System;

class ArrayValueParameter
{
    static void Main()
    {
        int[] aiNumbers = new int[5];
        ChangeArray(aiNumbers);
        Console.WriteLine(aiNumbers[0]);
    }
    static void ChangeArray(int[] ai)
    {
        ai[0] = 55;
        ai = null;
    }
}
