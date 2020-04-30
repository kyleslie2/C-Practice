using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Serialization;


public partial class Itineraries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string xmlFile = MapPath(@"~/App_Data/itineraries.xml");
		lblConfirmation.Visible = false;

        //Add your code to deserialize the xml file and initialize the dropdown list.
     
        if (!IsPostBack) //
        {
            //Deserialize the XML (in each step)
            itineraries allItineraries = null;
            using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
            {
                XmlSerializer serial = new XmlSerializer(typeof(itineraries));
                allItineraries = (itineraries)serial.Deserialize(xs);
            }

            drpPassenger.DataSource = allItineraries.itinerary;
            drpPassenger.DataTextField = "passenger";
            drpPassenger.DataBind();
            drpPassenger.Items.Insert(0, new ListItem("Select One...", "0"));
        }
       
    
    }

    protected void drpPassenger_SelectedIndexChanged(object sender, EventArgs e)
    {
        string xmlFile = MapPath(@"~/App_Data/itineraries.xml");

        //Add your code to handle the event when the user selects a different passenger

        //Deserialize the XML (in each step)
        itineraries allItineraries = null;
        using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
        {
            XmlSerializer serial = new XmlSerializer(typeof(itineraries));
            allItineraries = (itineraries)serial.Deserialize(xs);
        }

        //Remove all fields when index 0 is selected
        if (drpPassenger.SelectedValue == "0")
        {
            txtOutboundDeparture.Visible = false;
            txtOutboundArriving.Visible = false;
            txtInboundDeparture.Visible = false;
            txtInboundArriving.Visible = false;
            return;
        }
        else
        {
            //fill in relevant fields with data
            txtOutboundDeparture.Visible = true;
            txtOutboundArriving.Visible = true;
            txtInboundDeparture.Visible = true;
            txtInboundArriving.Visible = true;
   
            itinerariesItinerary info = allItineraries.itinerary[drpPassenger.SelectedIndex - 1];
            txtOutboundDeparture.Text = info.outbound.departure.city;
            txtOutboundArriving.Text = info.outbound.arriving.city;
            txtInboundDeparture.Text = info.inbound.departure.city;
            txtInboundArriving.Text = info.inbound.arriving.city;
            drpPassenger.SelectedValue = info.passenger.ToString();
        }

    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string xmlFile = MapPath(@"~/App_Data/itineraries.xml");
        
        lblConfirmation.Visible = true;
        lblConfirmation.Text = "Revised Itinerary has been saved to <br/>" + xmlFile;

        //Deserialize the XML (in each step)
        itineraries allItineraries = null;
        using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
        {
            XmlSerializer serial = new XmlSerializer(typeof(itineraries));
            allItineraries = (itineraries)serial.Deserialize(xs);
        }

        //If a passenger is presently selected
        if (drpPassenger.SelectedValue != "0") 
        {
            itinerariesItinerary info = allItineraries.itinerary[drpPassenger.SelectedIndex - 1];

            info.outbound.departure.city = txtOutboundDeparture.Text;
            info.outbound.arriving.city = txtOutboundArriving.Text;
            info.inbound.departure.city = txtInboundDeparture.Text;
            info.inbound.arriving.city = txtInboundArriving.Text;

         
            // serializing the xml to write changes
            using (FileStream xs = new FileStream(xmlFile, FileMode.Create))
            {
                XmlSerializer serializor = new XmlSerializer(typeof(itineraries));
                serializor.Serialize(xs, allItineraries);
            }

            //Add confirmation message
            lblConfirmation.Visible = true;
            lblConfirmation.Text = "Revised Itinerary has been saved to: <br/>" + xmlFile;
        }


    }    
}