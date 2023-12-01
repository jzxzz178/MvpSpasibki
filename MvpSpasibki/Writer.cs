using System.IO;
using System.Text;

namespace MvpSpasibki;

public static class Writer
{
    private static readonly StreamWriter Wrtr;

    static Writer()
    {
        try
        {
            Wrtr = new StreamWriter("spasibki.txt", true, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Wrtr?.Close();
            Console.WriteLine($"Exception: {e}");
            throw;
        }
        finally
        {
            Console.WriteLine("Writed new spasibka.");
        }
    }

    public static bool WriteSpasibka(string from, string to, string text)
    {
        try
        {
            Wrtr.WriteLine($"От: {from}");
            Wrtr.WriteLine($"Кому: {to}");
            Wrtr.WriteLine($"Текст: {text}");
            Wrtr.WriteLine();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
            return false;
        }

        finally
        {
            Wrtr.Close();
        }
        
    }
}