using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    internal class DriverRepository : IDriverRepository
    {
        private readonly TransportDbConnection dbContext = new();
        private readonly Mapper mapper = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<Driver, DriverDbModel>();
        }));

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
            var model = mapper.Map<DriverDbModel>(entity);
            var addedRows = await dbContext.DriverDbs
                .Value(d => d, model)
                .InsertAsync();
            return addedRows;
        }

        public async void Delete(Driver entity)
        {
            var model = mapper.Map<DriverDbModel>(entity);
            await dbContext.DriverDbs
                .Where(d => d.Equals(model))
                .DeleteAsync();
        }

        public async void DeleteByPassport(string passportPK)
        {
            await dbContext.DriverDbs
                .Where(d => d.Passport.Equals(passportPK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var drivers = await dbContext.DriverDbs.ToListAsync();
            return drivers.ConvertAll(d => mapper.Map<Driver>(d));
        }

        public async Task<Driver> GetByPassport(string passportPK)
        {
            var driver = await dbContext.DriverDbs
                .Where(d => d.Passport.Equals(passportPK))
                .FirstOrDefaultAsync();
            return mapper.Map<Driver>(driver);
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async void Update(Driver entity)
        {
            var model = mapper.Map<DriverDbModel>(entity);
            await dbContext.UpdateAsync(model);
        }

        public async void Update(string oldPassportPK, Driver entity)
        {
            var model = mapper.Map<DriverDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.DriverDbs
                    .Where(d => d.Passport.Equals(oldPassportPK))
                    .Set(d => d.Passport, entity.Passport)
                    .UpdateAsync();
                await dbContext.UpdateAsync(model);
                CommitTransaction();
            } catch
            {
                RollbackTransaction();
            }
        }
    }
}
