namespace MvpSpasibki.Db;

public class Spasibka
{
    public int Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Text { get; set; }
    
    public Spasibka(string from, string to, string text)
    {
        From = from;
        To = to;
        Text = text;
    }
}