using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class DriverTaxRepository : IDriverTaxRepository
    {
        private readonly TransportDbConnection dbContext;

        public DriverTaxRepository(TransportDbConnection dbContext)
        {
            this.dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            dbContext.CommitTransaction();
        }

        public async Task<IEnumerable<DriverTax>> GetAll()
        {
            var transportInfo = from t in dbContext.TransportDb
                                join tm in dbContext.TransportModelDbs on t.Model equals tm.Model
                                select new
                                {
                                    t.NumberTransport,
                                    tm.TransportType,
                                    t.DriverPassport,
                                    tm.Horsepower,
                                    PeriodInMonths = t.RegistrationDate.Year != DateTime.Now.Year ? 12 :
                                                     t.RegistrationDate.Day > 15 ? 12 - t.RegistrationDate.Month :
                                                                                   12 - t.RegistrationDate.Month + 1
                                };
            var taxInfo = from ti in transportInfo
                          from ttr in dbContext.TransportTaxRateDbs.Where(ttr => ti.TransportType.Equals(ttr.Type)
                          && ti.Horsepower > ttr.MinHorsepower &&
                          (ttr.MaxHorsepower == 0 || ti.Horsepower <= ttr.MaxHorsepower))
                          select new TransportTax
                          {
                              NumberTransport = ti.NumberTransport,
                              TransportType = ti.TransportType,
                              Driver = ti.DriverPassport,
                              Horsepower = ti.Horsepower,
                              PeriodInMonths = ti.PeriodInMonths,
                              TaxRate = ttr.TaxRate,
                              Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                          };
            var taxDriver = from ti in taxInfo
                            group ti by ti.Driver into td
                            orderby td.Sum(t => t.Tax)
                            select new DriverTax
                            {
                                DriverPassport = td.Key,
                                SumTax = td.Sum(t => t.Tax)
                            };
            return taxDriver;
        }

        public async Task<DriverTax?> GetByPassport(string passport)
        {
            var transportInfo = from t in dbContext.TransportDb.Where(d => d.DriverPassport.Equals(passport))
                                join tm in dbContext.TransportModelDbs on t.Model equals tm.Model
                                select new
                                {
                                    t.NumberTransport,
                                    tm.TransportType,
                                    t.DriverPassport,
                                    tm.Horsepower,
                                    PeriodInMonths = t.RegistrationDate.Year != DateTime.Now.Year ? 12 :
                                                     t.RegistrationDate.Day > 15 ? 12 - t.RegistrationDate.Month :
                                                                                   12 - t.RegistrationDate.Month + 1
                                };
            if (transportInfo == null)
                return null;
            var taxInfo = from ti in transportInfo
                          from ttr in dbContext.TransportTaxRateDbs.Where(ttr => ti.TransportType.Equals(ttr.Type)
                          && ti.Horsepower > ttr.MinHorsepower &&
                          (ttr.MaxHorsepower == 0 || ti.Horsepower <= ttr.MaxHorsepower))
                          select new TransportTax
                          {
                              NumberTransport = ti.NumberTransport,
                              TransportType = ti.TransportType,
                              Driver = ti.DriverPassport,
                              Horsepower = ti.Horsepower,
                              PeriodInMonths = ti.PeriodInMonths,
                              TaxRate = ttr.TaxRate,
                              Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                          };
            var taxDriver = from ti in taxInfo
                            group ti by ti.Driver into td
                            orderby td.Sum(t => t.Tax)
                            select new DriverTax
                            {
                                DriverPassport = td.Key,
                                SumTax = td.Sum(t => t.Tax)
                            };
            return await taxDriver.FirstOrDefaultAsync();
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }
    }
}
