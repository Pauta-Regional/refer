// ---------------------------------------------
// Violin.cs from "Programming in the Key of C#"
// ---------------------------------------------
class Violin: Musician
{
    int iBrokenStrings;

    public Violin(string strName, int iBrokenStrings): base(strName)
    {
        this.iBrokenStrings = iBrokenStrings;
    }
    public override decimal CalculatePay()
    {
        return 125 - 50 * iBrokenStrings;
    }
}

