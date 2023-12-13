namespace KirovTransportTax.Core.Entities
{
    public class TransportTax
    {
        public string NumberTransport { get; set; }
        public string TransportType { get; set; }
        public string Driver { get; set; }
        public int Horsepower { get; set; }
        public int PeriodInMonths { get; set; }
        public int TaxRate { get; set; }
        public float Tax { get; set; }
    }
}
