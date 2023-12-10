namespace KirovTransportTax.Domain.DTO
{
    public class TaxDriverDTO
    {
        public DriverDTO Driver { get; set; }
        public float SumTax { get; set; }

        public TaxDriverDTO(DriverDTO driver, float sumTax)
        {
            Driver = driver;
            SumTax = sumTax;
        }
    }
}
