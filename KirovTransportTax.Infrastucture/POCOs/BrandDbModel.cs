using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table(Name = "transport_brand")]
    public class BrandDbModel
    {
        [Column(Name = "brand"), PrimaryKey, NotNull]
        public string Name { get; set; }
    }
}
