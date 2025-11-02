using System.Globalization;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Converters
{
    public class LatLngConverter : ValueConverter<LatLng, string>
    {
        public LatLngConverter() : base(
            // Convert LatLng (list of floats) to string: "[34.07872,-117.644066]"
            latLng => SerializeLatLng(latLng),

            // Convert string back to LatLng object
            str => DeserializeLatLng(str))
        { }

        private static string SerializeLatLng(LatLng latLng)
        {
            if (latLng == null || latLng.Count == 0)
                return "[]";

            var floatStrings = latLng
                .Select(f => f?.ToString(CultureInfo.InvariantCulture) ?? "null");

            return $"[{string.Join(",", floatStrings)}]";
        }

        private static LatLng DeserializeLatLng(string input)
        {
            var trimmed = input.Trim('[', ']');

            if (string.IsNullOrWhiteSpace(trimmed))
            {
                return [];
            }

            var floatValues = trimmed
                .Split(',')
                .Select(s => float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : (float?)null)
                .ToList();

            var latLng = new LatLng();
            latLng.AddRange(floatValues);

            return latLng;
        }
    }
}
