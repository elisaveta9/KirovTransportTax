using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture
{
    public class TransportDbConnection : LinqToDB.Data.DataConnection
    {
        public TransportDbConnection(DataOptions dataOptions) : base(dataOptions) { } 

        public ITable<BrandDbModel> BrandDbs => this.GetTable<BrandDbModel>();
        public ITable<DriverDbModel> DriverDbs => this.GetTable<DriverDbModel>();
        public ITable<TransportDbModel> TransportDb => this.GetTable<TransportDbModel>();
        public ITable<TransportTypeDbModel> TransportTypeDbs => this.GetTable<TransportTypeDbModel>();
        public ITable<TransportModelDbModel> TransportModelDbs => this.GetTable<TransportModelDbModel>();
        public ITable<TransportTaxRateDbModel> TransportTaxRateDbs => this.GetTable<TransportTaxRateDbModel>();
    }
}
