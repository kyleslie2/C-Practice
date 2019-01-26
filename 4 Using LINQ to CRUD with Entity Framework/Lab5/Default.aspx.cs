using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : BasePage
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        #region set top menu
        base.Page_Load(sender, e);
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Enabled = false;
        #endregion

        if (!IsPostBack)
        {

        }
    }
}