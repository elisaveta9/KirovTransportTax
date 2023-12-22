﻿using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class TransportTaxRepository : ITransportTaxRepository
    {
        private readonly TransportDbConnection dbContext;

        public TransportTaxRepository(TransportDbConnection dbContext)
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

        public async Task<IEnumerable<TransportTax>> GetAll()
        {
            var tr = dbContext.TransportTaxRateDbs.ToList();
            var transportInfo = from t in dbContext.TransportDb
                                 join tm in dbContext.TransportModelDbs on t.Model equals tm.Model
                                 select new
                                 {
                                     t.NumberTransport,
                                     tm.Type,
                                     t.DriverPassport,
                                     tm.Horsepower,
                                     PeriodInMonths = t.RegistrationDate.Year != DateTime.Now.Year ? 12 :
                                                      t.RegistrationDate.Day > 15 ? 12 - t.RegistrationDate.Month :
                                                                                    12 - t.RegistrationDate.Month + 1
                                 };
            var taxInfo = from ti in transportInfo
                          from ttr in dbContext.TransportTaxRateDbs.Where(ttr => ti.Type.Equals(ttr.Type)
                          && ti.Horsepower > ttr.MinHorsepower && 
                          (ttr.MaxHorsepower == 0 || ti.Horsepower <= ttr.MaxHorsepower))
                           select new TransportTax
                           {
                               NumberTransport = ti.NumberTransport,
                               TransportType = ti.Type,
                               Driver = ti.DriverPassport,
                               Horsepower = ti.Horsepower,
                               PeriodInMonths = ti.PeriodInMonths,
                               TaxRate = ttr.TaxRate,
                               Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                           };
            return taxInfo.OrderByDescending(t => t.Tax);
        }

        public async Task<TransportTax?> GetByNumber(string numberTransportPK)
        {
            var transportInfo = from t in dbContext.TransportDb.Where(t => t.NumberTransport.Equals(numberTransportPK))
                                join tm in dbContext.TransportModelDbs on t.Model equals tm.Model
                                select new
                                {
                                    t.NumberTransport,
                                    tm.Type,
                                    t.DriverPassport,
                                    tm.Horsepower,
                                    PeriodInMonths = t.RegistrationDate.Year != DateTime.Now.Year ? 12 :
                                                     t.RegistrationDate.Day > 15 ? 12 - t.RegistrationDate.Month :
                                                                                   12 - t.RegistrationDate.Month + 1
                                };
            var transport = await transportInfo.FirstOrDefaultAsync();
            if (transport == null)
                return null;
            var taxInfo = from ti in transportInfo
                          from ttr in dbContext.TransportTaxRateDbs.Where(ttr => ti.Type.Equals(ttr.Type)
                          && ti.Horsepower > ttr.MinHorsepower &&
                          (ttr.MaxHorsepower == 0 || ti.Horsepower <= ttr.MaxHorsepower))
                          select new TransportTax
                          {
                              NumberTransport = ti.NumberTransport,
                              TransportType = ti.Type,
                              Driver = ti.DriverPassport,
                              Horsepower = ti.Horsepower,
                              PeriodInMonths = ti.PeriodInMonths,
                              TaxRate = ttr.TaxRate,
                              Tax = (float)Math.Round((double)ttr.TaxRate * ti.Horsepower * ti.PeriodInMonths / 12, 4)
                          };
            return await taxInfo.FirstOrDefaultAsync();
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }
    }
}
