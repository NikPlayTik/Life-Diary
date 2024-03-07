using System.Collections.ObjectModel;

namespace LifeDiary.PageProgram;

public static class DataStore
{
    public static ObservableCollection<DiaryEntryModel> Entries { get; } = new ObservableCollection<DiaryEntryModel>();
}
public partial class EPAddEntries : ContentPage
{
    public DiaryEntryModel DiaryEntry { get; set; }

    public EPAddEntries()
    {
        InitializeComponent();

        DiaryEntry = new DiaryEntryModel();
        this.BindingContext = DiaryEntry;
    }
    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonEntriesClicked = false;
        Navigation.PopAsync(); // ������������ �����
    }
    async void Save_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DiaryEntry.Title) || string.IsNullOrWhiteSpace(DiaryEntry.Description))
        {
            await DisplayAlert("������", "�������� � �������� �� ����� ���� �������.", "OK");
            return;
        }
        DataStore.Entries.Add(DiaryEntry); // ��������� ������ � ���������
        // ����� �������� ������ ���������� DiaryEntry � ���� ��������� ������
        await Navigation.PopAsync();
    }
}
