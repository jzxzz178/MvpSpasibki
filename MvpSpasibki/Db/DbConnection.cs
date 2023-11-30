using System.Data.SQLite;

namespace MvpSpasibki.Db;

public class DbConnection
{
    private static SQLiteConnection? connection;
    private static SQLiteCommand command;
    private const string DbName = "Spasibki.sqlite";

    static public bool Connect(string fileName)
    {
        try
        {
            connection = new SQLiteConnection($"Data Source={fileName};Version=3;FailIfMissing=False");
            connection.Open();
            return true;
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Ошибка доступа к базе данных. Исключение: {ex.Message}");
            return false;
        }
    }
    public static void Start()
    {
        if (Connect(DbName))
        {
            Console.WriteLine($"Connected to {DbName}");
        }
    }
}