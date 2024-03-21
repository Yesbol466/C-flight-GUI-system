using System;
using System.Collections.Generic;

namespace OOD_first_project
{
    public static class FlightDataConverter
    {
        public static FlightGUI ConvertToFlightGUI(Flight flight, Dictionary<ulong, AirPort> airports)
        {
            var origin = airports[flight.OriginID];
            var destination = airports[flight.TargetID];
            var currentPosition = CalculateCurrentPosition(origin, destination, flight.TakeoffTime, flight.LandingTime);
            var rotation = CalculateRotation(currentPosition, origin, destination);

            return new FlightGUI
            {
                ID = flight.ID,
                WorldPosition = new WorldPosition { Longitude = currentPosition.longitude, Latitude = currentPosition.latitude },
                MapCoordRotation = rotation
            };
        }


        private static (double longitude, double latitude) CalculateCurrentPosition(AirPort origin, AirPort destination, string takeoffTime, string landingTime)
        {
            DateTime takeoff = DateTime.Parse(takeoffTime);
            DateTime landing = DateTime.Parse(landingTime);
            DateTime now = DateTime.UtcNow; 

            if (now < takeoff) return (origin.Longitude, origin.Latitude);
            if (now > landing) return (destination.Longitude, destination.Latitude);

            double totalFlightDuration = (landing - takeoff).TotalSeconds;
            double elapsedFlightDuration = (now - takeoff).TotalSeconds;
            double progress = elapsedFlightDuration / totalFlightDuration;

            double currentLongitude = origin.Longitude + (destination.Longitude - origin.Longitude) * progress;
            double currentLatitude = origin.Latitude + (destination.Latitude - origin.Latitude) * progress;

            return (currentLongitude, currentLatitude);
        }

        private static double CalculateRotation((double longitude, double latitude) currentPosition, AirPort origin, AirPort destination)
        {
            double deltaY = destination.Latitude - origin.Latitude;
            double deltaX = destination.Longitude - origin.Longitude;
           
            double angleInRadians = Math.Atan2(deltaX, deltaY);
            return angleInRadians;
        }

    }
}
