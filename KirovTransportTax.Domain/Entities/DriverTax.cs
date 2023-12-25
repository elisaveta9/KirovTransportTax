namespace KirovTransportTax.Domain.Entities
{
    public class DriverTax
    {
        public string DriverPassport { get; set; }
        public string DriverLastname { get; set; }
        public string DriverName { get; set; }
        public string DriverPatronymic { get; set; }
        public DateOnly DriverBirthday { get; set; }
        public int CountTransport { get; set; }
        public float SumTax { get; set; }
    }
}
