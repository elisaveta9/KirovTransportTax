using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table("transport_tax_rate")]
    public class TransportTaxRateDbModel
    {
        [Column(Name = "id"), PrimaryKey, NotNull]
        public int Id { get; set; }

        [Column(Name = "transport_type"), NotNull]
        public string Type { get; set; }

        [Association(ThisKey = nameof(Type), OtherKey = nameof(TransportTypeDbModel.Type))]
        public TransportTypeDbModel TransportType { get; set; }

        [Column(Name = "min_horsepower"), NotNull]
        public int MinHorsepower { get; set; }

        [Column(Name = "max_horsepower"), NotNull]
        public int? MaxHorsepower { get; set; }

        [Column(Name = "tax_rate"), NotNull]
        public int TaxRate { get; set; }
    }
}
