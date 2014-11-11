// -------------------------------------------------
// AnimalNode.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;
using System.IO;
using System.Text;

class AnimalNode
{
    private string strQuestion;
    public AnimalNode anYes;
    public AnimalNode anNo;

    public string Question
    {
        set 
        {
            StringBuilder sb = new StringBuilder(value);
            sb.Replace(',', ' ');
            sb.Replace('(', ' ');
            sb.Replace(')', ' ');
            strQuestion = sb.ToString();
        }    
        get { return strQuestion; }
    }
    public void WriteNode(StreamWriter sw, int iLevel)
    {
        sw.Write("{0}({1},", new string(' ', iLevel * 4), Question);

        if (anYes != null)
        {
            sw.WriteLine();
            anYes.WriteNode(sw, iLevel + 1);
        }
        sw.Write(",");

        if (anNo != null)
        {
            sw.WriteLine();
            anNo.WriteNode(sw, iLevel + 1);
        }
        sw.Write(")");
    }
    public static AnimalNode ReadNode(StreamReader sr)
    {
        char ch;

        do
            ch = (char) sr.Read();
        while (Char.IsWhiteSpace(ch));

        AnimalNode an = new AnimalNode();

        while ((ch = (char) sr.Read()) != ',')
            an.Question += ch;

        if ((char) sr.Peek() != ',')
            an.anYes = ReadNode(sr);

        ch = (char) sr.Read();

        if ((char) sr.Peek() != ')')
            an.anNo = ReadNode(sr);
        
        ch = (char) sr.Read();    // This is the closing parenthesis.

        return an;
    }
}

