using System.IO;
using System.Text;

namespace MvpSpasibki;

public static class Writer
{
    public static bool WriteSpasibka(string from, string to, string text)
    {
        {
            using (var wr = new StreamWriter("spasibki.txt", true, Encoding.UTF8))
                wr.WriteLineAsync($"От: {from}\n" +
                                  $"Кому: {to}\n" +
                                  $"Текст: {text}\n")
                    .Wait();

            Console.WriteLine("Спасибка записана");
            return true;
        }
    }
}