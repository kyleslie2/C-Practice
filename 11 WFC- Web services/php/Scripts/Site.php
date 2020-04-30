<?php
//Receive and process the ajax calls from Site.js

$appConfigs = parse_ini_file("Lab6.ini");
$wsdl = $appConfigs["restaurantReviewServiceWsdl"];
$reviewClient = new SoapClient($wsdl);

$ajax_var = $_GET['fetch']; 

switch ($ajax_var) {
    case "names":
        get_names($reviewClient);
        break;
    case "restaurant":
        $selected_restaurant = $_GET['restaurant_name'];
        get_restaurant($selected_restaurant, $reviewClient);
        break;
    case "update":
        $restaurant_details = $_GET['restaurant_object'];
        update_xml($restaurant_details, $reviewClient);
        break;
}


function get_names($reviewClient){
    // $wsdl = "http://localhost/OnlineService/RestaurantReviewService.svc?wsdl";

    $restaurantName = $reviewClient->GetRestaurantNames();
    $restaurantName = $restaurantName->GetRestaurantNamesResult->string;
    echo json_encode($restaurantName);
}


function get_restaurant($selected_restaurant, $reviewClient){ 
    // $wsdl = "http://localhost/OnlineService/RestaurantReviewService.svc?wsdl";
    // $reviewClient = new SoapClient($wsdl);
    $restaurantList = [];
    $parameters = new stdClass;
    $parameters->name = $selected_restaurant;
    $restaurantInfo = $reviewClient->GetRestaurantByName($parameters);
    $restaurantInfo = $restaurantInfo->GetRestaurantByNameResult;
    $restaurant = array(
        $restaurantInfo->Name => array(
            "name" => $restaurantInfo->Name,
            "streetAddress" => $restaurantInfo->Location->Street,
            "city" => $restaurantInfo->Location->City,
            "provinceState" => $restaurantInfo->Location->Province,
            "postalZipCode" => $restaurantInfo->Location->PostalCode,
            "summary" => $restaurantInfo->Summary,
            "rating" => $restaurantInfo->Rating,
    ));
    echo json_encode($restaurant);
}

function update_xml($restaurantDetails, $reviewClient){
    // $wsdl = "http://localhost/OnlineService/RestaurantReviewService.svc?wsdl";
    // $reviewClient = new SoapClient($wsdl);
    $parameters = new stdClass;
    $rest = new stdClass;
    $rest->Location = new stdClass;

    $rest->Name = $restaurantDetails["name"];
    $rest->Rating = $restaurantDetails["rating"];
    $rest->Summary = $restaurantDetails["summary"];
    $rest->Location->City = $restaurantDetails["city"];
    $rest->Location->PostalCode = $restaurantDetails["postalZipCode"];
    $rest->Location->Province = $restaurantDetails["provinceState"];
    $rest->Location->Street = $restaurantDetails["streetAddress"];
    $parameters->restInfo = $rest;
    $saveUpdates = $reviewClient->SaveRestaurant($parameters);
    echo "Successfully updated data for ".$restaurantDetails["name"]. "!";

}


?>