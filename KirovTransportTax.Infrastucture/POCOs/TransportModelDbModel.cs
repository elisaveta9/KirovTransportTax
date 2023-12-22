using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table(Name = "transport_model")]
    public class TransportModelDbModel
    {
        [Column(Name = "model"), PrimaryKey, NotNull]
        public string Model { get; set; }

        [Column(Name = "brand"), NotNull]
        public string Brand { get; set; }

        [Association(ThisKey = nameof(Brand), OtherKey = nameof(BrandDbModel.Name))]
        public BrandDbModel BrandDb { get; set; }

        [Column(Name = "horsepower"), NotNull]
        public int Horsepower { get; set; }

        [Column(Name = "release_year")]
        public int ReleaseYear { get; set; }

        [Column(Name = "transport_type"), NotNull]
        public string TransportType { get; set; }

        [Association(ThisKey = nameof(TransportType), OtherKey = nameof(TransportTypeDbModel.Type))]
        public TransportTypeDbModel TransportTypeDb { get; set; }
    }
}
