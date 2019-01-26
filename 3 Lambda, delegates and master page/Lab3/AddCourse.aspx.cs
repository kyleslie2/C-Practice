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
       
        //SET UP MASTER PAGE LINKS 

        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += (s, a) => Response.Redirect("Default.aspx");
        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");

        List<Course> courses = (List<Course>)Session["courses"];


        if (!IsPostBack)
        {
            //topMenu.Items.Add(new ListItem("AddCourses"));
            topMenu.Items.Add(new ListItem("Add Student Records"));

        }
        topMenu.Click += topMenu_Click;

        //PUT STRING INSIDDDE SORT DIRECTION VAR
        ///////////////////////////////////////////////
       
        string sortDirection = (string)Session["sortDirection"];
        
        //put the params from aspx page here
        string sort = Request.Params["sort"];

        displayCourseTable(courses);




        //if (!string.IsNullOrEmpty(sort))
        //{
        //    displayCourseTable(courses);
        //}
        /////////////////////////////////////////////?


        //SORT BASED ON PARAM
        //sort list based on the param selected 
        //use session to indicate whether it should sort in one direction or another
        if (sort == "code")
        {
            if (sortDirection == "ascending")
            {
                /////////////////////////////////////////////?

                displayCourseTable(courses.OrderBy(x => int.Parse(x.CourseNumber)).ToList());
                Session["sortDirection"] = "descending";
                /////////////////////////////////////////////?

            }

            else
            {
                displayCourseTable(courses.OrderByDescending(x => int.Parse(x.CourseNumber)).ToList());
                Session["sortDirection"] = "ascending";
            }
        }

        if (sort == "title")
        {
            if (sortDirection == "ascending")
            {
                displayCourseTable(courses.OrderBy(x => x.CourseName).ToList());
                Session["sortDirection"] = "descending";
            }

            else
            {
                displayCourseTable(courses.OrderByDescending(x => x.CourseName).ToList());
                Session["sortDirection"] = "ascending";
            }
        }

    }

    //METHOD FOR CLICKING LINK IN HEAD
    protected void topMenu_Click(object sender, BulletedListEventArgs e)
    {
      if (e.Index == 0)
        {
            Response.Redirect("./AddStudent.aspx");
        }

    }


    protected void btnAddCourse(object sender, EventArgs e)
    {
        //make variable for list of courses (does not make a new list yet
        List<Course> courses;

        if (Session["courses"] != null)
        {
            //if the session exists put it into the variable courses
            courses = (List<Course>)Session["courses"];
        }
        else
        {
            //if the session doesn't exist, make a new list!
            courses = new List<Course>();
        }

        //get inputs from aspx
        string cNumber = txtCourseNumber.Text;
        string cName = txtCourseName.Text;

        //create new course object. You know the constructor format from the object viewer/ dll file
        Course newCourse = new Course(cNumber, cName);
        courses.Add(newCourse);

        //PUT UPDATED COURSE LIST INTO SESSION
        Session["courses"] = courses;
        //CALL DISPLAY METHOD
        displayCourseTable(courses);

    }

    //FOR EASY TESTING
    protected void btnClearSessions(object sender, EventArgs e)
    {
        List<Course> courses = (List<Course>)Session["courses"];

        if (courses != null)
        {
            
            Session.Abandon();

            displayCourseTable(courses);
        }
        Response.Redirect("./AddCourse.aspx");

    }


    private void displayCourseTable(List<Course> courses, string sort = "code")
    {

        //////////////////////////////////////////////////////////////////
        for (int i = CourseTable.Rows.Count - 1; i > 0; i--)
        {
            CourseTable.Rows.RemoveAt(i);
        }
        //////////////////////////////////////////////////////////////////


        //if courses list doesn't exists OR there are no courses in the list...
        if (courses == null || courses.Count == 0 )

        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "No Course Record Exist!";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            CourseTable.Rows.Add(lastRow);
        }
        else
        {
            foreach (Course c in courses)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = c.CourseNumber;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = c.CourseName;
                row.Cells.Add(cell);

                CourseTable.Rows.Add(row);
            }

        }
       
    }

}
