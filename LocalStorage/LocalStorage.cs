namespace LocalStorage;

public class LocalStorage
{
    public static LocalStorage Instance { get; private set; }

    private Dictionary<string, object> _data = new Dictionary<string, object>();
    private string _saveFilePath;

    public LocalStorage(string filePath)
    {
        _saveFilePath = filePath;

        var dirName = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dirName))
            Directory.CreateDirectory(dirName);

        if (!File.Exists(filePath))
            _data = new Dictionary<string, object>();
        else
        {
            try
            {
                _data = BinarySerializer.ReadFromBinaryFile<Dictionary<string, object>>(filePath);
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
            }
        }

        Instance = this;
    }

    public object Get(string key)
    {
        if (_data.TryGetValue(key, out var result))
            return result;
        else
            return null;
    }

    public void Set(string key, object value)
    {
        _data[key] = value;
    }

    public bool ContainsKey(string key)
    {
        return _data.ContainsKey(key);
    }

    public void Save()
    {
        BinarySerializer.WriteToBinaryFile(_saveFilePath, _data);
    }
}