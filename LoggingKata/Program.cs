﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Reflection.Emit;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line

            // Create a new instance of your TacoParser class

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            //logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            //logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable pointA = null;
            ITrackable pointB = null;
            ITrackable tacoBell_A = null;
            ITrackable tacoBell_B = null;
            double furthestDistance = 0;
            double distance = 0;
            double miles = 0;
            

            for (int i = 0; i < locations.Length; i++)
            {
                tacoBell_A = locations[i];
                double latitudeA = tacoBell_A.Location.Latitude;
                double longitudeA = tacoBell_A.Location.Longitude;
                var corA = new GeoCoordinate(latitudeA, longitudeA);
                for (int j = 0; j < locations.Length; j++)
                {
                    tacoBell_B = locations[j];
                    double latitudeB = tacoBell_B.Location.Latitude;
                    double longitudeB = tacoBell_B.Location.Longitude;
                    var corB = new GeoCoordinate(latitudeB, longitudeB);
                    distance = corA.GetDistanceTo(corB);
                    if (furthestDistance < distance)
                    {
                        pointA = tacoBell_A;
                        pointB = tacoBell_B;
                        furthestDistance = distance;
                    }                                                       

                }

            }
            miles = DataConversion.ConvertMetersToMiles(furthestDistance);
            Console.WriteLine($"{pointA.Name} and {pointB.Name} are the furthest away from " +
                $"each other at {Math.Round(furthestDistance, 2)} meters or {Math.Round(miles, 2)} miles apart.");
           
           
        }
    }
}
