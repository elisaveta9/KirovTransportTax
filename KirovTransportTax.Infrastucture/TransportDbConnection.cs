using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture
{
    public class TransportDbConnection : LinqToDB.Data.DataConnection
    {
        public TransportDbConnection() : 
            base(new DataOptions()
                .UsePostgreSQL(
                "Server=localhost;" + 
                "Port=5436;Database=kirov_transport_tax;User ID=postgres;Password=student;")) { }
        public TransportDbConnection(DataOptions dataOptions) : base(dataOptions) { } 

        public ITable<BrandDbModel> brandDbs => this.GetTable<BrandDbModel>();
        public ITable<DriverDbModel> driverDbs => this.GetTable<DriverDbModel>();
        public ITable<TransportDbModel> transportDb => this.GetTable<TransportDbModel>();
        public ITable<TransportModelDbModel> transportModelDbs => this.GetTable<TransportModelDbModel>();
    }
}
