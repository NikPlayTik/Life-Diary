namespace LifeDiary.PageProgram;

public partial class MainPage : ContentPage
{
    // Поля для исправления повторного нажатия
    public static bool IsButtonEntriesClicked = false;
    public static bool IsButtonGoalsClicked = false;
    public static bool IsButtonAchievementsClicked = false;

    public MainPage()
	{
		InitializeComponent();
        UpdateButtonState();
    }

    // Переход в окно ЗАПИСИ
    private void OnButtonEntriesClicked(object sender, EventArgs e)
    {
        // проверка на переход если кнопка уже была нажата
        if (!IsButtonEntriesClicked)
        {
            IsButtonEntriesClicked = true;
            // переход к MPEntries и обновление состояния кнопки там
            Navigation.PushAsync(new MPEntries());
        }
    }
    private void OnButtonMediaClicked(object sender, EventArgs e)
    {
        if (!IsButtonEntriesClicked)
        {
            IsButtonEntriesClicked = true; 
            Navigation.PushAsync(new MPEntries());
        }
    }
    
    // Переход в окно ЦЕЛИ
    private void OnButtonGoalsClicked(object sender, EventArgs e)
    {
        if (!IsButtonGoalsClicked)
        {
            IsButtonGoalsClicked = true;
            Navigation.PushAsync(new MPGoals());
        }
    }

    // Переход в окно ДОСТИЖЕНИЯ
    private void OnButtonAchievementsClicked(object sender, EventArgs e)
    {
        if (!IsButtonAchievementsClicked)
        {
            IsButtonAchievementsClicked = true;
            Navigation.PushAsync(new MPAchievements());
        }
    }
    private void UpdateButtonState()
    {
        ButtonEntries.IsEnabled = !IsButtonEntriesClicked;
        ButtonGoals.IsEnabled = !IsButtonGoalsClicked;
        ButtonAchievements.IsEnabled = !IsButtonAchievementsClicked;

    }

    // Загрузка недавних добавленных ЗАПИСЕЙ
    private string TrimDescription(string description, int maxLength = 100)
    {
        if (string.IsNullOrEmpty(description)) return description;
        return description.Length <= maxLength ? description : $"{description.Substring(0, maxLength)}...";
    }
    private async Task LoadLastEntry()
    {
        var entries = await App.Database.GetEntriesAsync();
        var lastEntry = entries.LastOrDefault();
        if (lastEntry != null)
        {
            LastEntryDate.Text = lastEntry.Date.ToString("dd.MM.yyyy HH:mm");
            ButtonEntries.Text = lastEntry.Title;
            LastEntryDescription.Text = TrimDescription(lastEntry.Description, 60);
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateButtonState();
        await LoadLastEntry();
    }
    


}

