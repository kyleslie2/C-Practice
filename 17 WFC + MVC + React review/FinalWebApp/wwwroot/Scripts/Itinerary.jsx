var Itinerary = React.createClass({
    getInitialState: function () {
        if (this.props.itinerary != undefined) {
            return {
                destinations: [],
                name: this.props.itinerary.passengerName,
                departure: this.props.itinerary.departure,
                arriving: this.props.itinerary.arriving,
                date: this.props.itinerary.date
            };
        }
        else {
            return {
                destinations: [],
                name: "",
                departure: "",
                arriving: "",
                date: ""
            };
        }
    },
    handleChange: function (e) {
        if (e.target.id == "drpDeparture")
            this.setState({ departure: e.target.value }, this.bubleItineraryChange);
        else if (e.target.id == "drpArriving")
            this.setState({ arriving: e.target.value }, this.bubleItineraryChange);
        else if (e.target.id == "txtDate")
            this.setState({ date: e.target.value }, this.bubleItineraryChange);
        else if (e.target.id == "txtName")
            this.setState({ name: e.target.value }, this.bubleItineraryChange);
    },

    bubleItineraryChange: function () {
        if (this.state.name != "" && this.state.departure != ""
            && this.state.arriving && this.state.date != "") {
            var itinerary = {
                PassengerName: this.state.name, Departure: this.state.departure,
                Arriving: this.state.arriving, Date: this.state.date
            }
            this.props.onItinearyChange(itinerary);
        }
    },

    componentDidMount: function () {
        $.ajax("/Home/GetAllDetinations",
            {
                type: "GET",
                dataType: "json",
                success: function (returnData) {
                    if (returnData != null) {
                        this.setState({ destinations: returnData });
                    }
                }.bind(this),
            });
    },
    render: function () {
        function makeDetinationOptions(x) {
            return <option key={x} value={x}>{x}</option>;
        }
        return (
            <fieldset>
                <div className="row">
                    <div className="col-md-2 label-align"><label htmlFor="txtName">Name:</label> </div>
                    <div className="col-md-4">
                        <input type="text" id="txtName" className="form-control" style={{ width: "100%" }}
                            onChange={this.handleChange} onBlur={this.handleChange} value={this.state.name} disabled={this.props.disabled} placeholder="Passenger Name" />
                    </div>
                    <div className="col-md-2 label-align"><label>Date:</label> </div>
                    <div className="col-md-4">
                        <input type="date" id="txtDate" className="form-control" style={{ width: "100%" }}
                            onChange={this.handleChange} value={this.state.date}
                            disabled={this.props.disabled} placeholder="YYYY-MM-DD" />
                    </div>
                </div>
                <div className="row">
                    <div className="col-md-2 label-align"><label htmlFor="drpDeparture">Departure:</label> </div>
                    <div className="col-md-4">
                        <select id="drpDeparture" className="form-control" style={{ width: "100%" }}
                            onChange={this.handleChange} value={this.state.departure} disabled={this.props.disabled}>
                            {this.state.destinations.map(makeDetinationOptions)}
                        </select>
                    </div>
                    <div className="col-md-2 label-align"><label htmlFor="drpArriving">Arriving:</label> </div>
                    <div className="col-md-4">
                        <select id="drpArriving" className="form-control" style={{ width: "100%" }}
                            onChange={this.handleChange} value={this.state.arriving} disabled={this.props.disabled}>
                            {this.state.destinations.map(makeDetinationOptions)}
                        </select>
                    </div>
                </div>
            </fieldset>
        );
    }
});