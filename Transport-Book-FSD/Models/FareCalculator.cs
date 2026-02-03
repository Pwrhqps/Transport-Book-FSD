using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;

namespace TransportBookFSD.Models
// FareCalculator estimates the ride fare based on distance and time
{
    // Calculates an estimated ride fare based on pickup/dropoff text + pickup time
    // (simple rule-based calculator, not real-world pricing)
    public static class FareCalculator
    {
        // Rough coordinates (lat, lon) for common areas in Singapore
        // Used to estimate distance when users type place names
        private static readonly Dictionary<string, (double lat, double lon)> Areas = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Bedok"] = (1.3236, 103.9273),
            ["Jurong"] = (1.3329, 103.7436),
            ["Tampines"] = (1.3547, 103.9437),
            ["Woodlands"] = (1.4382, 103.7890),
            ["Yishun"] = (1.4304, 103.8354),
            ["Ang Mo Kio"] = (1.3691, 103.8454),
            ["Bishan"] = (1.3508, 103.8485),
            ["Toa Payoh"] = (1.3327, 103.8476),
            ["Clementi"] = (1.3151, 103.7649),
            ["Bukit Batok"] = (1.3491, 103.7496),
            ["Sengkang"] = (1.3868, 103.8914),
            ["Punggol"] = (1.4053, 103.9023),
            ["Pasir Ris"] = (1.3731, 103.9493),
            ["Changi"] = (1.3644, 103.9915),
            ["City"] = (1.2868, 103.8536) // CBD-ish
        };

        public static decimal CalculateFare(string pickup, string dropoff, DateTime pickupDateTime)
        {
            // Base settings (tweakable)
            decimal baseFare = 4.50m;
            decimal perKm = 1.00m;

            // Estimate distance using area coordinates
            double km = EstimateDistanceKm(pickup, dropoff);

            // If location not recognized, assume a default distance
            if (km <= 0.1) km = 8.0;

            decimal fare = baseFare + (decimal)km * perKm;

            // Peak hour multiplier
            // Morning peak: 7-9, Evening peak: 17-20
            int hour = pickupDateTime.Hour;
            bool isPeak = (hour >= 7 && hour < 10) || (hour >= 17 && hour < 21);
            if (isPeak) fare *= 1.25m;

            // Weekend surcharge (substitute for public holiday)
            bool isWeekend = pickupDateTime.DayOfWeek == DayOfWeek.Saturday ||
                             pickupDateTime.DayOfWeek == DayOfWeek.Sunday;
            if (isWeekend) fare *= 1.10m;

            // Round to 2 dp
            return Math.Round(fare, 2, MidpointRounding.AwayFromZero);
        }

        // Converts pickup and dropoff names into coordinates and calculates distance between them
        private static double EstimateDistanceKm(string pickup, string dropoff)
        {
            if (!TryGetPoint(pickup, out var p1)) return 0;
            if (!TryGetPoint(dropoff, out var p2)) return 0;

            // Calculate distance using Haversine formula
            return HaversineKm(p1.lat, p1.lon, p2.lat, p2.lon);
        }

        private static bool TryGetPoint(string input, out (double lat, double lon) point)
        {
            input = (input ?? "").Trim();

            // Try exact match first (e.g. "Bedok")
            if (Areas.TryGetValue(input, out point)) return true;

            // Try contains match (e.g. "Bedok Reservoir" contains "Bedok")
            foreach (var kv in Areas)
            {
                if (input.Contains(kv.Key, StringComparison.OrdinalIgnoreCase))
                {
                    point = kv.Value;
                    return true;
                }
            }

            // Not recognized
            point = default;
            return false;
        }

        private static double HaversineKm(double lat1, double lon1, double lat2, double lon2)
        {
            // Calculated Earth (great-circle) distance because pickup and dropoff locations
            // are given as latitude and longitude on a curved Earth, not on a flat map.
            const double R = 6371; // Earth radius in km
            double dLat = ToRad(lat2 - lat1);
            double dLon = ToRad(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        // Convert degrees to radians
        private static double ToRad(double deg) => deg * (Math.PI / 180.0);
    }
}
