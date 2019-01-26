using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlgonquinCollege.Registration.Entities;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SET UP SESSION VARIABLES
        //list for students added to a course
        List<AcademicRecord> records = (List<AcademicRecord>)Session["records"];
        //list of courses added on last page
        List<Course> courses = (List<Course>)Session["courses"];
        //course chosen to add to
        Course selectedCourse = (Course)Session["selectedCourse"];

        //BUTTONS IN HEADER
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += (s, a) => Response.Redirect("Default.aspx");
        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");


        if (!IsPostBack)
        {
            topMenu.Items.Add(new ListItem("AddCourses"));
            //topMenu.Items.Add(new ListItem("Add Student Records"));

            //PROPAGATE DROPDOWN LIST WITH COURSES FROM COURSE LIST
            if (courses != null)
            {
                foreach (Course c in courses)
                {
                    dropdownCourseList.Items.Add(c.ToString());
                }

            }
           

        }
        topMenu.Click += topMenu_Click;

    }

    //LINK REDIRECT
    protected void topMenu_Click(object sender, BulletedListEventArgs e)
    {
        if (e.Index == 0)
        {
            Response.Redirect("./AddCourse.aspx");
        }
       
      
    }




    protected void dropdownCourseList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if the records and courses sessions exist and if the dropdown list is changed from it's initial value then...
        if (Session["courses"] != null && Session["records"] != null && dropdownCourseList.SelectedItem.Value != "-1")
        {
            //remove the students from   the table, put records and courses sessions in vars            
            removeStudents();
            List<AcademicRecord> records = (List<AcademicRecord>)Session["records"];
            List<Course> courses = (List<Course>)Session["courses"];

            //make selectedCourse the item chosen
            Course selectedCourse = courses[dropdownCourseList.SelectedIndex - 1];

            //display the items in records for the course chosen
            foreach (AcademicRecord r in records)
            {
                if (r.Course == selectedCourse)
                {
                    DisplayStudentsTable(r);
                }
            }
        }
    }





    protected void btnAddtoCourseRecord(object sender, EventArgs e)
    {
        //put inputs into vars
        string sNumber = txtStudentID.Text;
        string sName = txtStudentName.Text;
        int sGrade = int.Parse(txtGrade.Text);

        //attach courses sesssion to courses list and selectedCourse to index of dropdown
        List<Course> courses = (List<Course>)Session["courses"];
        Course selectedCourse = courses[dropdownCourseList.SelectedIndex - 1];

        //create new student object with inputs and new academicrecord object with selected course and student objects
        Student student = new Student(sNumber, sName);
        AcademicRecord newRecord = new AcademicRecord(selectedCourse, student);

        //add grade to record object
        newRecord.Grade = sGrade;



        //make records list var (no list yet)
        List<AcademicRecord> records;
        if (Session["records"] != null)
        {
            //if records session exists, attach it to records list and add new Record, otherwise make new list 
            records = (List<AcademicRecord>)Session["records"];
            records.Add(newRecord);
        }
        else
        {
            records = new List<AcademicRecord>();
            records.Add(newRecord);
        }

        //make list accessible by content page
        List<AcademicRecord> visibleList = new List<AcademicRecord>();

        //add the students to the new list
        foreach (AcademicRecord r in records)
        {
            if (r.Course == selectedCourse)
            {
                visibleList.Add(r);
            }
        }

        //The following line of code was copied from Brenna Arbour
        visibleList = visibleList.OrderBy(x => x.Student.Name.Split(' ').Last()).ThenBy(x => x.Student.Name.Split(' ').First()).ToList();

        //update records sesssion and selectedCourse 
        Session["records"] = records;
        Session["selectedcourse"] = selectedCourse;

        //display students using the new list
        foreach (AcademicRecord r in visibleList)
        {
            DisplayStudentsTable(r);
        }
    }

  
    //remove students
    private void removeStudents()
    {
        while (studentRecordsTable.Rows.Count >1)
        {
            studentRecordsTable.Rows.RemoveAt(1);
        }
    }

    private void DisplayStudentsTable(AcademicRecord r)
    {
        TableRow row = new TableRow();

        TableCell cell = new TableCell();
        cell.Text = r.Student.Id;
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = r.Student.Name;
        row.Cells.Add(cell);

        cell = new TableCell();
        cell.Text = r.Grade.ToString();
        row.Cells.Add(cell);

        studentRecordsTable.Rows.Add(row);
    }
}