using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
  
    }



    protected void btnSubmitCourseInfo(object sender, EventArgs e)
    {
        //capture inputs into variables
        string numberInput = CourseNumber.Text;
        string nameInput = CourseName.Text;

        //create course object with inputs
        Course course = new Course(numberInput, nameInput);

        //put course object into course session
        Session["course"] = course;

        //Go to next page
        Response.Redirect("./StudentRecords.aspx");

    }
}