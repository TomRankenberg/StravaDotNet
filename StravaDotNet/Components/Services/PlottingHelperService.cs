using Data.Models.Strava;
using DataManagement.Models;
using Newtonsoft.Json;
using StravaDotNet.ViewModels;

namespace StravaDotNet.Components.Services
{
    public class PlottingHelperService
    {
        /// <summary>
        /// Prepares data for a scatter plot.
        /// </summary>
        public string PrepareScatterPlotData<T>(List<T> items, Func<T, float?> xSelector, Func<T, double?> ySelector, Func<T, string> colorSelector, Func<T, DateOnly>? dateSelector = null)
        {
            var data = items.Select(item => new
            {
                x = xSelector(item),
                y = Math.Round((decimal)ySelector(item), 2),
                color = colorSelector(item),
                date = dateSelector(item)
            }).ToList();

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Calculates a color based on a date range.
        /// </summary>
        public static string GetColorForDate(DateTime? date, DateTime? minDate, DateTime? maxDate)
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

        public static List<HeatmapInput> AddColorToHeatmap(List<HeatmapInput> heatmapInputs)
        {
            heatmapInputs = heatmapInputs.OrderBy(h => h.StartTime).ToList();
            DateTime earliest = heatmapInputs.First().StartTime ?? DateTime.MinValue;
            DateTime latest = heatmapInputs.Last().StartTime ?? DateTime.MaxValue;
            foreach (HeatmapInput input in heatmapInputs)
            {
                input.LineColor = GetColorForDate(input.StartTime, earliest, latest);
            }

            return heatmapInputs;
        }

        /// <summary>
        /// Prepares data for a monthly stacked bar chart.
        /// </summary>
        public string PrepareMonthlyBarChartData(List<ActivityVm> activities)
        {
            var groupedActivities = activities
                .GroupBy(a => new { a.Activity.StartDate.Value.Year, a.Activity.StartDate.Value.Month, a.Activity.Type })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Type = g.Key.Type,
                    Count = g.Count()
                })
                .ToList();

            var labels = groupedActivities
                .Select(g => $"{g.Year}-{g.Month:00}")
                .Distinct()
                .OrderBy(label => label)
                .ToList();

            var types = groupedActivities
                .Select(g => g.Type)
                .Distinct()
                .ToList();

            var datasets = types.Select(type => new
            {
                label = type,
                data = labels.Select(label =>
                {
                    var yearMonth = label.Split('-');
                    var year = int.Parse(yearMonth[0]);
                    var month = int.Parse(yearMonth[1]);
                    return groupedActivities
                        .Where(g => g.Year == year && g.Month == month && g.Type == type)
                        .Sum(g => g.Count);
                }).ToList()
            }).ToList();

            var chartData = new
            {
                labels,
                datasets
            };

            return JsonConvert.SerializeObject(chartData);
        }

        /// <summary>
        /// Prepares data for an hourly stacked bar chart.
        /// </summary>
        public string PrepareHourlyBarChartData(List<ActivityVm> activities)
        {
            var groupedActivities = activities
                .GroupBy(a => new { a.Activity.StartDateLocal.Value.Hour, a.Activity.Type })
                .Select(g => new
                {
                    Hour = g.Key.Hour,
                    Type = g.Key.Type,
                    Count = g.Count()
                })
                .ToList();

            var labels = groupedActivities
                .Select(g => $"{g.Hour:00}")
                .Distinct()
                .OrderBy(label => label)
                .ToList();

            var types = groupedActivities
                .Select(g => g.Type)
                .Distinct()
                .ToList();

            var datasets = types.Select(type => new
            {
                label = type,
                data = labels.Select(label =>
                {
                    var hour = int.Parse(label);
                    return groupedActivities
                        .Where(g => g.Hour == hour && g.Type == type)
                        .Sum(g => g.Count);
                }).ToList()
            }).ToList();

            var chartData = new
            {
                labels,
                datasets
            };

            return JsonConvert.SerializeObject(chartData);
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

        public string PrepareCumulativeDistanceData(List<ActivityVm> activities, string activityType)
        {
            var filteredActivities = activities
                .Where(a => a.Activity.Type == activityType && a.Activity.StartDate.HasValue)
                .OrderBy(a => a.Activity.StartDate.Value)
                .ToList();

            var groupedByYear = filteredActivities
                .GroupBy(a => a.Activity.StartDate.Value.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Data = g
                        .OrderBy(a => a.Activity.StartDate.Value)
                        .Select(a => new
                        {
                            x = a.Activity.StartDate.Value.ToString("dd-MM"), // Use only day/month for x-axis
                            y = g
                                .Where(x => x.Activity.StartDate.Value <= a.Activity.StartDate.Value)
                                .Sum(x => x.Activity.Distance / 1000) // Convert to km
                        })
                        .ToList()
                })
                .ToList();

            var minYear = groupedByYear.Min(g => g.Year);
            var maxYear = groupedByYear.Max(g => g.Year);

            var datasets = groupedByYear.Select(g => new
            {
                label = g.Year.ToString(),
                data = g.Data, // Pass the data directly
                backgroundColor = GetColorForDate(new DateTime(g.Year, 1, 1), new DateTime(minYear, 1, 1), new DateTime(maxYear, 12, 31)),
            });

            var chartData = new
            {
                labels = Enumerable.Range(1, 12)
                    .SelectMany(month => Enumerable.Range(1, DateTime.DaysInMonth(2023, month))
                    .Select(day => new DateTime(2023, month, day).ToString("dd-MM"))), // Generate all possible day/month combinations
                datasets
            };

            return JsonConvert.SerializeObject(chartData);
        }
    }
}