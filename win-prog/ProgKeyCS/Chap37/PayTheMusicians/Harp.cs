// -------------------------------------------
// Harp.cs from "Programming in the Key of C#"
// -------------------------------------------
class Harp: Musician
{
    int iWeight;

    public Harp(string strName, int iWeight): base(strName)
    {
        this.iWeight = iWeight;
    }
    public override decimal CalculatePay()
    {
        return 1.5m * iWeight;
    }
}

