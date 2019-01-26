using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentRecordDal;

public partial class Default3 : BasePage
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        #region set top menu
        base.Page_Load(sender, e);
        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
        topMenu.Items[1].Enabled = false;
        #endregion


        int selectedIndex = drpCourse.SelectedIndex;


        if (!IsPostBack)
        {
            //when the page loads: using the database of student records make a list of courses
            //order them by title and apply value and text based on paramaters
            //write them to a list
            using (StudentRecordEntities entityContext = new StudentRecordEntities())
            {
                var courseList = (from c in entityContext.Courses
                                  orderby c.Title
                                  select new { Value = c.Code, Text = (c.Code + " - " + c.Title) }).ToList();
                //if there are no courses in the list reload addStudent?
                if (courseList.Count == 0)
                {
                    Response.Redirect("AddCourse.aspx");
                }

                //add the databound items to the dropdown menu
                drpCourse.AppendDataBoundItems = true;
                drpCourse.DataTextField = "Text";
                drpCourse.DataValueField = "Value";
                drpCourse.DataSource = courseList;

                //does session go here?
                drpCourse.SelectedValue = Session["selectedCourseCode"] as string;
                //databinding what? 
                drpCourse.DataBind();

                drpCourse.Enabled = true;

                //does this go here?
                drpCourse.SelectedIndex = selectedIndex;

                txtStudentName.Text = "";
                txtStudentName.ReadOnly = false;
                txtGrade.Text = "";
            }

        }
        string action = Request.Params["action"] as string;
        if (action == "change")
        {
            btnSaveStudent.Click -= btnAddNewStudent_Click;
            btnSaveStudent.Click += btnChangeGrade_Click;
        }
        else
        {
            btnSaveStudent.Click += btnAddNewStudent_Click;
            btnSaveStudent.Click -= btnChangeGrade_Click;
        }
        txtStudentIdExists.Text = "";


    }

    protected void btnAddNewStudent_Click(object sender, EventArgs e)
    {
        string selectedCourseCode = Session["selectedCourseCode"] as string;

        if (selectedCourseCode != null && selectedCourseCode != "-1")
        {
            using (StudentRecordEntities entityContext = new StudentRecordEntities())
            {
                Course selectedCourse = (from c in entityContext.Courses where c.Code == selectedCourseCode select c).FirstOrDefault<Course>();
                //might need .ToList() or list type after FirstorDefault

                string idInput = txtStudentID.Text.Trim().ToUpper();
                Student newStudent = (from s in entityContext.Students
                                      where s.Id == idInput
                                      select s).FirstOrDefault<Student>();
                if (newStudent == null)
                {
                    newStudent = new Student();
                    newStudent.Name = txtStudentName.Text;
                    newStudent.Id = txtStudentID.Text;
                    entityContext.Students.Add(newStudent);

                    AcademicRecord newRecord = new AcademicRecord();
                    newRecord.Student = newStudent;
                    newRecord.Course = selectedCourse;
                    newRecord.Grade = int.Parse(txtGrade.Text);
                    entityContext.AcademicRecords.Add(newRecord);
                    entityContext.SaveChanges();

                    Response.Redirect("AddStudent.aspx");

                }
                else
                {
                    AcademicRecord ar = (from a in entityContext.AcademicRecords
                                         where a.Course.Code == selectedCourse.Code && a.StudentId == newStudent.Id
                                         select a).FirstOrDefault<AcademicRecord>();

                    //check if the system already has a record of this student/ course?
                    if (ar != null)
                    {
                        txtStudentIdExists.Text = "The system already has record of this student in this class";
                    }
                }
            }

        }
    }


    protected void btnChangeGrade_Click(object sender, EventArgs e)
    {
        string studentID = txtStudentID.Text.ToUpper().Trim();
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            string selectedCourseCode = Session["selectedCourseCode"] as string;
            var academicRecord = (from ar in entityContext.AcademicRecords
                                  where ar.CourseCode == selectedCourseCode
                                  && ar.StudentId == txtStudentID.Text
                                  select ar).FirstOrDefault<AcademicRecord>();

            academicRecord.Grade = int.Parse(txtGrade.Text);

            entityContext.Entry(academicRecord).State = System.Data.Entity.EntityState.Modified; ;
            entityContext.SaveChanges();

            Response.Redirect("AddStudent.aspx");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            string selectedCourseCode = Session["selectedCourseCode"] as string;
            ////////////////////////////////////////////////////////////////////////////////////////

            //useless?
            List<AcademicRecord> records = entityContext.AcademicRecords.ToList<AcademicRecord>();


            string action = Request.Params["action"] as string;

            if (action == "delete")
            {
                string ID = Request.Params["id"];
                var student = (from s in entityContext.Students where s.Id == ID select s).FirstOrDefault<Student>();
                if (student != null)
                {
                    for (int i = student.AcademicRecords.Count() - 1; i >= 0; i--)
                    {
                        var ar = student.AcademicRecords.ElementAt<AcademicRecord>(i);
                        student.AcademicRecords.Remove(ar);
                    }
                }
                entityContext.Students.Remove(student);

                entityContext.SaveChanges();

                Response.Redirect("AddStudent.aspx");
            }
            else if (action == "change")
            {
                string idFromTable = Request.Params["id"];
                var matchingStudent = (from s in entityContext.Students where s.Id == idFromTable select s).FirstOrDefault<Student>();
                if (matchingStudent != null)
                {
                    drpCourse.Text = selectedCourseCode;
                    drpCourse.Enabled = false;
                    txtStudentID.Text = idFromTable;
                    txtStudentID.ReadOnly = true;
                    txtStudentName.Text = matchingStudent.Name;
                    txtStudentName.ReadOnly = true;
                    txtGrade.Text = "";


                }
            }

            List<AcademicRecord> DisplayRecordList = (from ar in entityContext.AcademicRecords
                                                      where ar.CourseCode == selectedCourseCode
                                                      select ar).ToList<AcademicRecord>();


            string selectedCourse = Session["selectedCourseCode"] as string;
            
            drpCourse.Text = selectedCourse;


            ShowAcademicRecords(DisplayRecordList, Request.Params["sort"]);

        }
    }

    protected void dropdownCourseList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["selectedCourseCode"] = drpCourse.SelectedValue;
    }



    private void removeStudents()
    {
        while (studentRecordsTable.Rows.Count > 1)
        {
            studentRecordsTable.Rows.RemoveAt(1);
        }
    }

    private void ShowAcademicRecords(List<AcademicRecord> records, string sort)
    {



        for (int i = studentRecordsTable.Rows.Count - 1; i > 0; i--)
        {
            studentRecordsTable.Rows.RemoveAt(i);
        }
        if (records == null || records.Count == 0)
        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "No Course Record Exist!";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            studentRecordsTable.Rows.Add(lastRow);
        }
        else
        {
            //if (sort == "code")
            //{
            //    courses.Sort((c1, c2) => c1.Code.CompareTo(c2.Code));
            //}
            //else if (sort == "title")
            //{
            //    courses.Sort((c1, c2) => c1.Title.CompareTo(c2.Title));
            //}
            //if (!string.IsNullOrEmpty(sort))
            //{
            //    if (Session["order"] != null && (string)Session["order"] == "descending")
            //    {
            //        courses.Reverse();
            //        Session["order"] = "ascending";
            //    }
            //    else
            //    {
            //        Session["order"] = "descending";
            //    }
            //}

            foreach (AcademicRecord s in records)
            {


                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = s.StudentId;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = s.Student.Name;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = s.Grade.ToString();
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = "<a href='AddStudent.aspx?action=change&id=" + s.StudentId + "'>Change Grade</a>"
                    + " | <a class='deleteRecord' href='AddStudent.aspx?action=delete&id=" + s.StudentId + "' >Delete</a>";
                row.Cells.Add(cell);

                studentRecordsTable.Rows.Add(row);
            }

        }
    }




}



