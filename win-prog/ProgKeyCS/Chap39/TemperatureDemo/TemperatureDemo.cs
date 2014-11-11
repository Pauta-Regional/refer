// ------------------------------------------------------
// TemperatureDemo.cs from "Programming in the Key of C#"
// ------------------------------------------------------
using System;
using Petzold.KeyOfCSharp;

class TemperatureDemo
{
    static void Main()
    {
        Temperature temp = new Temperature();
        Console.WriteLine(temp);

        temp = new Temperature(100, TemperatureUnits.Centigrade);
        Console.WriteLine(temp.ToString(TemperatureUnits.Fahrenheit));
    }
}
