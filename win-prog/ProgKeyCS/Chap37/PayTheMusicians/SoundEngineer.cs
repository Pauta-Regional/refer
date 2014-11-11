// ----------------------------------------------------
// SoundEngineer.cs from "Programming in the Key of C#"
// ----------------------------------------------------
class SoundEngineer: Musician
{
    public SoundEngineer(string strName): base(strName)
    {
    }
    public override decimal CalculatePay()
    {
        return 1.25m * base.CalculatePay();
    }
}

