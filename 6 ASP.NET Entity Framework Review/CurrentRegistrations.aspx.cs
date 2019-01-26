using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RegistrationEF;

public partial class CurrentRegistrations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Redirect unauthenticated user to the Default page.
        string authenticationStatus = Session["loggedIn"] as string;

        if (authenticationStatus == "no" || authenticationStatus == null)
        {
            Response.Redirect("Default.aspx");
        }

        //Get the Home LinkButton on the Master page
        //make it visible and attach an event handler 
        //so that when clicked, redirect the user to the Default page.

        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += (s, a) => Response.Redirect("Default.aspx");
       
        //I know this isn't properly connected to masterpage buttons at the moment :(
        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
        if (!IsPostBack)
        {
            topMenu.Items.Add(new ListItem("Home"));
            topMenu.Items.Add(new ListItem("Log Out"));
        }
        //topMenu.Click += topMenu_Click;

        //Get the Logout LinkButton on the Master page
        //make it visible and attach the event handler Logout to it
        LinkButton btnLogout = (LinkButton)Master.FindControl("btnLogout");
        btnLogout.Click += (s, a) => Logout(s, a);


        using (RegistrationDB entityContext = new RegistrationDB())
        {
            //Get the registered course list of the authenticated user.
            string studentNumber = Session["studentNumber"] as string;

            //trying to pair the student's number with the course code from the DB. 
            //I think my sql server isn't correct so I have some incorrect variables here

            //attempted to display correct courses via the database using Entity Framework 
            List<Course> CourseList = (from a in entityContext
                                 where a.Course.Code == Registration.Course_CourseID && a.StudentId == studentNumber
                                 select a).FirstOrDefault<Course>();





            //If the user clicked the delete link of a coure,
            //delete the selected course from the user's registration
            //same problems as above in terms of accessing "Registration"

            string action = Request.Params["action"] as string;

            if (action == "delete")
            {
                string ID = Request.Params["id"];
                var course = (from c in entityContext.Courses where c.CourseID == ID select c).FirstOrDefault<Course>();
                if (course != null)
                {
                    //I realize "Registration is the incorrect property
                    for (int i = course.Registration.Count() - 1; i >= 0; i--)
                    {
                        var record = course.Registration.ElementAt<Course>(i);
                        course.Registration.Remove(record);
                    }
                }
                entityContext.Courses.Remove(course);

                entityContext.SaveChanges();

                Response.Redirect("CurrentRegistrations.aspx");
            }


            //making list to display up to date courses
            



            //Display the registered course list (use ShowCourseInfo method)

            ShowCourseInfo(CourseList);



        }

    }

    //alternative method for displaying link buttons and doing redirects
    //protected void topMenu_Click(object sender, BulletedListEventArgs e)
    //{
    //    if (e.Index == 0)
    //    {
    //        Response.Redirect("Default.aspx");
    //    }
    //    if (e.Index == 1)
    //    {
    //        //trying to call logout method 
    //        //CurrentRegistrations.Logout(s,a);
    //    }

    //}


    protected void Logout(object sender, EventArgs e)
    {
        //Clear session and redirect the user to the Default page.

        Session.Abandon();
        Response.Redirect("Default.aspx");

    }

    private void ShowCourseInfo(List<Course> courses)
    {
        if (courses == null || courses.Count == 0)
        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "You have not registered any course yet";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            tblCourses.Rows.Add(lastRow);
        }
        else
        {
            foreach (Course course in courses)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = course.CourseID;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = course.CourseTitle;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = course.HoursPerWeek.ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "<a class='deleteRegistration' href='CurrentRegistrations.aspx?action=delete&id=" + course.CourseID + "'>Delete</a>";
                row.Cells.Add(cell);
                tblCourses.Rows.Add(row);
            }
        }
    }
}