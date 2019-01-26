using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Book
/// </summary>
public class Course
{
    private string courseNumber;
    public string CourseNumber { get { return courseNumber; } }

    private string courseName;
    public string CourseName { get { return courseName; } }

    public List<Student> StudentList = new List<Student>();

    //A constructor to initialize a course object with a spcified course number and course name
    public Course(string courseNumber, string courseName)
    {

        this.courseNumber = courseNumber;
        this.courseName = courseName;
    }

    //A method to add a student object to the course
    public void AddStudent(Student newStudent)
    {

        StudentList.Add(newStudent);
    }

    public List<Student> GetStudents()
    {

        return StudentList;
    }

    public override string ToString()
    {
        return CourseName + " " + CourseNumber;
    }



  


}




