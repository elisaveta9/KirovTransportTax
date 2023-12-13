using LinqToDB.Mapping;
namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table("driver")]
    public class Driver
    {
        [Column(Name = "passport"), PrimaryKey, NotNull]
        public string Passport { get; set; }

        [Column(Name = "last_name"), NotNull]
        public string LastName { get; set; }

        [Column(Name = "name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "patronymic")]
        public string? Patronymic { get; set; }

        [Column(Name = "birthday"), NotNull]
        public DateOnly Birthday { get; set; }
    }
}
