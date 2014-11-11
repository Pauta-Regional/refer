// ------------------------------------------------------
// TableOfContents.cs from "Programming in the Key of C#"
// ------------------------------------------------------
using System;

class TableOfContents
{
    static string[] astrChapter = { "You the Programmer", 
                "First Assignments", "Declarations of Purpose",
                "Edit, Compile, Run", "Console Output" };
    static int[] aiPageNumber = { 3, 10, 16, 24, 29 };
                
    static void Main()
    {
        DisplayLine(0);
        DisplayLine(1);
        DisplayLine(2);
        DisplayLine(3);
        DisplayLine(4);
    }
    static void DisplayLine(int i)
    {
        const int iWidth = 60;
        string strDots = new string('.', 
            iWidth - (i + 1).ToString().Length 
                   - astrChapter[i].Length 
                   - aiPageNumber[i].ToString().Length);

        Console.WriteLine("{0}. {1}{2}{3}", 
            i + 1, astrChapter[i], strDots, aiPageNumber[i]);
    }
}