namespace LifeDiary.PageProgram;

public partial class MainPage : ContentPage
{
    public static bool IsButtonEntriesClicked = false;
    public static bool IsButtonGoalsClicked = false;
    public static bool IsButtonAchievementsClicked = false;

    public MainPage()
	{
		InitializeComponent();
        UpdateButtonState();
    }

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
    private void OnButtonGoalsClicked(object sender, EventArgs e)
    {
        if (!IsButtonGoalsClicked)
        {
            IsButtonGoalsClicked = true;
            Navigation.PushAsync(new MPGoals());
        }
    }
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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateButtonState();
    }
    


}

