//---------------------------------------
// School.cs (c) 2005 by Charles Petzold
//---------------------------------------
using System.Collections.Generic;

public class School
{
    List<Student> studentlist = new List<Student>();

    public List<Student> Students
    {
        set { studentlist = value; }
        get { return studentlist; }
    }
}
