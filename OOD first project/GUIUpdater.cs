using FlightTrackerGUI;
using System.Timers;

namespace OOD_first_project
{
    public static class GUIUpdater
    {
        private static string filePath = "example_data.ftr"; 

        public static void UpdateGUIPeriodically()
        {
            var allData = new FileReader().ReadFromFile("example_data.ftr");
            var airports = LoadAirports(allData);
            var flights = LoadFlights(allData);

            while (true)
            {
                FlightsGUIData flightsGUIData = new FlightsGUIData();
                var list = new List<FlightGUI>();
                foreach (var flight in flights)
                {
                    if (DateTime.UtcNow >= DateTime.Parse(flight.LandingTime))
                        continue;
                    if (DateTime.UtcNow < DateTime.Parse(flight.TakeoffTime))
                        continue;
                    var flightGUI = FlightDataConverter.ConvertToFlightGUI(flight, airports);
                    const double positionChangeThreshold = 0.01;
                    if (Math.Abs(flightGUI.WorldPosition.Longitude - flight.Longitude) < positionChangeThreshold &&
                        Math.Abs(flightGUI.WorldPosition.Latitude - flight.Latitute) < positionChangeThreshold)
                        continue;
                    list.Add(flightGUI);
                }
                flightsGUIData.UpdateFlights(list);
                Runner.UpdateGUI(flightsGUIData);
                
                Thread.Sleep(1000);
            }
        }
       




        private static List<Flight> LoadFlights(List<Data> datas)
        {

            var flights = new List<Flight>();
            foreach (var data in datas)
            {
                if (data is Flight flight)
                {
                    flights.Add(flight);
                }
            }
            return flights;
        }

        public static Dictionary<ulong, AirPort> LoadAirports(List<Data> datas)
        {

            var airports = new Dictionary<ulong, AirPort>();
            foreach (var data in datas)
            {
                if (data is AirPort airport)
                {
                    airports[airport.ID] = airport;
                }
            }
            return airports;
        }
    }
}
