using Microsoft.Extensions.Hosting;
using MvpSpasibki.BotApi;
using MvpSpasibki.Db;

namespace MvpSpasibki;

class Program
{
    static void Main(string[] args)
    {
        // DbConnection.Start();
        using var db = new SpasibkiContext();
        var sp = new Spasibka("Коля", "Лера", "Спасибо за ручку");
        db.Spasibki.Add(sp);
        BotClient.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder().ConfigureServices((context, collection) =>
        {
            
        });
    }
}