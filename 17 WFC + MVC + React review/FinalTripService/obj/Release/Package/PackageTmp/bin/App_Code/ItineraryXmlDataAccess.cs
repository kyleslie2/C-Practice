using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Web;
public static class ItineraryXmlDataAccess
{
    private static Object fileLock = new Object();
    private static string[] cities = {"Toronto", "Montreal", "Calgary", "Ottawa",
                             "Edmonton", "Winnipeg", "Vancouver", "Quebec City",
                             "New York City", "Dallas", "Chicago", "Los Angeles",
                             "Houston", "Philadelphia", "Washington D.C."};
    public static List<TripItinerary> GetItinerariesFromXml()
    {
        lock (fileLock) { 
            try { 
                itineraries allItinearies = null;
                string xmlFile = HostingEnvironment.MapPath(@"~/App_Data/itineraries.xml");

                using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
                {
                    XmlSerializer serializor = new XmlSerializer(typeof(itineraries));
                    allItinearies = (itineraries)serializor.Deserialize(xs);
                }

                List<TripItinerary> itins = allItinearies.itinerary.Select<itinerariesItinerary, TripItinerary>(MapItineray).ToList();
                return itins;
            }
            catch
            {
                return new List<TripItinerary>();
            }
        }

    }

    public static string SaveItinerary(TripItinerary newItinerary)
    {
        if (newItinerary == null) return "No itinerary received!";
        
        itinerariesItinerary it = new itinerariesItinerary();

        if (string.IsNullOrWhiteSpace(newItinerary.PassengerName))
            return "Passenger name missing!";

        it.passenger = newItinerary.PassengerName;

        if (!cities.Contains(newItinerary.DepartureCity))
                return "Departure city is not valid!";

        if (!cities.Contains(newItinerary.ArrivingCity))
            return "Arriving city is not valid!";

        it.outbound = new itinerariesItineraryOutbound();
        it.outbound.departure = new itinerariesItineraryOutboundDeparture();
        it.outbound.departure.city = newItinerary.DepartureCity;
         
        it.outbound.arriving = new itinerariesItineraryOutboundArriving();
        it.outbound.arriving.city = newItinerary.ArrivingCity; ;

        if (string.IsNullOrWhiteSpace(newItinerary.Date))
            return "Trip date missing!";

        try {
            it.outbound.departure.date = DateTime.ParseExact(newItinerary.Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            it.outbound.arriving.date = it.outbound.departure.date;
        }
        catch
        {
            return "Trip date is not a valid date or is in wrong format!";
        }

        string xmlFile = HostingEnvironment.MapPath(@"~/App_Data/itineraries.xml");
        lock (fileLock)
        {
            try
            {
                itineraries allItinearies = null;
                using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
                {
                    XmlSerializer serializor = new XmlSerializer(typeof(itineraries));
                    allItinearies = (itineraries)serializor.Deserialize(xs);
                }
                List<itinerariesItinerary> itineraryList = allItinearies.itinerary.ToList();
                itineraryList.Add(it);

                allItinearies.itinerary = itineraryList.ToArray();

                using (FileStream xs = new FileStream(xmlFile, FileMode.Create))
                {
                    XmlSerializer serializor = new XmlSerializer(typeof(itineraries));
                    serializor.Serialize(xs, allItinearies);
                }
                return "New itineray has been saved.";
            }
            catch (Exception e)
            {
                return "Unable to save new itinerary: " + e.Message;
            }
        }
    }

    private static TripItinerary MapItineray(itinerariesItinerary arg)
    {
        TripItinerary itin = new TripItinerary();

        itin.PassengerName = arg.passenger;
        itin.DepartureCity = arg.outbound.departure.city;
        itin.ArrivingCity = arg.outbound.arriving.city;
        itin.Date = arg.outbound.departure.date.ToString("yyyy-MM-dd");
        return itin;
    }
}
