// ---------------------------------------------------
// BetterPoints.cs from "Programming in the Key of C#"
// ---------------------------------------------------
using System;

class BetterPoints
{
    static void Main()
    {
        Point2D ptA = new Point2D(10, 20);
        Point2D ptB = new Point2D(20, 30);

        Console.WriteLine(ptA - ptB);

        Point3D ptC = new Point3D(10, 20, 10);
        Point3D ptD = new Point3D(20, 30, 10);

        Console.WriteLine(ptC - ptD);
    }
}
class Point3D
{
    public double x, y, z;

    public Point3D()
    {
        x = y = z = 0;
    }
    public Point3D(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static double operator - (Point3D pt1, Point3D pt2)
    {
        return Math.Sqrt(Math.Pow(pt1.x - pt2.x, 2) + 
            Math.Pow(pt1.y - pt2.y, 2) + 
            Math.Pow(pt1.z - pt2.z, 2));
    }
}
class Point2D: Point3D
{
    private new double z = 0;

    public Point2D()
    {
        z = z;    // Prevents a warning message
    }
    public Point2D(double x, double y): base(x, y, 0) 
    {
    }
}
