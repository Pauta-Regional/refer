// -------------------------------------------------
// PointsDemo.cs from "Programming in the Key of C#"
// -------------------------------------------------
using System;

class PointsDemo
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
class Point2D
{
    public double x, y;

    public Point2D() 
    {
        x = y = 0;
    }
    public Point2D(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public static double operator - (Point2D ptL, Point2D ptR)
    {
        return Math.Sqrt(Math.Pow(ptL.x - ptR.x, 2) + 
                         Math.Pow(ptL.y - ptR.y, 2));
    }
}
class Point3D: Point2D
{
    public double z;

    public Point3D()
    {
        z = 0;
    }
    public Point3D(double x, double y, double z): base(x, y)
    {
        this.z = z;
    }
    public static double operator - (Point3D ptL, Point3D ptR)
    {
        return Math.Sqrt(Math.Pow(ptL.x - ptR.x, 2) + 
                         Math.Pow(ptL.y - ptR.y, 2) + 
                         Math.Pow(ptL.z - ptR.z, 2));
    }
}