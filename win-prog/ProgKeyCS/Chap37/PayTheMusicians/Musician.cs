// -----------------------------------------------
// Musician.cs from "Programming in the Key of C#"
// -----------------------------------------------
class Musician
{
    public string strName;

    public Musician(string strName)
    {
        this.strName = strName;
    }
    public virtual decimal CalculatePay()
    {
        return 100;
    }
}