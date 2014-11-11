// -------------------------------------------------
// FrenchHorn.cs from "Programming in the Key of C#"
// -------------------------------------------------
class FrenchHorn: Musician
{
    int iGoodNotes, iFlubbedNotes;

    public FrenchHorn(string strName, int iGoodNotes, int iFlubbedNotes):
        base(strName)
    {
        this.iGoodNotes = iGoodNotes;
        this.iFlubbedNotes = iFlubbedNotes;
    }
    public override decimal CalculatePay()
    {
        return 1.5m * iGoodNotes + 0.75m * iFlubbedNotes;
    }
}

