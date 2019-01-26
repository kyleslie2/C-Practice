using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentRecordDal;


public partial class Default4 : BasePage
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
            txtFullName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        
    }

    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        string employeeName = txtFullName.Text.ToUpper().Trim();
        string employeeUsername = txtUsername.Text.ToLower().Trim();
        string employeePassword = txtPassword.Text.Trim();


        //Entity framework to add a new employee to database
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            //checking if employee is already in database
            if ((from em in entityContext.Employees
                 where em.Name == employeeName || em.UserName == employeeUsername
                 select em).FirstOrDefault() != null)
            {
                lblEmployeeError.Text = 
                    "Employee with this name or username already exists";
            }
            else
            {
                //creating new employee and adding it to database (taking username that's already input and adding new Name)

                //make counter for employee ID
                List<Employee> employeesList = entityContext.Employees.ToList<Employee>();


                List<string> selectedRoles = checkboxListRoles.Items.Cast<ListItem>()
                            .Where(li => li.Selected)
                            .Select(li => li.Value)
                            .ToList();
                Employee newEmployee = new Employee();


                //The following code was written by Eric Taylor//////////// 
                var Department_Chair = (from dc in entityContext.Roles where dc.Role1 == "Department Chair" select dc).FirstOrDefault<Role>();
                var Coordinator = (from c in entityContext.Roles where c.Role1 == "Coordinator" select c).FirstOrDefault<Role>();
                var Instructor = (from i in entityContext.Roles where i.Role1 == "Instructor" select i).FirstOrDefault<Role>();

                for (int i = 0; i < selectedRoles.Count; i++)
                {
                    if (selectedRoles[i].Contains("Department Chair"))
                    {
                        newEmployee.Roles.Add(Department_Chair);
                    }
                    else if (selectedRoles[i].Contains("Coordinator"))
                    {
                        newEmployee.Roles.Add(Coordinator);
                    }
                    else if (selectedRoles[i].Contains("Instructor"))
                    {
                        newEmployee.Roles.Add(Instructor);
                    }
                }
                /////////////////////////////////////////////////////////


                newEmployee.Name = txtFullName.Text;
                newEmployee.UserName = txtUsername.Text;
                newEmployee.Password = txtPassword.Text;



                entityContext.Employees.Add(newEmployee);
                entityContext.SaveChanges();

                Response.Redirect("AddEmployee.aspx");
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Entity Framework
        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            List<Employee> employees = entityContext.Employees.ToList<Employee>();
            //List<Role> roles = entityContext.Roles.ToList<Role>();



            ShowEmployeeInfo(employees);
        }
    }

   



    private void ShowEmployeeInfo(List<Employee> employees)
    {
        for (int i = EmployeeTable.Rows.Count - 1; i > 0; i--)
        {
            EmployeeTable.Rows.RemoveAt(i);
        }
        if (employees == null || employees.Count == 0)
        {
            TableRow lastRow = new TableRow();
            TableCell lastRowCell = new TableCell();
            lastRowCell.Text = "No Employee Records Exist!";
            lastRowCell.ForeColor = System.Drawing.Color.Red;
            lastRowCell.ColumnSpan = 3;
            lastRowCell.HorizontalAlign = HorizontalAlign.Center;
            lastRow.Cells.Add(lastRowCell);
            EmployeeTable.Rows.Add(lastRow);
        }
        else
        {
            foreach (Employee e in employees)
            {

                if (e.Roles != null && e.Roles.Count != 0)
                {


                    //This code was provided by Randy Wu////////////
                    string displayRoles = e.Roles.ToList()[0].Role1;
                    for (int i = 1; i < e.Roles.Count(); i++)

                    {
                        displayRoles += ", " + e.Roles.ToList()[i].Role1;
                    }
                    /////////////////////////////////////////////////

                    TableRow row = new TableRow();

                    //TableCell cell = new TableCell();
                    //cell.Text = e.Id.ToString();
                    //row.Cells.Add(cell);

                    TableCell cell = new TableCell();
                    cell.Text = e.Name;
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = e.UserName;
                    row.Cells.Add(cell);



                    cell = new TableCell();
                    cell.Text = displayRoles;
                    row.Cells.Add(cell);




                    EmployeeTable.Rows.Add(row);
                }
                else
                {
                    TableRow row = new TableRow();

                    //TableCell cell = new TableCell();
                    //cell.Text = e.Id.ToString();
                    //row.Cells.Add(cell);

                    TableCell cell = new TableCell();
                    cell.Text = e.Name;
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = e.UserName;
                    row.Cells.Add(cell);



                    cell = new TableCell();
                    cell.Text = "It's Null or empty";
                    row.Cells.Add(cell);




                    EmployeeTable.Rows.Add(row);
                }

            }

        }
    }

}