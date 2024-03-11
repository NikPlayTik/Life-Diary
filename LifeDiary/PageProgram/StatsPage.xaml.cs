using Microcharts;
using SkiaSharp;

namespace LifeDiary.PageProgram;

public partial class StatsPage : ContentPage
{
	public StatsPage()
	{
		InitializeComponent();

        App.Database = new DiaryEntryDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DiaryEntries.db3"));
        UpdateCharts();
    }

    public async Task UpdateCharts()
    {
        // Получаем данные из базы данных
        var entries = await App.Database.GetEntriesAsync();
        // Создаем данные для диаграмм
        var entriesPerMonth = entries
            .GroupBy(e => e.Date.ToString("MMMM"))
            .ToDictionary(g => g.Key, g => g.Count());

        var entriesActivity = entries
            .GroupBy(e => new { DayOfWeek = GetDayOfWeekInRussian(e.Date.DayOfWeek), TimeSlot = GetTimeSlot(e.Date.Hour) })
            .ToDictionary(g => g.Key, g => g.Count());
        // Создаем диаграммы
        var chartEntries = entriesPerMonth.Select(e => new ChartEntry(e.Value)
        {
            Label = e.Key,
            ValueLabel = e.Value.ToString(),
            ValueLabelColor = SKColor.Parse("#ffffff"),
            
            Color = SKColor.Parse("#D6FD57")
        }).ToList();
        var pointChart = new PointChart
        {
            Entries = chartEntries,
            LabelTextSize = 50f, // размер шрифта для меток
            LabelColor = SKColor.Parse("#ffffff"),
            AnimationDuration = TimeSpan.Zero,
            BackgroundColor = SKColors.Transparent
        };
        BarChartView.Chart = pointChart;

        // Добавляем вторую диаграмму
        var chartEntriesActivity = entriesActivity.Select(e => new ChartEntry(e.Value)
        {
            Label = $"{e.Key.DayOfWeek} {e.Key.TimeSlot}",
            ValueLabel = e.Value.ToString(),
            ValueLabelColor = SKColor.Parse("#ffffff"),
            Color = SKColor.Parse("#D6FD57")
        }).ToList();
        var barChartActivity = new BarChart
        {
            Entries = chartEntriesActivity,
            LabelTextSize = 50f, // размер шрифта для меток
            LabelColor = SKColor.Parse("#ffffff"),
            CornerRadius = 30,
            AnimationDuration = TimeSpan.Zero,
            BackgroundColor = SKColors.Transparent
        };
        ActivityChartView.Chart = barChartActivity;




    }


    // Общая реализация
    private string GetTimeSlot(int hour)
    {
        if (hour >= 0 && hour < 3) return "0:00";
        if (hour >= 3 && hour < 6) return "3:00";
        if (hour >= 6 && hour < 9) return "6:00";
        if (hour >= 9 && hour < 12) return "9:00";
        if (hour >= 12 && hour < 15) return "12:00";
        if (hour >= 15 && hour < 18) return "18:00";
        return "21:00";
    }
    private string GetDayOfWeekInRussian(DayOfWeek dayOfWeek)
    {
        var cultureInfo = new System.Globalization.CultureInfo("ru-RU");
        return cultureInfo.DateTimeFormat.GetDayName(dayOfWeek);
    }

}