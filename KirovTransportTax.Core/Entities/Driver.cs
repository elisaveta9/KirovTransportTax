namespace KirovTransportTax.Domain.Entities
{
    public class Driver
    {
        public string Passport { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
