namespace LifeDiary.PageProgram;

public partial class MPEntries : ContentPage
{
	public MPEntries()
	{
		InitializeComponent();
	}

    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonEntriesClicked = false;
        Navigation.PopAsync(); // ������������ �����
    }

    protected override bool OnBackButtonPressed()
    {
        MainPage.IsButtonEntriesClicked = false;
        return base.OnBackButtonPressed();
    }

}