using Data.Models.Strava;
using DataManagement.Models;

namespace DataManagement.BusinessLogic
{
    public class HeatmapManager
    {
        public static HeatMapData ProcessHeatmapData(List<DetailedActivity> activities)
        {
            HeatMapData data = GetHeatmapData(activities);

            return data;
        }

        public static HeatMapData GetHeatmapData(List<DetailedActivity> activities)
        {
            HeatMapData data = new HeatMapData();
            data.Input = new List<HeatmapInput>();
            data.Count = activities.Count;
            foreach (DetailedActivity activity in activities)
            {
                HeatmapInput input = new HeatmapInput();
                input.ActivityType = activity.Type;
                input.Polyline = activity.Polyline;
                input.StartPoint = activity.StartLatlng;
                input.EndPoint = activity.EndLatlng;
                input.StartTime = activity.StartDateLocal;
                input = DetermineStartLocation(input);
                data.Input.Add(input);
            }
            return data;
        }

        public static HeatmapInput DetermineStartLocation(HeatmapInput input)
        {
            double[] amaliaStraat = { 52.09262715438605, 5.092068072807608 };
            double[] rooseveltLaan = { 52.071870429353034, 5.08918803820036 };
            double[] molukkenStraat = { 52.07665919642143, 5.117474360648716};

            if (input.StartPoint != null && input.StartPoint.Count > 0 && input.StartPoint[0] != null && input.StartPoint[1] != null)
            {
                double[] startPoint = { (double)input.StartPoint[0], (double)input.StartPoint[1] };

                double amaliaDistance = Math.Pow(amaliaStraat[0] - startPoint[0], 2) + Math.Pow(amaliaStraat[1] - startPoint[1], 2);
                double rooseveltDistance = Math.Pow(rooseveltLaan[0] - startPoint[0], 2) + Math.Pow(rooseveltLaan[1] - startPoint[1], 2);
                double molukkenDistance = Math.Pow(molukkenStraat[0] - startPoint[0], 2) + Math.Pow(molukkenStraat[1] - startPoint[1], 2);

                double smallestDistance = Math.Min(molukkenDistance, Math.Min(rooseveltDistance, amaliaDistance));

                if ( smallestDistance > 10)
                {
                    input.StartLocation = "Other";
                    return input;
                }

                if (smallestDistance == molukkenDistance)
                {
                    input.StartLocation = "Molukkenstraat";
                    return input;
                }
                else if (smallestDistance == rooseveltDistance)
                {
                    input.StartLocation = "Rooseveltlaan";
                    return input;
                }
                else
                {
                    input.StartLocation = "Amaliastraat";
                    return input;
                }
            }
            else
            {
                input.StartLocation = "Unknown";
                return input;
            }
        }
    }
}
