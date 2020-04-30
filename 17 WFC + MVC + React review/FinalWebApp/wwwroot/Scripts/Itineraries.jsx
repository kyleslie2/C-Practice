var Itineraries = React.createClass({

    getInitialState: function () {
        return {
            itineraries: [],
        };
    },

    componentDidMount: function () {
        $.ajax("/Home/GetAllItinearies",
            {
                type: "GET",
                dataType: "json",
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
        function makeItineraryList(x) {
            var key = Math.floor(Math.random() * 10000);
            return <Itinerary itinerary={x} key={key} disabled="disabled" />;
        }
        return (
            <div>
                <div style={{ height: 400, overflowY: "scroll", borderStyle: "double", borderColor: "green", marginTop: 10 }}>
                    <h1>Itinearies</h1>
                    {this.state.itineraries.map(makeItineraryList, this)}
                </div>

            </div>
        );
    }
});