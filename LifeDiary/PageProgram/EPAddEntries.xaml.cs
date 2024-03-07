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

        DiaryEntry = new DiaryEntryModel
        {
            Date = DateTime.Now, // Установка текущей даты
            Time = DateTime.Now.TimeOfDay // Установка текущего времени
        };
        this.BindingContext = DiaryEntry;
    }
    private void OnButtonTransitionMainPage(object sender, EventArgs e)
    {
        MainPage.IsButtonEntriesClicked = false;
        Navigation.PopAsync(); // Возвращаемся назад
    }
    async void Save_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DiaryEntry.Title) || string.IsNullOrWhiteSpace(DiaryEntry.Description))
        {
            await DisplayAlert("Ошибка", "Название и описание не могут быть пустыми.", "OK");
            return;
        }
        DiaryEntry.Date = DiaryEntry.Date.Date + DiaryEntry.Time;
        await App.Database.SaveEntryAsync(DiaryEntry); // Добавляем запись в коллекцию
        await Navigation.PopAsync();
    }
}
