// -------------------------------------------------
// EqualsTest.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class EqualsTest
{
    static void Main()
    {
        PointStruct ps1 = new PointStruct();
        ps1.x = ps1.y = 55;

        PointStruct ps2 = new PointStruct();
        ps2.x = ps2.y = 55;

        PointClass pc1 = new PointClass();
        pc1.x = pc1.y = 55;

        PointClass pc2 = new PointClass();
        pc2.x = pc2.y = 55;

        Console.WriteLine("ps1.Equals(ps2) results in " + ps1.Equals(ps2));
        Console.WriteLine("ps1.Equals(pc1) results in " + ps1.Equals(pc1));
        Console.WriteLine("pc1.Equals(pc2) results in " + pc1.Equals(pc2));
        Console.WriteLine("pc1 == pc2 results in " + (pc1 == pc2));
        // Console.WriteLine("ps1 == ps2 results in " + (ps1 == ps2));
    }
}
struct PointStruct
{
    public int x, y;
}
class PointClass
{
    public int x, y;
}

