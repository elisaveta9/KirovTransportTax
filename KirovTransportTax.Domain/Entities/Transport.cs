namespace KirovTransportTax.Domain.Entities
{
    public class Transport
    {
        public string NumberTransport { get; set; }
        public string Model { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public string DriverPassport { get; set; }
    }
}
