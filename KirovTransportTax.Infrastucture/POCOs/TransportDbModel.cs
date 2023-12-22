using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table(Name = "transport")]
    public class TransportDbModel
    {
        [Column(Name = "number_transport"), PrimaryKey, NotNull]
        public string NumberTransport { get; set; }

        [Column(Name = "model"), NotNull]
        public string Model { get; set; }

        [Association(ThisKey = nameof(Model), OtherKey = nameof(TransportModelDbModel.Model))]
        public TransportModelDbModel TransportModel { get; set; }

        [Column(Name = "registration_date"), NotNull]
        public DateOnly RegistrationDate { get; set; }

        [Column(Name = "driver"), NotNull]
        public string DriverPassport { get; set; }

        [Association(ThisKey = nameof(DriverPassport), OtherKey = nameof(DriverDbModel.Passport))]
        public DriverDbModel DriverModel { get; set; }
    }
}
