using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace Data.Converters
{
    public class IntListConverter : ValueConverter<List<int?>, string>
    {
        public IntListConverter() : base(
            list => SerializeIntList(list),
            str => DeserializeIntList(str))
        { }

        private static string SerializeIntList(List<int?> list)
        {
            if (list == null || list.Count == 0)
                return "[]";

            var intStrings = list
                .Select(i => i.HasValue ? i.Value.ToString(CultureInfo.InvariantCulture) : "null");

            return $"[{string.Join(",", intStrings)}]";
        }

        private static List<int?> DeserializeIntList(string input)
        {
            var trimmed = input.Trim('[', ']');

            if (string.IsNullOrWhiteSpace(trimmed))
                return [];

            var intValues = trimmed
                .Split(',')
                .Select(s => int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : (int?)null)
                .ToList();

            return intValues;
        }
    }

    public class FloatListConverter : ValueConverter<List<float?>, string>
    {
        public FloatListConverter() : base(
            list => SerializeFloatList(list),
            str => DeserializeFloatList(str))
        { }

        private static string SerializeFloatList(List<float?> list)
        {
            if (list == null || list.Count == 0)
                return "[]";

            var floatStrings = list
                .Select(i => i.HasValue ? i.Value.ToString(CultureInfo.InvariantCulture) : "null");

            return $"[{string.Join(",", floatStrings)}]";
        }

        private static List<float?> DeserializeFloatList(string input)
        {
            var trimmed = input.Trim('[', ']');

            if (string.IsNullOrWhiteSpace(trimmed))
                return [];

            var floatValues = trimmed
                .Split(',')
                .Select(s => float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : (float?)null)
                .ToList();

            return floatValues;
        }
    }

    public class BoolListConverter : ValueConverter<List<bool?>, string>
    {
        public BoolListConverter() : base(
            list => SerializeFloatList(list),
            str => DeserializeFloatList(str))
        { }

        private static string SerializeFloatList(List<bool?> list)
        {
            if (list == null || list.Count == 0)
                return "[]";

            var boolStrings = list
                .Select(i => i.HasValue ? i.Value.ToString(CultureInfo.InvariantCulture) : "null");

            return $"[{string.Join(",", boolStrings)}]";
        }

        private static List<bool?> DeserializeFloatList(string input)
        {
            var trimmed = input.Trim('[', ']');

            if (string.IsNullOrWhiteSpace(trimmed))
                return [];

            var boolValues = trimmed
                .Split(',')
                .Select(s => bool.TryParse(s, out var result)
                    ? result
                    : (bool?)null)
                .ToList();

            return boolValues;
        }
    }


}
