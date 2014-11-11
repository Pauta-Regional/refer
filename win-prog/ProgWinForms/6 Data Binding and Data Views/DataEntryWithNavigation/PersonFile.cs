//-------------------------------------------
// PersonFile.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Collections.Generic;

public class PersonFile
{
    DateTime dtCreation = DateTime.Now;
    List<Person> persons = new List<Person>();

    public DateTime CreationDate
    {
        get { return dtCreation; }
        set { dtCreation = value; }
    }

    public List<Person> Persons
    {
        get { return persons; }
        set { persons = value; }
    }
}
