namespace KirovTransportTax.Domain.Entities
{
    public class TransportTax
    {
        public string DriverPassport { get; set; }
        public string DriverLastname {  get; set; }
        public string DriverName { get; set; }
        public string DriverPatronymic { get; set; }
        public DateOnly DriverBirthday { get; set; }
        public string NumberTransport { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string TransportType { get; set; }
        public int AgeTransport { get; set; }
        public int Horsepower { get; set; }
        public int PeriodInMonths { get; set; }
        public int TaxRate { get; set; }
        public float Tax { get; set; }
    }
}
