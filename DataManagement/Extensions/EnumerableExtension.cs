namespace DataManagement.Extensions
{
    public static class EnumerableExtension
    {
        public static double? Median(this IEnumerable<float?> source)
        {
            // Based on https://learn.microsoft.com/en-us/dotnet/csharp/linq/how-to-extend-linq
            if (source is null)
            {
                throw new InvalidOperationException("Cannot compute median for a null set.");
            }

            var nonNullValues = source.Where(x => x.HasValue).Select(x => x!.Value).ToList();

            if (nonNullValues.Count == 0)
            {
                return null;
            }

            var sortedList = nonNullValues.OrderBy(number => number).ToList();
            int itemIndex = sortedList.Count / 2;

            if (sortedList.Count % 2 == 0)
            {
                // Even number of items.
                return (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2;
            }
            else
            {
                // Odd number of items.
                return sortedList[itemIndex];
            }
        }
    }
}
