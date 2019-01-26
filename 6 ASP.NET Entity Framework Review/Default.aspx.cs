 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using RegistrationEF;
public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        pnlLogin.Visible = false;
        pnlWelcome.Visible = false;

        if (!IsPostBack)
        {
            pnlLogin.Visible = true;

            txtStudentNum.Text = "";
            txtPassword.Text = "";



        }
        //show different penal based on the user's authentication status
        if (Session["loggedIn"] != null)
        {
            var authenticationStatus = Session["loggedIn"] as string;
            if (authenticationStatus == "yes")
            {
                pnlLogin.Visible = false;
                pnlWelcome.Visible = true;
                
                //here it will assign the name of the student to the welcome page when they navigate back here after authentication
                //lblName.Text = string(Session)["username"];
                
            }
            else
            {
                pnlLogin.Visible = true;
                pnlWelcome.Visible = false;
                lblLoginError.Text = "Sorry, that username or password was incorrect";
            }


        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string studentNum = txtStudentNum.Text;
        string password = txtPassword.Text;

        using (RegistrationDB entityContext = new RegistrationDB())
        {
            //Authenicate the user's credential againt data stored 
            //in the Student table in the Registration DB by searching 
            //for the student with the user entered studentNum and password
            
            if ((from s in entityContext.Students
                    where s.StudentNum == studentNum && s.Password == password
                    select s).FirstOrDefault() != null)
            {
                //store result into session  if student already exists

                Session["loggedIn"] = "yes";
                (Session)["studentNumber"] = studentNum;



                //sends user to CurrentRegustrations.aspx page. 
                //will give them welcome screen  if they come back here
                Response.Redirect("CurrentRegistrations.aspx");


            }
            else
            {

                // generate 
                Session["loggedIn"] = "no";



                //Reload page so changes are submitted
                Response.Redirect("Default.aspx");
            }
            


        }
    }
}