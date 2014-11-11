// --------------------------------------------------
// Temperature.cs from "Programming in the Key of C#"
// --------------------------------------------------
using System;

namespace Petzold.KeyOfCSharp
{
    public struct Temperature
    {
        double dKelvin;

        public double Kelvin
        {
            set 
            { 
                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                dKelvin = value; 
            }
            get { return dKelvin; }
        }
        public double Celsius
        {
            set { Kelvin = value + 273.15; }
            get { return Kelvin - 273.15; }
        }
        public double Fahrenheit
        {
            set { Celsius = 5 * (value - 32) / 9; }
            get { return 9 * Celsius / 5 + 32; }
        }
        public Temperature(double dTemp, TemperatureUnits tu)
        {
            dKelvin = 0;    // Avoid compiler message.

            switch (tu)
            {
            case TemperatureUnits.Kelvin:
                Kelvin = dTemp;
                break;

            case TemperatureUnits.Celsius:
                Celsius = dTemp;
                break;

            case TemperatureUnits.Fahrenheit:
                Fahrenheit = dTemp;
                break;
            }
        }
        public override string ToString()
        {
            return ToString(TemperatureUnits.Celsius);
        }
        public string ToString(TemperatureUnits tu)
        {
            switch (tu)
            {
            case TemperatureUnits.Kelvin:
                return String.Format("{0}\u00B0 K", Kelvin);

            case TemperatureUnits.Celsius:
                return String.Format("{0}\u00B0 C", Celsius);

            case TemperatureUnits.Fahrenheit:
                return String.Format("{0}\u00B0 F", Fahrenheit);
            }
            return "";
        }
    }
}