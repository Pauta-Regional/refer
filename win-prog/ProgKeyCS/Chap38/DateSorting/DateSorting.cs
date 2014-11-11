// --------------------------------------------------
// DateSorting.cs from "Programming in the Key of C#"
// --------------------------------------------------
using System;

class DateSorting
{
    static void Main()
    {
        string[] astrComposers = 
            {
                "John Adams",         "Johann Sebastian Bach",
                "Bela Bartok",        "Ludwig van Beethoven",
                "Hector Berlioz",     "Pierre Boulez",
                "Johannes Brahms",    "Benjamin Britten",
                "Aaron Copland",      "Claude Debussy",
                "Philip Glass",       "George Frideric Handel",
                "Franz Joseph Haydn", "Gustav Mahler",
                "Claudio Monteverdi", "Wolfgang Amadeus Mozart",
                "Sergei Prokofiev",   "Steve Reich",
                "Franz Schubert",     "Igor Stravinsky",
                "Richard Wagner",     "Anton Webern"
            };
        SuperDate[] asdBirthDates =
            {
                new SuperDate(1947, 2, 15),  new SuperDate(1685, 3, 21),
                new SuperDate(1881, 3, 25),  new SuperDate(1770, 12, 17),
                new SuperDate(1803, 12, 11), new SuperDate(1925, 3, 26),
                new SuperDate(1833, 5, 7),   new SuperDate(1913, 11, 22),
                new SuperDate(1900, 11, 14), new SuperDate(1862, 8, 22),
                new SuperDate(1937, 1, 31),  new SuperDate(1685, 2, 23),
                new SuperDate(1732, 3, 31),  new SuperDate(1860, 7, 7),
                new SuperDate(1567, 5, 15),  new SuperDate(1756, 1, 27),
                new SuperDate(1891, 4, 23),  new SuperDate(1936, 10, 3),
                new SuperDate(1797, 1, 31),  new SuperDate(1882, 6, 17),
                new SuperDate(1813, 5, 22),  new SuperDate(1883, 12, 3)
            };

        Array.Sort(asdBirthDates, astrComposers);

        for (int i = 0; i < astrComposers.Length; i++)
            Console.WriteLine("{0} was born on {1}.",
                astrComposers[i], asdBirthDates[i]);
    }
}