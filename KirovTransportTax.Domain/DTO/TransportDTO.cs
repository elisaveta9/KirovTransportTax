namespace KirovTransportTax.Domain.DTO
{
    public class TransportDTO
    {
        public string Number { get; set; }
        public TransportModelDTO TransportModel { get; set; }
        public DateOnly RegistrationDate { get; set; }

        public TransportDTO(string number, TransportModelDTO transportModel, DateOnly registrationDate)
        {
            Number = number;
            TransportModel = transportModel;
            RegistrationDate = registrationDate;
        }
    }
}
