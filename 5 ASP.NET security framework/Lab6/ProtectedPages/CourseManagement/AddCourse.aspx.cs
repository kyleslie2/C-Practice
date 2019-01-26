using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentRecordDal;

public partial class Default2 : BasePage
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        #region set top menu
        base.Page_Load(sender, e);
        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
        topMenu.Items[0].Enabled = false;
        #endregion

        if (!IsPostBack)
        {
            //initial textbox values
            txtCourseName.Text = "";
            txtCourseNumber.Text = "";
            txtCourseNumber.ReadOnly = false;
        }

        //find object with value associated with string "action"-- in the table header string below
        string action = Request.Params["action"] as string;
        if (action == "edit")
        {
            //add and remove delegates when you click the word edit (makes it so newcourse_click will run on SaveChange_Click will run
            btnAddCourseInfo.Click -= btnNewCourse_Click;
            btnAddCourseInfo.Click += btnChangeCourseInfo_Click;
        }
        else
        {
            btnAddCourseInfo.Click -= btnChangeCourseInfo_Click;
            btnAddCourseInfo.Click += btnNewCourse_Click;
        }
    }
    protected void btnNewCourse_Click(object sender, EventArgs e)
    {
        //coursecode that was input converted and trimmed
        string courseCode = txtCourseNumber.Text.ToUpper().Trim();

        //Entity frameword to add a new course to database
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            //checking if course code is already in database
            if ((from c in entityContext.Courses
                 where c.Code == courseCode
                 select c).FirstOrDefault() != null)
            {
                txtCourseNumberExist.Text = "Course with this code already exists";
            }
            else
            {
                //creating new course and adding it to database (taking courseNumber that's already input and adding new Name)
                Course course = new Course();
                course.Code = courseCode;
                course.Title = txtCourseName.Text;
                entityContext.Courses.Add(course);
                entityContext.SaveChanges();

                //this is important...
                Response.Redirect("AddCourse.aspx");
            }
        }
    }

    protected void btnChangeCourseInfo_Click(object sender, EventArgs e)
    {

        string courseCode = txtCourseNumber.Text.ToUpper().Trim();
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            Course course = (from c in entityContext.Courses
                             where c.Code == courseCode
                             select c).FirstOrDefault<Course>();
            if (course != null)
            {
                course.Title = txtCourseName.Text;
                entityContext.Entry(course).State = System.Data.Entity.EntityState.Modified; ;
                entityContext.SaveChanges();

                Response.Redirect("AddCourse.aspx");
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Entity Framework
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            //creates a list of courses from Student Records enetities
            List<Course> courses = entityContext.Courses.ToList<Course>();

            //gets string from action 
            string action = Request.Params["action"] as string;

            if (action == "delete")
            {
                //determines which course via params
                string codeFromTable = Request.Params["id"];

                //if course code matches course in database select it 
                var matchingCourse = (from c in entityContext.Courses where c.Code == codeFromTable select c).FirstOrDefault<Course>();
                //if course isn't null, remove it from Academic record 
                if (matchingCourse != null)
                {
                    for (int i = matchingCourse.AcademicRecords.Count() - 1; i >= 0; i--)
                    {
                        var matchingAcademicRecord = matchingCourse.AcademicRecords.ElementAt<AcademicRecord>(i);
                        matchingCourse.AcademicRecords.Remove(matchingAcademicRecord);
                    }
                }

                //remove from database
                entityContext.Courses.Remove(matchingCourse);

                entityContext.SaveChanges();

                Response.Redirect("AddCourse.aspx");
            }
            else if (action == "edit")
            {
                string codeFromTable = Request.Params["id"];
                var matchingCourse = (from c in entityContext.Courses where c.Code == codeFromTable select c).FirstOrDefault<Course>();
                if (matchingCourse != null)
                {
                    txtCourseName.Text = matchingCourse.Title;
                    txtCourseNumber.Text = codeFromTable;
                    txtCourseNumber.ReadOnly = true;
                }
            }
            ShowCourseInfo(courses, Request.Params["sort"]);
        }
    }

    private void ShowCourseInfo(List<Course> courses, string sort)
    {
        for (int i = CourseTable.Rows.Count - 1; i > 0; i--)
        {
            CourseTable.Rows.RemoveAt(i);
        }
        if (courses == null || courses.Count == 0)
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
            //how to sort table based on string in params request
            if (sort == "code")
            {
                courses.Sort((c1, c2) => c1.Code.CompareTo(c2.Code));
            }
            else if (sort == "title")
            {
                courses.Sort((c1, c2) => c1.Title.CompareTo(c2.Title));
            }
            //if there's no params string look at sessions
            if (!string.IsNullOrEmpty(sort))
            {
                if (Session["order"] != null && (string)Session["order"] == "descending")
                {
                    courses.Reverse();
                    Session["order"] = "ascending";
                }
                else
                {
                    Session["order"] = "descending";
                }
            }


            foreach (Course c in courses)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = c.Code;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = c.Title;
                row.Cells.Add(cell);

                //set up string for params request + add c.Code for matching
                cell = new TableCell();
                cell.Text = "<a href='AddCourse.aspx?action=edit&id=" + c.Code + "'>Edit</a>"
                    + " | <a class='deleteCourse' href='AddCourse.aspx?action=delete&id=" + c.Code + "' >Delete</a>";
                row.Cells.Add(cell);

                CourseTable.Rows.Add(row);
            }

        }
    }

}