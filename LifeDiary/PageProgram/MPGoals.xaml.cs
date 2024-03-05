namespace LifeDiary.PageProgram;

public partial class MPGoals : ContentPage
{
	public MPGoals()
	{
		InitializeComponent();
	}

    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonGoalsClicked = false;
        Navigation.PopAsync(); // Возвращаемся назад
    }
    protected override bool OnBackButtonPressed()
    {
        MainPage.IsButtonGoalsClicked = false;
        return base.OnBackButtonPressed();
    }
}