// ----------------------------------------------------
// BitOperations.cs from "Programming in the Key of C#"
// ----------------------------------------------------
using System;

class BitOperations
{
    static void Main()
    {
        int i1 = 0x1357;    // Bits: 0001-0011-0101-0111
        int i2 = 0x2468;    // Bits: 0010-0100-0110-1000
 
        Console.WriteLine("And: {0,8:X}\nOr:  {1,8:X}\nXor: {2,8:X}",
                          i1 & i2, i1 | i2, i1 ^ i2);
    }
}
