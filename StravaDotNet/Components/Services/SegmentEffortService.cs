using Data.Models.Strava;

namespace StravaDotNet.Components.Services
{
    public class SegmentEffortService(HttpClient httpClient)
    {
        public List<DetailedSegmentEffort> GetDetailedSegmentEffortsAsync()
        {
            List<DetailedSegmentEffort> segmentEfforts = httpClient.GetFromJsonAsync<List<DetailedSegmentEffort>>("api/segmenteffort/GetPRs").Result;

            return segmentEfforts;
        }

        public List<object> GetMonthlyScatterChartData(List<DetailedSegmentEffort> efforts)
        {
            var minDate = efforts.Min(da => da.StartDate);
            var maxDate = efforts.Max(da => da.StartDate);

            var data = efforts.GroupBy(da => new { da.StartDate.Value.Year, da.StartDate.Value.Month })
                .Select(g => new
                {
                                  monthYear = $"{g.Key.Year}-{g.Key.Month:D2}",
                                  y = g.OrderByDescending(da => (da.Distance / da.ElapsedTime) * 3.6).FirstOrDefault().Distance / g.OrderByDescending(da => (da.Distance / da.ElapsedTime) * 3.6).FirstOrDefault().MovingTime * 3.6,
                                  date = g.OrderByDescending(da => (da.Distance / da.ElapsedTime) * 3.6).FirstOrDefault().StartDate?.ToString("yyyy-MM-dd"),
                                  color = GetColorForDate(g.OrderByDescending(da => (da.Distance / da.ElapsedTime) * 3.6).FirstOrDefault().StartDate, minDate, maxDate)
                              }).ToList();

            data = data.OrderBy(d => d.date).ToList();

            return data.Cast<object>().ToList();
        }

        private string GetColorForDate(DateTime? date, DateTime? minDate, DateTime? maxDate)
        {
            if (date == null || minDate == null || maxDate == null)
                return "rgba(75, 192, 192, 0.2)"; // Default color

            var totalDays = (maxDate.Value - minDate.Value).TotalDays;
            var daysFromStart = (date.Value - minDate.Value).TotalDays;
            var ratio = daysFromStart / totalDays;

            // Calculate color based on ratio (0.0 to 1.0)
            var red = (int)(255 * ratio);
            var green = (int)(192 * (1 - ratio));
            var blue = 192;

            return $"rgba({red}, {green}, {blue}, 0.8)";
        }
    }
}