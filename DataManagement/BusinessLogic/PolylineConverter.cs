using System.Text;
using DataManagement.Models;

namespace DataManagement.BusinessLogic
{
    public class PolylineConverter
    {
        // Decode Google Polyline encoded string to list of points
        public static List<Point> Decode(string encodedPolyline)
        {
            if (string.IsNullOrEmpty(encodedPolyline))
            {
                return [];
            }

            List<Point> points = [];
            int index = 0;
            int lat = 0;
            int lng = 0;

            while (index < encodedPolyline.Length)
            {
                // Decode latitude
                int result = 0;
                int shift = 0;
                int b;

                do
                {
                    b = encodedPolyline[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;

                // Decode longitude
                result = 0;
                shift = 0;

                do
                {
                    b = encodedPolyline[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;

                // Convert to decimal degrees (the encoding uses 1e5 precision)
                double latitude = lat / 1e5;
                double longitude = lng / 1e5;

                points.Add(new Point(longitude, latitude));
            }

            return points;
        }

        // Encode list of points to Google Polyline format
        public static string Encode(List<Point> points, int precision = 5)
        {
            if (points == null || points.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new();
            int prevLat = 0;
            int prevLng = 0;

            double factor = Math.Pow(10, precision);

            foreach (Point point in points)
            {
                // Note: Point.X is longitude, Point.Y is latitude
                int lat = (int)Math.Round(point.Y * factor);
                int lng = (int)Math.Round(point.X * factor);

                EncodeValue(lat - prevLat, result);
                EncodeValue(lng - prevLng, result);

                prevLat = lat;
                prevLng = lng;
            }

            return result.ToString();
        }

        private static void EncodeValue(int value, StringBuilder result)
        {
            int sgn_num = value << 1;
            if (value < 0)
            {
                sgn_num = ~sgn_num;
            }

            while (sgn_num >= 0x20)
            {
                int next_chunk = (0x20 | (sgn_num & 0x1f)) + 63;
                result.Append((char)next_chunk);
                sgn_num >>= 5;
            }

            result.Append((char)(sgn_num + 63));
        }

        // Decode and convert to lat/lng coordinate pairs for readability
        public static List<(double latitude, double longitude)> DecodeToLatLng(string encodedPolyline)
        {
            List<Point> points = Decode(encodedPolyline);
            var coords = new List<(double, double)>();

            foreach (Point point in points)
            {
                // Point.X is longitude, Point.Y is latitude
                coords.Add((point.Y, point.X));
            }

            return coords;
        }
    }
}