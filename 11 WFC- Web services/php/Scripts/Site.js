/* Add event handler to HTML document ready event to perform the following tasks 

1. Populate the restaurant name dropdown list.
   Make an ajax call to the php script Site.php to get the names of the restaurants.
   
2. Attach the event handler to handle the restaurant name dropdown list's selection changed event.
   Make an ajax call to the php script Site.php to get the selected restaurant data 
   
3. Attach the event handler to handle the Save button clicked event.
   Make an ajax call to the php script Site.php to save the user modified restaurant data    
*/

$(document).ready(function () {
   var dropdown = $('#drpRestaurant')
   if (dropdown.find(":selected").val() == -1) {
      $("#restaurant-info").hide();
   }
   
   //ajax request for names
   ajax_get_names();

   dropdown.on("change", function (e) {
      var selectedValue = $(this).find(":selected").val();
      // console.log('selectedValue', selectedValue)

      if (selectedValue != -1) {
         $("#restaurant-info").show();
         $("#lblConfirmation").hide();
         var selectedText = $(this).find(":selected").text();
         ajax_get_restaurant(selectedText);
      }
      if (selectedValue == -1){
         $("#restaurant-info").hide();
      }
   });


   function ajax_get_names(){
      // console.log('getting names!')
      //ajax request for names
      var fetch = "names";
      $.ajax({
         type: "GET",
         url: "Scripts/Site.php",
         data: { fetch },
         dataType: "json",
         success: function (data) {
            if (data) {
            //run function to populate page with array of restaurant names data
               populate_dropdown(data);
            } else {
               //No match so we will clear controls
            console.log('no return!')
            }
         },
         error: function (event, request, settings) {
            console.log('AjaxError' + ' : ' + settings);
         },
         timeout: 20000
      });
   }


   function ajax_get_restaurant(restaurant_name) {

      var fetch = "restaurant";

      //ajax request for specific restaurant
      $.ajax({
         type: "GET",
         url: "Scripts/Site.php",
         data: {fetch, restaurant_name},
         dataType: "json",
         success: function (data) {
            if (data) {
               populate_fields(data[restaurant_name]);
            } else {
               //No match so we will clear controls
               console.log('no return!')
            }
         },
         error: function (event, request, settings) {
            console.log('AjaxError' + ' : ' + settings);
         },
         timeout: 20000
      });
   }

   function populate_dropdown(name_array){

      //create options for name dropdown
      $.each(name_array, function (index, value) {
         var option = document.createElement('option');
         option.text = value;
         option.value = index;
         dropdown.append(option);
      });

      //create options for rating dropdown
      for (i=0;i<6;i++){
         var rating_option = document.createElement('option');
         if (i === 0){
            rating_option.text = "";
            rating_option.value = i;
         }else {
            rating_option.text = rating_option.value = i;
         }
         $('#drpRating').append(rating_option);
      }
   }

  function populate_fields(restaurant_object){

      $('#txtStreetAddress').val(restaurant_object.streetAddress);
      $('#txtCity').val(restaurant_object.city);
      $('#txtProvinceState').val(restaurant_object.provinceState);
      $('#txtPostalZipCode').val(restaurant_object.postalZipCode);
      $('#txtSummary').val(restaurant_object.summary);
      $('#drpRating').val(restaurant_object.rating);
   };


   $("#btnSave").on("click", function() {
      var selectedRestaurant = $("#drpRestaurant").find(":selected").text().trim();
      var txtStreetAddress = $('#txtStreetAddress').val().trim();
      var txtCity = $('#txtCity').val().trim();
      var txtProvinceState = $('#txtProvinceState').val().trim();
      var txtPostalZipCode = $('#txtPostalZipCode').val().trim();
      var txtSummary = $('#txtSummary').val().trim();
      var drpRating = $('#drpRating').val().trim();

      var updated_restaurant_object= {
         name: selectedRestaurant,
         streetAddress: txtStreetAddress,
         city: txtCity,
         provinceState: txtProvinceState,
         postalZipCode: txtPostalZipCode,
         summary: txtSummary,
         rating: drpRating
      }

      //run ajax function to update xml
      ajax_update_xml(updated_restaurant_object);

   });

   function ajax_update_xml(restaurant_object){
      var fetch = "update";

      $.ajax({
         type: "GET",
         url: "Scripts/Site.php",
         data: {
            fetch,
            restaurant_object
         },
         success: function (data) {
            if (data) {
               populate_confirmation(data);
            } else {
               //No match so we will clear controls
               console.log('no return!')
            }
         },
         error: function (event, request, settings) {
            console.log('AjaxError' + ' : ' + settings);
         },
         timeout: 20000
      });

   }
   function populate_confirmation(message){
      $("#lblConfirmation").text(message).show();
   }
});
