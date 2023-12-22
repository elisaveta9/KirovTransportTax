namespace KirovTransportTax.Application.Transports.Commands
{
    public class CreateTransportDto
    {
        public string NumberTransport { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public int Horsepower { get; set; }

        public int ReleaseYear { get; set; }

        public DateOnly RegistrationDate { get; set; }

        public string TransportType { get; set; }

        public string DriverPassport { get; set; }

        public string DriverLastName { get; set; }

        public string DriverName { get; set; }

        public string? DriverPatronymic { get; set; }

        public DateOnly DriverBirthday { get; set; }
    }
}