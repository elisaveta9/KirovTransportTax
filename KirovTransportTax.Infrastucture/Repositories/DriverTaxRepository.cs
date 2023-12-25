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
                                    tm.Brand,
                                    tm.Model,
                                    tm.ReleaseYear,
                                    tm.TransportType,
                                    t.DriverPassport,
                                    tm.Horsepower,
                                    PeriodInMonths = t.RegistrationDate.Year != DateTime.Now.Year ? 12 :
                                                     t.RegistrationDate.Day > 15 ? 12 - t.RegistrationDate.Month :
                                                                                   12 - t.RegistrationDate.Month + 1
                                };
            var taxInfo = from ti in transportInfo
                          join ttr in dbContext.TransportTaxRateDbs on ti.TransportType equals ttr.Type
                          join d in dbContext.DriverDbs on ti.DriverPassport equals d.Passport
                          where (ti.Horsepower > ttr.MinHorsepower &&
                          (ttr.MaxHorsepower == null || ti.Horsepower <= ttr.MaxHorsepower))
                          select new TransportTax
                          {
                              DriverPassport = ti.DriverPassport,
                              DriverLastname = d.LastName,
                              DriverName = d.Name,
                              DriverPatronymic = d.Patronymic,
                              DriverBirthday = d.Birthday,
                              NumberTransport = ti.NumberTransport,
                              Brand = ti.Brand,
                              Model = ti.Model,
                              TransportType = ti.TransportType,
                              AgeTransport = DateTime.Now.Year - ti.ReleaseYear,
                              Horsepower = ti.Horsepower,
                              PeriodInMonths = ti.PeriodInMonths,
                              TaxRate = ttr.TaxRate,
                              Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                          };
            var taxDriver = from ti in taxInfo
                            group ti by ti.DriverPassport into td
                            orderby td.Sum(t => t.Tax)
                            select new DriverTax
                            {
                                DriverPassport = td.Key,
                                DriverLastname = td.First().DriverLastname,
                                DriverName = td.First().DriverName,
                                DriverPatronymic = td.First().DriverPatronymic,
                                DriverBirthday = td.First().DriverBirthday,
                                CountTransport = td.Count(),
                                SumTax = td.Sum(t => t.Tax)
                            };
            return taxDriver.OrderByDescending(td => td.SumTax).OrderByDescending(td => td.SumTax);
        }

        public async Task<DriverTax?> GetByPassport(string passport)
        {
            var transportInfo = from t in dbContext.TransportDb.Where(d => d.DriverPassport.Equals(passport))
                                join tm in dbContext.TransportModelDbs on t.Model equals tm.Model
                                select new
                                {
                                    t.NumberTransport,
                                    tm.Brand,
                                    tm.Model,
                                    tm.ReleaseYear,
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
                          join ttr in dbContext.TransportTaxRateDbs on ti.TransportType equals ttr.Type
                          join d in dbContext.DriverDbs on ti.DriverPassport equals d.Passport
                          where (ti.Horsepower > ttr.MinHorsepower &&
                          (ttr.MaxHorsepower == null || ti.Horsepower <= ttr.MaxHorsepower))
                          select new TransportTax
                          {
                              DriverPassport = ti.DriverPassport,
                              DriverLastname = d.LastName,
                              DriverName = d.Name,
                              DriverPatronymic = d.Patronymic,
                              DriverBirthday = d.Birthday,
                              NumberTransport = ti.NumberTransport,
                              Brand = ti.Brand,
                              Model = ti.Model,
                              TransportType = ti.TransportType,
                              AgeTransport = DateTime.Now.Year - ti.ReleaseYear,
                              Horsepower = ti.Horsepower,
                              PeriodInMonths = ti.PeriodInMonths,
                              TaxRate = ttr.TaxRate,
                              Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                          };
            var taxDriver = from ti in taxInfo
                            group ti by ti.DriverPassport into td
                            orderby td.Sum(t => t.Tax)
                            select new DriverTax
                            {
                                DriverPassport = td.Key,
                                DriverLastname = td.First().DriverLastname,
                                DriverName = td.First().DriverName,
                                DriverPatronymic = td.First().DriverPatronymic,
                                DriverBirthday = td.First().DriverBirthday,
                                CountTransport = td.Count(),
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
