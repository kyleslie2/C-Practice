using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Web.Configuration;

public partial class Lab4: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Create the transformation. For better performance cache the compiled XSLT in the session. 
        XslCompiledTransform transform = null;

        if (Application["xslt"] == null)
        {
            transform = new XslCompiledTransform();

            var xslt_path = Server.MapPath("App_Data/restaurant_reviews.xslt");
            //string xsltPath = WebConfigurationManager.AppSettings["xsltPath"];

            transform.Load(xslt_path);

            Application["xslt"] = transform;

            Session["restaurantReviewXslt"] = transform;
        }
        else
        {
            transform = Application["xslt"] as XslCompiledTransform;

            Session["restaurantReviewXslt"] = transform; 
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //retrieve compiled XSLT in the session.
        XslCompiledTransform transform = Session["restaurantReviewXslt"] as XslCompiledTransform;

        //create the XSLT parameters
        int minRating = int.Parse(txtMinRating.Text);
        XsltArgumentList xslArguments = new XsltArgumentList();
        xslArguments.AddParam("minRating", "", minRating.ToString());

        //create transformation output string.
        StringBuilder htmlStringBuilder = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(htmlStringBuilder);

        //transform the xml
        var xml_path = Server.MapPath("App_Data/restaurant_reviews.xml");
        //string xmlPath = WebConfigurationManager.AppSettings["Server.MapPath('restaurant_reviews.xslt')"];

        transform.Transform(xml_path, xslArguments, xw);

        //add transformation result to the page
        string htmlString = htmlStringBuilder.ToString();
        divRestaurantReviews.InnerHtml = htmlString;

    }
}