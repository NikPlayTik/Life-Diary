namespace LifeDiary.PageProgram;

public partial class MPAchievements : ContentPage
{
	public MPAchievements()
	{
		InitializeComponent();
	}

    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonAchievementsClicked = false;
        Navigation.PopAsync(); // Возвращаемся назад
    }

    protected override bool OnBackButtonPressed()
    {
        MainPage.IsButtonAchievementsClicked = false;
        return base.OnBackButtonPressed();
    }
}