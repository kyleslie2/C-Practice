using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

//Define an interface for the services as required by the test sheet:

//Add your code here
[ServiceContract]
public interface ITripService
{

    [OperationContract]
    List<string> GetAllDestinations();

    [OperationContract]
    List<TripItinerary> GetAllTripItineraries();

    [OperationContract]
    string SaveTripItinerary(TripItinerary ti);
}

[DataContract]
public class TripItinerary
{
    [DataMember]
    public string PassengerName { get; set; }
    [DataMember]
    public string Date { get; set; }  //in YYYY-MM-DD
    [DataMember]
    public string DepartureCity { get; set; }
    [DataMember]
    public string ArrivingCity { get; set; }
}
