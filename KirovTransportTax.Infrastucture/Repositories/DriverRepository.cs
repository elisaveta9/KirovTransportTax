using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;
using Npgsql;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly TransportDbConnection dbContext;
        private readonly Mapper mapperFrom = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<Driver, DriverDbModel>();
        }));
        private readonly Mapper mapperTo = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<DriverDbModel, Driver>();
        }));

        public DriverRepository(TransportDbConnection dbContext)
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

        public async Task<int> Create(Driver entity)
        {
            try
            {
                var model = mapperFrom.Map<DriverDbModel>(entity);
                return await dbContext
                    .InsertAsync(model);
            } catch (NpgsqlException ex)
            {
                if (ex.SqlState == "23505")
                    return 0;
                throw;
            }
        }

        public async Task<int> Delete(Driver entity)
        {
            var model = mapperFrom.Map<DriverDbModel>(entity);
            return await dbContext.DriverDbs
                .Where(d => d.Equals(model))
                .DeleteAsync();
        }

        public async Task<int> DeleteByPassport(string passportPK)
        {
            return await dbContext.DriverDbs
                .Where(d => d.Passport.Equals(passportPK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var drivers = await dbContext.DriverDbs.ToListAsync();
            return drivers.ConvertAll(d => mapperTo.Map<Driver>(d));
        }

        public async Task<Driver> GetByPassport(string passportPK)
        {
            var driver = await dbContext.DriverDbs
                .Where(d => d.Passport.Equals(passportPK))
                .FirstOrDefaultAsync();
            return mapperTo.Map<Driver>(driver);
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async Task<int> Update(Driver entity)
        {
            var model = mapperFrom.Map<DriverDbModel>(entity);
            return await dbContext.UpdateAsync(model);
        }

        public async Task<int> Update(string oldPassportPK, Driver entity)
        {
            var model = mapperFrom.Map<DriverDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.DriverDbs
                    .Where(d => d.Passport.Equals(oldPassportPK))
                    .Set(d => d.Passport, entity.Passport)
                    .UpdateAsync();
                var updatesRows = await dbContext.UpdateAsync(model);
                CommitTransaction();
                return updatesRows;
            } catch
            {
                RollbackTransaction();
                return 0;
            }
        }
    }
}
