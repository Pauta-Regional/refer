// -------------------------------------------------------
// InheritedMethods.cs from "Programming in the Key of C#"
// -------------------------------------------------------
using System;

class InheritedMethods
{
    static void Main()
    {
        DerivedClass dc = new DerivedClass();
        BaseClass bc = dc;

        bc.VirtualMethod();
        bc.NonVirtualMethod();
    }
}
class BaseClass
{
    public virtual void VirtualMethod()
    {
        Console.WriteLine("VirtualMethod in BaseClass");
    }
    public void NonVirtualMethod()
    {
        Console.WriteLine("NonVirtualMethod in BaseClass");
    }
}
class DerivedClass: BaseClass
{
    public override void VirtualMethod()
    {
        Console.WriteLine("VirtualMethod in DerivedClass");
    }
    public new void NonVirtualMethod()
    {
        Console.WriteLine("NonVirtualMethod in DerivedClass");
    }
}
