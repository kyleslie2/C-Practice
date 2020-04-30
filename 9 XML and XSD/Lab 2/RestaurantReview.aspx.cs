using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Serialization;


public partial class RestaurantReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        
        if (!IsPostBack)
        {
            //Use the names of the restaurants in the XML file  to populate the dropdown list 

            restaurants allRestaurants = deserialize_xml();

            drpRestaurants.Items.Add("--Select One");

            foreach (restaurant restaurant in allRestaurants.restaurant)
            {
                drpRestaurants.Items.Add(restaurant.name.ToString());

                txtAddress.Text = null;
                txtAddress.Attributes.Add("readonly", "readonly");

                txtCity.Text = null;
                txtCity.Attributes.Add("readonly", "readonly");

                txtProvinceState.Text =null;
                txtProvinceState.Attributes.Add("readonly", "readonly");

                txtPostalZipCode.Text = null;
                txtPostalZipCode.Attributes.Add("readonly", "readonly");

                txtSummary.Text = null;
                txtSummary.Attributes.Add("readonly", "readonly");

                drpRating.Style.Add("display", "none");
            }
        }
    }

    protected void drpRestaurants_SelectedIndexChanged(object sender, EventArgs e)
    {
        //show the selected restaurant data as specified in the lab requirements 
        lblConfirmation.Visible = false;

        restaurants allRestaurants = deserialize_xml();

        string selectedRestaurant = null;
        selectedRestaurant = drpRestaurants.SelectedValue;

        foreach (restaurant restaurant in allRestaurants.restaurant)
        {
            if(restaurant.name == selectedRestaurant)
            {
                txtAddress.Text = restaurant.location.street.ToString();
                txtAddress.Attributes.Add("readonly", "readonly");

                txtCity.Text = restaurant.location.city.ToString();
                txtCity.Attributes.Add("readonly", "readonly");

                txtProvinceState.Text = restaurant.location.provstate.ToString();
                txtProvinceState.Attributes.Add("readonly", "readonly");

                txtPostalZipCode.Text = restaurant.location.postalzipcode.ToString();
                txtPostalZipCode.Attributes.Add("readonly", "readonly");

                txtSummary.Text = restaurant.summary.ToString();
                txtSummary.Attributes.Remove("readonly");

                drpRating.Style.Remove("display");
                drpRating.SelectedValue =restaurant.rating.ToString();
            }
            else if("--Select One" == selectedRestaurant)
            {
                txtAddress.Text = null;
                txtAddress.Attributes.Add("readonly", "readonly");

                txtCity.Text = null;
                txtCity.Attributes.Add("readonly", "readonly");

                txtProvinceState.Text = null;
                txtProvinceState.Attributes.Add("readonly", "readonly");

                txtPostalZipCode.Text = null;
                txtPostalZipCode.Attributes.Add("readonly", "readonly");

                txtSummary.Text = null;
                txtSummary.Attributes.Add("readonly", "readonly");

                drpRating.Style.Add("display", "none");
            }
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Save the changed restaurant restaurant data back to the XML file. 

        string xmlFile = @MapPath("~/App_Data/restaurant_reviews.xml");

        restaurants allRestaurants = deserialize_xml();

        string summaryValue = txtSummary.Text;
        int ratingValue = Convert.ToInt32(drpRating.SelectedValue);


        string selectedRestaurant = null;
        selectedRestaurant = drpRestaurants.SelectedValue;

       
          


        foreach (restaurant restaurant in allRestaurants.restaurant)
        {
            if (restaurant.name == selectedRestaurant)
            {
                    restaurant.summary = summaryValue;
                    restaurant.rating = ratingValue;

                FileStream xs = new FileStream(xmlFile, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(restaurants));
                serializer.Serialize(xs, allRestaurants);

                xs.Close();


            }
        }

        if (drpRestaurants.Text != "--Select One")
        {
            string confirmationString = "Changes have been made too " + xmlFile;
            lblConfirmation.Text = confirmationString;
            lblConfirmation.Visible = true;
        }
       



    }

    public restaurants deserialize_xml()
    {
        //function for deserializing xml
        string xmlString = @MapPath("~/App_Data/restaurant_reviews.xml");

        FileStream xs = new FileStream(xmlString, FileMode.Open);

        XmlSerializer serializor = new XmlSerializer(typeof(restaurants));

        restaurants allRestaurants = (restaurants)serializor.Deserialize(xs);

        xs.Close();

        return allRestaurants;
        
    }

    


}