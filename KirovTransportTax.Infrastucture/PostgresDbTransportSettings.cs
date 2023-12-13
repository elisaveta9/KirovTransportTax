using LinqToDB;
using LinqToDB.Configuration;

namespace KirovTransportTax.Infrastucture
{
    internal class PostgresDbTransportSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders =>
            Enumerable.Empty<IDataProviderSettings>();

        public string? DefaultConfiguration => "PostgreSQL 16";

        public string? DefaultDataProvider => "PostgreSQL 16";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "kirov_transport_tax",
                        ProviderName = ProviderName.PostgreSQL,
                        ConnectionString = @"Server=.\;Database=kirov_transport_tax;Trusted_Connection=True;"
                    };
            }
        }

    }
}
