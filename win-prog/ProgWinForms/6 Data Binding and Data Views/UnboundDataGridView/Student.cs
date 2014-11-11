//----------------------------------------
// Student.cs (c) 2005 by Charles Petzold
//----------------------------------------
using System;

public class Student
{
    CourtesyTitle court = CourtesyTitle.None;
    string strFirstName = "<first name>";
    string strLastName = "<last name>";
    DateTime dtBirth = new DateTime(1985, 1,1);
    bool bEnrolled = false;

    public CourtesyTitle Courtesy
    {
        set { court = value; }
        get { return court; }
    }
    public string FirstName
    {
        set { strFirstName = value; }
        get { return strFirstName; }
    }
    public string LastName
    {
        set { strLastName = value; }
        get { return strLastName; }
    }
    public DateTime BirthDate
    {
        set { dtBirth = value; }
        get { return dtBirth; }
    }
    public bool Enrolled
    {
        set { bEnrolled = value; }
        get { return bEnrolled; }
    }
}
