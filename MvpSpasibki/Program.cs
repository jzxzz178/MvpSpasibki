using System.Configuration;
using MvpSpasibki.BotApi;
using System.Configuration;
using System.Collections.Specialized;

namespace MvpSpasibki;

class Program
{
    public static void Main(string[] args)
    {
        var tokenReader = new StreamReader("BOT_API_TOKEN.txt");
        var token = tokenReader.ReadLine();
        tokenReader.Close();
        Console.WriteLine(token);
        
        if (token == null) throw new ArgumentException($"Token was: {token}");
        var botClient = new BotClient(token);
        botClient.Run();
    }
}