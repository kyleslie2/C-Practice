var NewItinerary = React.createClass({

    getInitialState: function () {
        return {
            newItinerary: {},
            Confirmation: "",
            ShowConfirmation: { display: "none" }
        };
    },
    handleChange: function(itinerary) {
        this.setState({
            newItinerary: itinerary,
        });
    },
    saveNewItinerary: function () {

        //Implement Ajax request to the Web Application to save the new itinerary 
        //and show comfirmation received from the Web Application.

             //Add your code here

        //NOTE:

        //would grab itinerary data from newItinerary component
        // grab all 4 fields and have ITINERARY object passed via ajax. then add those variables to the newItinerary object in the SaveItinerary function
        // then would display success message using react component
        console.log('this ...')
        console.log(this);
        $.ajax("/Home/SaveItinerary",
            {
                type: "POST",
                dataType: "json",
                data: Itinerary,
                success: function (returnData) {
                    if (returnData != null) {
                        this.setState({ itineraries: returnData });
                    }
                }.bind(this),
                error: function (jqRequest, status, e) {
                    alert("Status: " + status + "    Error: " + e);
                }
            });


    },
    render: function () {
        return (<div>
                    <div className="row">
                        <div className="col-md-12"><h1>New Itinerary</h1></div>
                    </div>
                    <div className="row">
                        <div className="col-md-12">

                    {/* Display Itinerary component and pass in event handler */} 

                    <div id='content'>
                        <Itinerary/>
                        </div>

                    {/* Add your code here */}
                            
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-1">
                            <button className="btn btn-primary" type="button" onClick={this.saveNewItinerary}>Save</button>
                        </div>
                        <div className="col-md-11">
                            <span className="form-control alert-success" style={this.state.ShowConfirmation }>{this.state.Confirmation}</span>
                        </div>
                    </div>
                </div>);
    }
});

ReactDOM.render((<div>

    {/* Display Itineraries component and NewItinerary component */}



    <Itineraries />

    <NewItinerary />


    {/* Add your code here */}
      


                </div>), document.getElementById("content"));
