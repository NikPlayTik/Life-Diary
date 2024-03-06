using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace LifeDiary.PageProgram;

public partial class MPEntries : ContentPage
{
    private ObservableCollection<DiaryEntry> entries = new ObservableCollection<DiaryEntry>();
    public MPEntries()
	{
		InitializeComponent();
	}

    // ����������� ���� � ��������� �� �������� ���� � ������
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


    // ������� �� ����� ����� ��������� ����������� ����
    private async void Frame_Tapped(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet(null, null, null, "�������������", "�������");
        switch (action)
        {
            case "�������������":
                // �������� ��� ��������������
                EditItem();
                break;
            case "�������":
                // �������� ��� ��������
                DeleteItem();
                break;
        }
    }
    private void EditItem()
    {
        // ���������� ������ ��������������
        DisplayAlert("��������������", "�� ������� ������������� �������", "OK");
    }
    private void DeleteItem()
    {
        // ���������� ������ ��������
        DisplayAlert("��������", "�� ������� ������� �������", "OK");
    }
    private async void AddEntries(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EPAddEntries());
    }

    // �������� ������ �� ���� �������� ������
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadEntries();
    }
    private void LoadEntries()
    {
        EntriesStackLayout.Children.Clear(); // ������� ������� ������ � UI

        foreach (var entry in DataStore.Entries)
        {
            var entryFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#1F1277"),
                Padding = 15,
                CornerRadius = 30,
                Margin = new Thickness(0, 0, 0, 20) // ��������� ������ ������ ��� ���������� �������
            };

            var contentStackLayout = new StackLayout();

            var headerGrid = new Grid
            {
                RowDefinitions = { new RowDefinition { Height = GridLength.Auto } },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }
            }
            };

            var dateLabel = new Label
            {
                Text = entry.Date.ToString("g"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                TextColor = Color.FromHex("#FF8F62"),
                HorizontalOptions = LayoutOptions.Start
            };

            var titleLabel = new Label
            {
                Text = entry.Title,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                TextColor = Color.FromHex("#ffffff"),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            headerGrid.Children.Add(dateLabel); // ��������� ������� ��� �������� �������
            Grid.SetRow(dateLabel, 0); // ������������� ������ ��� ��������
            Grid.SetColumn(dateLabel, 0); // ������������� ������� ��� ��������

            headerGrid.Children.Add(titleLabel); // ���������� ��������� ������ �������
            Grid.SetRow(titleLabel, 0);
            Grid.SetColumn(titleLabel, 1);

            var descriptionLabel = new Label
            {
                Text = entry.Description,
                TextColor = Color.FromHex("#ffffff"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Margin = new Thickness(0, 10)
            };

            contentStackLayout.Children.Add(headerGrid);
            contentStackLayout.Children.Add(descriptionLabel);

            var imagesGrid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            },
                Margin = new Thickness(0, 10)
            };

            var image1 = new Image
            {
                BackgroundColor = Color.FromHex("#808080"),
                HeightRequest = 130,
                Margin = new Thickness(0, 0, 10, 0)
            };

            var image2 = new Image
            {
                BackgroundColor = Color.FromHex("#808080"),
                HeightRequest = 130,
                Margin = new Thickness(10, 0, 0, 0)
            };

            imagesGrid.Children.Add(image1);
            Grid.SetColumn(image1, 0); // ��������� ������������� ������� ��� ������� �����������

            imagesGrid.Children.Add(image2);
            Grid.SetColumn(image2, 1); // ��������� ������������� ������� ��� ������� �����������

            contentStackLayout.Children.Add(imagesGrid);

            entryFrame.Content = contentStackLayout;
            EntriesStackLayout.Children.Add(entryFrame);
        }
    }
}