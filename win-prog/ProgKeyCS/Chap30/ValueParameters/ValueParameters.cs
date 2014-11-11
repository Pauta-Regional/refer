// ------------------------------------------------------
// ValueParameters.cs from "Programming in the Key of C#"
// ------------------------------------------------------
using System;

class ValueParameters
{
    static void Main()
    {
        int i = 0;
        ChangeInteger(i);
        Console.WriteLine(i);
    }
    static void ChangeInteger(int i)
    {
        i = 55;
    }
}
