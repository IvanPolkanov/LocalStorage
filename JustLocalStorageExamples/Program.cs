namespace JustLocalStorageExamples;

internal class Program
{
    static void Main(string[] args)
    {
        Init();

        var intList = GenerateData();

        SaveData(intList);
    }

    private static List<int> GenerateData()
    {
        var intList = new List<int>();
        var rnd = new Random();
        for (int i = 0; i < 100; i++)
        {
            intList.Add(rnd.Next());
        }

        return intList;
    }

    private static void Init()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var fileName = Path.Combine(appDataPath, "JustLocalStorageExamples\\LocalStorage.bin");

        new LocalStorage.LocalStorage(fileName);
    }

    private static void SaveData(object toSave)
    {
        var storage = LocalStorage.LocalStorage.Instance;

        storage.Set("MyBestData", toSave);

        storage.Save();
    }
}