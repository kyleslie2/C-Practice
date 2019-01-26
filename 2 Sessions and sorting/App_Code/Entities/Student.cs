using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Student
{
    private string id;
    public string ID { get { return id; } }

    private string firstName;
    public string FirstName { get { return firstName; } }

    private string lastName;
    public string LastName { get { return lastName; } }

    private int grade;
    public int Grade { get { return grade;} }

    public Student(string id, string firstName, string lastName, int grade)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.grade = grade;
    }



    //~~~~~~~~MOVED THIS TO STUDENTCOMPARER CLASS



    //public int CompareTo(Student other)
    //{
    //    if (other == null) return 1;

    //    //if (other.LastName == this.LastName)
    //    //{
    //    //    return this.FirstName.CompareName(other.FirstName);
                

    //    //}
    //    return this.ID.CompareTo(other.ID);
    //}

    //static int CompareName(Student student1, Student student2)
    //{
    //    if (student1 == null && student2 != null)
    //        return -1;
    //    if (student1 != null && student2 == null)
    //        return 1;
    //    if (student1 == null && student2 == null)
    //        return 0;

    //    return student1.FirstName.CompareTo(student2.FirstName);
    //}


}






