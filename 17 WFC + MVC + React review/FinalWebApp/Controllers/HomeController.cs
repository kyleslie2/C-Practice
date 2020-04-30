using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Final.Models;
using TripService;
using System.Web;

namespace Final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllDetinations()
        {
            //Get an array of destination cities from the WCF Web Service TripService 

            //Add your code here.
            TripServiceClient client = new TripServiceClient();
            string[] destinations = client.GetAllDestinations();



            //Return the destination cities to the browser as a JSON string 
            return Json(destinations);
        }

     

        public ActionResult GetAllItinearies()
        {
   
            TripServiceClient client = new TripServiceClient();
            TripItinerary[] trips = client.GetAllTripItineraries();

          
            List<Itinerary> itineraries = new List<Itinerary>(new Itinerary[trips.Length]);


            for (int i = 0; i < trips.Length; i++)
            {

                itineraries[i] = new Models.Itinerary();
                itineraries[i].Date = trips[i].Date;
                itineraries[i].Arriving = trips[i].ArrivingCity;
                itineraries[i].Departure = trips[i].DepartureCity;
                itineraries[i].PassengerName = trips[i].PassengerName;
            }
            return Json(itineraries);
        }

        [HttpPost]
        public ActionResult SaveItinerary(string newItinerary)
        {
            //1. Decode the received JSON string newItinerary to get an Itinerary object.
            //2. Use the result to populate a TripItinerary object as defined by the web service's data contract
            //3. Save the TripItinerary object to the WCF Web Service TripService. 

            //Add your code here\
            TripServiceClient client = new TripServiceClient();
            Console.WriteLine(newItinerary);

            //Models.Itinerary itineraryInfo = JsonConvert.DeserializeObject<Models.Itinerary>(newItinerary);

            if (!string.IsNullOrWhiteSpace(newItinerary))
            {
                Models.Itinerary itineraryInfo =
                JsonConvert.DeserializeObject<Models.Itinerary>(newItinerary);

                TripItinerary trip = new TripItinerary();
                trip.PassengerName = itineraryInfo.PassengerName;
                trip.Date = itineraryInfo.Date;
                trip.DepartureCity = itineraryInfo.Departure;
                trip.ArrivingCity = itineraryInfo.Arriving;


                //return the save result received from WCF Web Service TripService to the browser as a JSON string.

                return Json(client.SaveTripItinerary(trip)); //replace null with the JSON string!!
            }
            else
            {
                return null;

            }

        }

    }
}