using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentRecords : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["course"] == null)
            {
                ////todo: Redirect the user to Default.aspx page
                Response.Redirect("./Default.aspx");

            }
            else
            {
                //todo: Retrieve course from the session
                Course course = (Course)Session["course"];

                // print course in title of page
                Page.Title = course.ToString();

                //todo: Call Display method to display students in table (empty on first load)
                DisplayStudents(course.StudentList);

            }


        }
       

    }

    protected void btnAddtoCourseRecord(object sender, EventArgs e)
    {
        //create variables from
        string id = txtStudentID.Text;
        string name = txtStudentName.Text;

        string firstName = name.Split(' ')[0];
        string lastName = name.Split(' ')[1];

        int grade = int.Parse(txtGrade.Text);

        //create student object
        Student student = new Student(id, firstName, lastName, grade);
        
        
        //Put course session inside course variable
        Course course = (Course)Session["course"];

        //add student object to the StudentList inside the course object usign the AddStudent Method
        course.AddStudent(student);

        //update session with course object (which now has student object added to student list)
        Session["course"] = course;

        //Display list of students in list held in course object
        DisplayStudents(course.StudentList);
 
    }

    protected void SortStudentList(object sender, EventArgs e)
    {
        
        //get course session
        Course course = (Course)Session["course"];


        if (sortingOptions.SelectedValue == "ID")
        {

            //new instance of StudentComparer Class
            course.StudentList.Sort(new StudentComparer());
            course.StudentList.Sort((s1, s2) => int.Parse(s1.ID).CompareTo(int.Parse(s2.ID)));
            DisplayStudents(course.StudentList);

        }
        if (sortingOptions.SelectedValue == "Name")
        {

            //new instance of StudentComparer Class

            course.StudentList.Sort(new StudentComparer());
            course.StudentList.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));
            DisplayStudents(course.StudentList);

        }
        if (sortingOptions.SelectedValue == "Grade")
        {

            //new instance of StudentComparer Class

            course.StudentList.Sort(new StudentComparer());
            course.StudentList.Sort((s1, s2) => s1.Grade.CompareTo(s2.Grade));
            DisplayStudents(course.StudentList);

        }
       
    }


    private void DisplayStudents(List<Student> student)
    {
        //CREDIT FOR !student.Any() property by: Randy Wu
        if (!student.Any())
        
        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "No Student Record Exist!";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            studentRecordsTable.Rows.Add(lastRow);
        }
        else
        {
            foreach (Student stud in student)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = stud.ID;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = stud.FirstName + " " + stud.LastName;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = stud.Grade.ToString();
                //Why is it ToString?
                row.Cells.Add(cell);

                studentRecordsTable.Rows.Add(row);
            }

        }
    }
}