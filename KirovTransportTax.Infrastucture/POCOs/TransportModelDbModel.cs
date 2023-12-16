using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table(Name = "transport_model")]
    public class TransportModelDbModel
    {
        [Column(Name = "model"), PrimaryKey, NotNull]
        public string Model { get; set; }

        [Column(Name = "brand"), NotNull]
        [Association(ThisKey = nameof(BrandName), OtherKey = nameof(BrandDbModel.Name))]
        public string BrandName { get; set; }

        [Column(Name = "horsepower"), NotNull]
        public int Horsepower { get; set; }

        [Column(Name = "release_year")]
        public int ReleaseYear { get; set; }

        [Column(Name = "transport_type"), NotNull]
        [Association(ThisKey = nameof(Type), OtherKey = nameof(TransportTypeDbModel.Type))]
        public string Type { get; set; } 
    }
}
