namespace KirovTransportTax.Domain.DTO
{
    public class TaxTransportDTO
    {
        public TransportDTO Transport { get; set; }
        public string NameDriver { get; set; }
        public float Tax { get; set; }

        public TaxTransportDTO(TransportDTO transport, string nameDriver, float tax)
        {
            Transport = transport;
            NameDriver = nameDriver;
            Tax = tax;
        }
    }
}
