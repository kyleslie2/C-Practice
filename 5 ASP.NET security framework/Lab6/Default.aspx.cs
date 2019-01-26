using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using StudentRecordDal;

public partial class Default : BasePage
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Enabled = false;

        pnlGreeting.Visible = false;

        if(Request.IsAuthenticated)
        {
            pnlGreeting.Visible = true;
            pnlLogin.Visible = false;
        }

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.ToLower().Trim();
        string password = txtPassword.Text.Trim();

        //taken from slides
        //slides were login.aspx.cs, not default. webconfig automatically redirects to default.aspx

        using (StudentRecordEntities entityContext = new StudentRecordEntities())
        {
            Employee em = (from emp in entityContext.Employees
                           where emp.UserName == userName && emp.Password == password
                           select emp).FirstOrDefault<Employee>();
            if (em != null)
            {
                
                FormsAuthentication.RedirectFromLoginPage(em.Id.ToString(), false);
            }
            else
            {
                lblLoginError.Text =
                    "The enetered username and/ or password are incorrect!";
            }
        }
    }
}