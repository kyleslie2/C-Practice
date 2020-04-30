using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Text;
using System.Xml.Serialization;


namespace Lab6Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestaurantReviewService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestaurantReviewService.svc or RestaurantReviewService.svc.cs at the Solution Explorer and start debugging.
    public class RestaurantReviewService : IRestaurantReviewService
    {
        public List<string> GetRestaurantNames()
        {
            List<string> names = new List<string>();
            restaurants all_restaurants = GetRestaurantsFromXml();
            if (all_restaurants != null)
            {
                foreach (restaurant restaurant in all_restaurants.restaurant)
                {
                    names.Add(restaurant.name.ToString());
                }
            }
            return names;
        }
        public restaurants GetRestaurantsFromXml()
        {
            //function for deserializing xml
            string xmlFile = HttpContext.Current.Server.MapPath(@"App_Data/restaurant_reviews.xml");
            restaurants restaurantList = null;
            //Opening XML file and serializing into classes
            using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(restaurants));
                restaurantList = (restaurants)serializer.Deserialize(xs);
            }
            return restaurantList;
        }
        public RestaurantInfo GetRestaurantByName(string name)
        {
            restaurants all_restaurants = GetRestaurantsFromXml();
            if (all_restaurants != null)
            {
                foreach (restaurant restaurant in all_restaurants.restaurant)
                {
                    if (restaurant.name == name)
                    {
                        var restaurant_info = new RestaurantInfo
                        {
                            Name = restaurant.name,
                            Rating = restaurant.rating.ToString(),
                            Summary = restaurant.summary,
                            Location = new Address
                            {
                                Street = restaurant.location.street,
                                City = restaurant.location.city,
                                Province = restaurant.location.provstate,
                                PostalCode = restaurant.location.postalzipcode
                            }
                        };
                        return restaurant_info;
                    }
                }
            }
            // return empty RestaurantInfo is name not found
            return new RestaurantInfo();
        }
        public List<RestaurantInfo> GetRestaurantsByRating(int rating)
        {
            restaurants all_restaurants = GetRestaurantsFromXml();
            List<RestaurantInfo> restaurant_info = new List<RestaurantInfo>();
            if (all_restaurants != null)
            {
                foreach (restaurant restaurant in all_restaurants.restaurant)
                {
                    if (restaurant.rating >= rating)
                    {
                        RestaurantInfo info = new RestaurantInfo
                        {
                            Name = restaurant.name,
                            Rating = restaurant.rating.ToString(),
                            Summary = restaurant.summary,
                            Location = new Address
                            {
                                Street = restaurant.location.street,
                                City = restaurant.location.city,
                                Province = restaurant.location.provstate,
                                PostalCode = restaurant.location.postalzipcode
                            }
                        };
                        restaurant_info.Add(info);
                    }
                }
                return restaurant_info;
            }
            // return empty RestaurantInfo is name not found
            return restaurant_info;
        }
        public bool SaveRestaurant(RestaurantInfo restaurant_info)
        {
            restaurants all_restaurants = GetRestaurantsFromXml();
            restaurant selected_restaurant;
            foreach (restaurant rest in all_restaurants.restaurant)
            {
                if (rest.name == restaurant_info.Name)
                {
                    selected_restaurant = rest;
                    string xmlFile = HttpContext.Current.Server.MapPath(@"App_Data/restaurant_reviews.xml");
                    selected_restaurant.summary = restaurant_info.Summary;
                    selected_restaurant.rating = int.Parse(restaurant_info.Rating);
                    selected_restaurant.location.street = restaurant_info.Location.Street;
                    selected_restaurant.location.postalzipcode = restaurant_info.Location.PostalCode;
                    selected_restaurant.location.provstate = restaurant_info.Location.Province;
                    selected_restaurant.location.city = restaurant_info.Location.City;
                    using (FileStream xs = new FileStream(xmlFile, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(restaurants));
                        serializer.Serialize(xs, all_restaurants);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
