namespace KirovTransportTax.Domain.DTO
{
    public class TransportModelDTO
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string TransportType { get; set; }
        public int Horsepower { get; set; }
        public int seats { get; set; }
        public int ReleaseYear { get; set; }

        public TransportModelDTO(string model, string brand, string transportType, int horsepower, int seats, int releaseYear)
        {
            Model = model;
            Brand = brand;
            TransportType = transportType;
            Horsepower = horsepower;
            this.seats = seats;
            ReleaseYear = releaseYear;
        }
    }
}
