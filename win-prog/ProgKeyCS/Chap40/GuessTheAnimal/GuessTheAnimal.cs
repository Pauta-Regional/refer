// -----------------------------------------------------
// GuessTheAnimal.cs from "Programming in the Key of C#"
// -----------------------------------------------------
using System;
using System.IO;

class GuessTheAnimal
{
    static void Main()
    {
        string strFilename = "GuessTheAnimal.txt";
        AnimalNode anRoot;
        
        try
        {
            StreamReader sr = new StreamReader(strFilename);
            anRoot = AnimalNode.ReadNode(sr);
            sr.Close();
        }
        catch
        {
            anRoot = new AnimalNode();
            anRoot.Question = "cat";
        }

        do
        {
            // Start playing.

            Console.Write("Think of an animal and press Enter.");
            Console.ReadLine();
            AskQuestion(ref anRoot);

            // Save the file.

            StreamWriter sw = new StreamWriter(strFilename);
            anRoot.WriteNode(sw, 0);
            sw.Close();

            // Possibly play again.

            Console.Write("Would you like to play again? ");
        }
        while (GetYesNo());
    }
    static void AskQuestion(ref AnimalNode an)
    {
        if (an.anYes != null)    // Ask a question; get an answer.
        {
            Console.Write(an.Question + " ");

            if (GetYesNo())
                AskQuestion(ref an.anYes);
            else
                AskQuestion(ref an.anNo);
        }
        else    // End of the line; time to wrap up this game.
        {
            Console.Write("Is it a " + an.Question + " (Yes/No)? ");

            if (GetYesNo())
            {
                Console.WriteLine("I guessed it!");
            }
            else
            {
                Console.Write("What animal were you thinking of? ");
                string strAnimal = Console.ReadLine();

                Console.WriteLine("And what yes/no question might " + 
                                  "distinguish a " + strAnimal + 
                                  " from a " + an.Question + "? ");
                string strQuestion = Console.ReadLine();

                Console.Write("And what's the answer for a " + 
                              strAnimal + " (Yes/No)? ");
                bool bAnswer = GetYesNo();

                AnimalNode anNewAnimal = new AnimalNode();
                anNewAnimal.Question = strAnimal;

                AnimalNode anNewQuestion = new AnimalNode();
                anNewQuestion.Question = strQuestion;

                if (bAnswer)
                {
                    anNewQuestion.anYes = anNewAnimal;
                    anNewQuestion.anNo = an;
                }
                else
                {
                    anNewQuestion.anYes = an;
                    anNewQuestion.anNo = anNewAnimal;
                }
                an = anNewQuestion;
            }
        }
    }
    static bool GetYesNo()
    {
        while (true)
        {
            string strAnswer = Console.ReadLine().Trim().ToUpper();

            if (strAnswer.Length > 0 && strAnswer[0] == 'Y')
                return true;

            else if (strAnswer.Length > 0 && strAnswer[0] == 'N')
                return false;

            Console.Write(" Try again: ");
        }
    }
}
