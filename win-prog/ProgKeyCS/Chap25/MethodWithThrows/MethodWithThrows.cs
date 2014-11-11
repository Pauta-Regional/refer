// -------------------------------------------------------
// MethodWithThrows.cs from "Programming in the Key of C#"
// -------------------------------------------------------
using System;

class MethodWithThrows
{
    static void Main()
    {
        int iInput;

        Console.Write("Enter an unsigned integer: ");
        
        try
        {
            iInput = MyParse(Console.ReadLine());
            Console.WriteLine("You entered {0}", iInput);
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }
    }
    static int MyParse(string str)
    {
        int iResult = 0, i = 0;
 
        // If argument is null, throw an exception.

        if (str == null)
            throw new ArgumentNullException();

        // Get rid of white space.

        str = str.Trim();

        // Check if there's at least one character.
        
        if (str.Length == 0)
            throw new FormatException(); 

        // Loop through all the characters in the string.

        while (i < str.Length)
        {
            // If the next character's not a digit, throw exception.

            if (!Char.IsDigit(str, i))
                throw new FormatException();

            // Accumulate the next digit (notice "checked").

            iResult = checked(10 * iResult + (int) str[i] - (int) '0');

            i++;
        }
        return iResult;
    }
}