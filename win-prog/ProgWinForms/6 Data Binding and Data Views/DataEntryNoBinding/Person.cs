//---------------------------------------
// Person.cs (c) 2005 by Charles Petzold
//---------------------------------------
using System;

public class Person
{
    // Private fields.
    string strFirstName, strLastName;
    DateTime dtBirth = new DateTime(1800, 1, 1);

    // Public properties.
    public string FirstName
    {
        get { return strFirstName; }
        set { strFirstName = value; }
    }
    public string LastName
    {
        get { return strLastName; }
        set { strLastName = value; }
    }
    public DateTime BirthDate
    {
        get { return dtBirth; }
        set { dtBirth = value; }
    }
}
