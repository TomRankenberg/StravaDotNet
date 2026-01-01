namespace StravaDotNet.ViewModels
{
    public class CalendarDayVm
    {
        public DateTime Date { get; set; }
        public List<SimpleActivityVm> Activities { get; set; } = new();
    }

    public class CalendarWeekVm
    {
        public List<CalendarDayVm> Days { get; set; } = new();
    }
}
