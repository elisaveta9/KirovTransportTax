﻿using LinqToDB.Mapping;

namespace KirovTransportTax.Infrastucture.POCOs
{
    [Table("transport_tax_rate")]
    public class TransportTaxRate
    {
        [Column(Name = "id"), PrimaryKey, NotNull]
        public int Id { get; set; }

        [Column(Name = "transport_type"), NotNull]
        [Association(ThisKey = nameof(Type), OtherKey = nameof(TransportType.Type))]
        public string Type { get; set; }

        [Column(Name = "min_horsepower"), NotNull]
        public int MinHorsepower { get; set; }

        [Column(Name = "max_horsepower"), NotNull]
        public int MaxHorsepower { get; set; }

        [Column(Name = "tax_rate"), NotNull]
        public int TaxRate { get; set; }
    }
}
