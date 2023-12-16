
using KirovTransportTax.Infrastucture;
using LinqToDB;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new TransportDbConnection();
        db.brandDbs
            .Value(p => p.Name, "car")
            .Insert();
    }
}