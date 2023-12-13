namespace KirovTransportTax.Domain.Entities
{
    public class TransportTaxRate
    {
        public int Id { get; set; }
        public string TransportType { get; set; }
        public int MinHorsepower { get; set; }
        public int MaxHorsepower { get; set; }
        public int TaxRate { get; set; }
    }
}
