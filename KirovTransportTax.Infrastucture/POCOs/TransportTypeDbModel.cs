using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table("transport_type")]
    public class TransportTypeDbModel
    {
        [Column(Name = "type"), PrimaryKey, NotNull]
        public string Type { get; set; }
    }
}
