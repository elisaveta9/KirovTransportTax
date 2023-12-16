using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table(Name = "transport")]
    public class TransportDbModel
    {
        [Column(Name = "number_transport"), PrimaryKey, NotNull]
        public string NumberTransport { get; set; }

        [Column(Name = "model"), NotNull]
        [Association(ThisKey = nameof(Model), OtherKey = nameof(TransportModelDbModel.Model))]
        public string Model { get; set; }

        [Column(Name = "registration_date"), NotNull]
        public DateOnly RegistrationDate { get; set; }

        [Column(Name = "driver"), NotNull]
        [Association(ThisKey = nameof(DriverPassport), OtherKey = nameof(DriverDbModel.Passport))]
        public string DriverPassport { get; set; }
    }
}
