using System.Linq;
using System.Collections.Generic;

//Implement the interface you added in ITripService.cs file 
//to provide the services as required by the test sheet.

    //Add your code here

public class TripService : ITripService
{
    public List<string> GetAllDestinations() 
    {
        return DataAccessHelper.GetAllDetinations();
    }

    public List<TripItinerary> GetAllTripItineraries()
    {
        return DataAccessHelper.GetAllItineraries();
    }

    public string SaveTripItinerary(TripItinerary itinerary)
    {
        return DataAccessHelper.SaveItinerary(itinerary);
    }
}
     
