// -----------------------------------------------------
// AssignmentTest.cs from "Programming in the Key of C#"
// -----------------------------------------------------
using System;

class AssignmentTest
{
    static void Main()
    {
        PointStruct ps1 = new PointStruct();
        ps1.x = ps1.y = 22;

        PointStruct ps2 = new PointStruct();
        ps2.x = ps2.y = 33;

        ps1 = ps2;
        ps2.x = ps2.y = 55;

        Console.WriteLine("ps1 is ({0}, {1})", ps1.x, ps1.y);
        Console.WriteLine("ps2 is ({0}, {1})", ps2.x, ps2.y);
        Console.WriteLine("ps1.Equals(ps2) results in " + ps1.Equals(ps2));

        PointClass pc1 = new PointClass();
        pc1.x = pc1.y = 22;

        PointClass pc2 = new PointClass();
        pc2.x = pc2.y = 33;

        pc1 = pc2;
        pc2.x = pc2.y = 55;

        Console.WriteLine("pc1 is ({0}, {1})", pc1.x, pc1.y);
        Console.WriteLine("pc2 is ({0}, {1})", pc2.x, pc2.y);
        Console.WriteLine("pc1.Equals(pc2) results in " + pc1.Equals(pc2));
        Console.WriteLine("pc1 == pc2 results in " + (pc1 == pc2));
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
