using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Serialization;
using Lab_2.Lab6Service;


public partial class RestaurantReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        
        if (!IsPostBack)
        {
            //Use the names of the restaurants in the XML file  to populate the dropdown list 
            RestaurantReviewServiceClient reviewer = new RestaurantReviewServiceClient();

            string[] restaurantNames = reviewer.GetRestaurantNames();
            drpRestaurants.DataSource = restaurantNames;
            drpRestaurants.DataBind();
            drpRestaurants.Items.Insert(0, new ListItem("Select One...", "0"));
            pnlViewRestaurant.Visible = false;

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

    protected void drpRestaurants_SelectedIndexChanged(object sender, EventArgs e)
    {
        //show the selected restaurant data as specified in the lab requirements 
        lblConfirmation.Visible = false;

        string selectedRestaurant = null;
        selectedRestaurant = drpRestaurants.SelectedValue;

        RestaurantReviewServiceClient client = new RestaurantReviewServiceClient();

        RestaurantInfo restaurant = client.GetRestaurantByName(selectedRestaurant);


        if (selectedRestaurant == "0")
        {
            pnlViewRestaurant.Visible = false;

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
        else
        {
            pnlViewRestaurant.Visible = true;

            txtAddress.Text = restaurant.Location.Street.ToString();
            txtAddress.Attributes.Add("readonly", "readonly");

            txtCity.Text = restaurant.Location.City.ToString();
            txtCity.Attributes.Add("readonly", "readonly");

            txtProvinceState.Text = restaurant.Location.Province.ToString();
            txtProvinceState.Attributes.Add("readonly", "readonly");

            txtPostalZipCode.Text = restaurant.Location.PostalCode.ToString();
            txtPostalZipCode.Attributes.Add("readonly", "readonly");

            txtSummary.Text = restaurant.Summary.ToString();
            txtSummary.Attributes.Remove("readonly");

            drpRating.Style.Remove("display");
            drpRating.SelectedValue = restaurant.Rating.ToString();
        }



  


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Save the changed restaurant restaurant data back to the service file. 

        string selectedRestaurant = null;
        selectedRestaurant = drpRestaurants.SelectedValue;

        RestaurantReviewServiceClient client = new RestaurantReviewServiceClient();
        RestaurantInfo restaurant = client.GetRestaurantByName(selectedRestaurant);

        restaurant.Summary = txtSummary.Text;
        restaurant.Rating = drpRating.SelectedValue;


        if (drpRestaurants.Text != "Select One...")
        {
            client.SaveRestaurant(restaurant);

            string confirmationString = "Changes have been made to " + restaurant.Name;
            lblConfirmation.Text = confirmationString;
            lblConfirmation.Visible = true;
        }




    }






}