using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture
{
    public class TransportDbConnection : LinqToDB.Data.DataConnection
    {
        private TransportDbConnection(DataOptions dataOptions) : base(dataOptions) { } 

        public ITable<BrandDbModel> BrandDbs => this.GetTable<BrandDbModel>();
        public ITable<DriverDbModel> DriverDbs => this.GetTable<DriverDbModel>();
        public ITable<TransportDbModel> TransportDb => this.GetTable<TransportDbModel>();
        public ITable<TransportModelDbModel> TransportModelDbs => this.GetTable<TransportModelDbModel>();

        public static TransportDbConnection GetTransportDbConnection(string provider, string connectionString)
        {
            switch (provider)
            {
                case "PostgreSQL":
                    {
                        return new TransportDbConnection(new DataOptions()
                            .UsePostgreSQL(
                            "Server=localhost;" +
                            "Port=5436;Database=kirov_transport_tax;User ID=postgres;Password=student;"));
                    }
                default:
                    {
                        throw new ArgumentException($"Unknown database povider: {provider}");
                    }
            }
        }
    }
}
