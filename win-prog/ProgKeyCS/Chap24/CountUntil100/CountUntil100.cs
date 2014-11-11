// ----------------------------------------------------
// CountUntil100.cs from "Programming in the Key of C#"
// ----------------------------------------------------
using System;

class CountUntil100
{
    static void Main()
    {
        int i = 0;

        while (i < 100)
        {
            Console.WriteLine(i);
            i++;
        }
        Console.WriteLine("Final value of i equals " + i);
    }
}
