// ------------------------------------------------------
// KeplersThirdLaw.cs from "Programming in the Key of C#"
// ------------------------------------------------------
using System;

class KeplersThirdLaw
{
    static void Main()
    {
        string[] astrPlanet = { "Mercury", "Venus", "Earth", 
                                  "Mars", "Jupiter", "Saturn", 
                                  "Uranus", "Neptune", "Pluto" };

        // Periods of revolution around the sun 
        //     are in earth years.

        double[] adPeriod = { 0.2409, 0.6152, 1.000, 
                                1.881, 11.86, 29.65, 
                                83.67, 163.9, 247.7 } ;

        // Mean distances from the sun are in 
        //     Astronomical Units where Earth is 1.

        double[] adDistance = { 0.3871, 0.7233, 1.000, 
                                  1.524, 5.202, 9.581, 
                                  19.13, 29.95, 39.44 };

        // All data for epoch July 20, 2003 from 
        //     "Observer's Handbook 2003" (Royal 
        //     Astronomical Society of Canada), page 21.
 
        int i = 0;

        while (i < astrPlanet.Length)
        {
            double dRatio = Math.Pow(adPeriod[i], 2) / 
                Math.Pow(adDistance[i], 3);

            Console.WriteLine("{0,-10}: {1,7:F3}", 
                astrPlanet[i], dRatio);
            i++;
        }
    }
}
