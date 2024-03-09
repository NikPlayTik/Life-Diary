using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;

namespace LifeDiary.PageProgram;

public partial class MPGoals : ContentPage
{
    private ObservableCollection<DiaryGoalsModel> goals = new ObservableCollection<DiaryGoalsModel>();
    public MPGoals()
	{
		InitializeComponent();
    }

    // ������� ����� � ����
    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonGoalsClicked = false;
        Navigation.PopAsync(); // ������������ �����
    }
    protected override bool OnBackButtonPressed()
    {
        MainPage.IsButtonGoalsClicked = false;
        return base.OnBackButtonPressed();
    }

    // ����������� ���� ��� ������� �� Frame
    private async void Frame_Tapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var goal = (DiaryGoalsModel)frame.BindingContext;

        string action = await DisplayActionSheet(null, null, null, "�������������", "�������");
        switch (action)
        {
            case "�������������":
                // �������� ��� ��������������
                EditItem(goal);
                break;
            case "�������":
                // �������� ��� ��������
                DeleteItem(goal);
                break;
        }
    }
        private async void AddGoals(object sender, EventArgs e)
        {
        await Navigation.PushAsync(new GPAddGoals());
        }
        private async void EditItem(DiaryGoalsModel goal)
        {
        await Navigation.PushAsync(new GPAddGoals(goal));
        }
        private async void DeleteItem(DiaryGoalsModel goal)
        {
        // ���������� ������ ��������
        var result = await App.GoalsDatabase.DeleteGoalAsync(goal);
        if (result == 1) // ���� �������� ������ �������
        {
            DisplayAlert("��������", "���� ������� �������", "OK");
            LoadGoals(); // ��������� ������ �����
        }
        else
        {
            DisplayAlert("������", "��������� ������ ��� �������� ����", "OK");
        }
        }

    // �������� ������ �����
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadGoals();
    }
    private async void LoadGoals()
    {
        GoalsStackLayout.Children.Clear(); // ������� ������� ���� � UI
        var goals = await App.GoalsDatabase.GetGoalsAsync();
        foreach (var goal in goals) // ���������� ���� �� SQLite
        {
            var goalFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#1F1277"),
                Padding = 15,
                CornerRadius = 30,
                Margin = new Thickness(0, 0, 0, 20) // ��������� ������ ������ ��� ���������� �����
            };

            // ��������� ���������� ������� Tapped ���� 2 ���� ������
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += Frame_Tapped;
            goalFrame.GestureRecognizers.Add(tapGestureRecognizer);

            // ������������� BindingContext ��� ������
            goalFrame.BindingContext = goal;
            var contentStackLayout = new StackLayout();

            var headerGrid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition
                {
                    Height = new GridLength(70, GridUnitType.Absolute)
                }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                },
                new ColumnDefinition
                {
                    Width = new GridLength(2, GridUnitType.Star)
                }
            }
            };

            var dateLabel = new Label
            {
                Text = goal.Deadline.ToString("dd.MM.yyyy"), // ��������� ������� ����
                FontAttributes = FontAttributes.Bold,
                FontSize = 17,
                TextColor = Color.FromHex("#FF8F62"),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 18, 0, 0)
            };

            // ������� ������
            var deadlineEllipse = new Ellipse
            {
                WidthRequest = 35,
                HeightRequest = 35,
                HorizontalOptions = LayoutOptions.End,
            };

            // ��������� ���������� ���� �� ��������
            var daysToDeadline = (goal.Deadline - DateTime.Now).TotalDays;

            // ������������� ���� ������� � ����������� �� ���������� ���� �� ��������
            if (daysToDeadline > 7)
            {
                deadlineEllipse.Fill = Color.FromHex("#5AFD57"); // �������
            }
            else if (daysToDeadline > 3)
            {
                deadlineEllipse.Fill = Color.FromHex("#FFA500"); // ���������
            }
            else
            {
                deadlineEllipse.Fill = Color.FromHex("#FF0000"); // �������
            }


            // ��������� ������ � Grid
            headerGrid.Children.Add(deadlineEllipse);
            Grid.SetRow(deadlineEllipse, 0);
            Grid.SetColumn(deadlineEllipse, 1);

            var descriptionLabel = new Label
            {
                Text = goal.Description,
                TextColor = Color.FromHex("#ffffff"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Margin = new Thickness(0, 10)
            };

            contentStackLayout.Children.Add(headerGrid);
            contentStackLayout.Children.Add(descriptionLabel);

            var progressBar = new ProgressBar
            {
                Progress = goal.Progress,
                Margin = new Thickness(0, 10)
            };

            contentStackLayout.Children.Add(progressBar);
            goalFrame.Content = contentStackLayout;

            var progressLabel = new Label
            {
                Text = $"��������: {goal.Progress * 100}%", // ��������� ������� ���������
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                TextColor = Color.FromHex("#ffffff"),
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center
            };

            contentStackLayout.Children.Add(progressLabel); // ��������� ������� � StackLayout

            headerGrid.Children.Add(dateLabel); // ��������� ������� ��� �������� �������
            Grid.SetRow(dateLabel, 0); // ������������� ������ ��� ��������
            Grid.SetColumn(dateLabel, 0); // ������������� ������� ��� ��������

            GoalsStackLayout.Children.Add(goalFrame);
        }
    }

}