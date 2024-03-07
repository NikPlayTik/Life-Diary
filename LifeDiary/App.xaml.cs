using LifeDiary.PageProgram;

namespace LifeDiary;

public partial class App : Application
{
    public static DiaryEntryDatabase Database { get; private set; }
    public App()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DiaryEntries.db3");
        Database = new DiaryEntryDatabase(dbPath);

        MainPage = new AppShell();
	}
}
