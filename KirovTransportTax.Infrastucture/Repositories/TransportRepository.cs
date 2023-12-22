using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    internal class TransportRepository : ITransportRepository
    {
        private readonly TransportDbConnection dbContext;
        private readonly Mapper mapper = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<Transport, TransportDbModel>();
        }));

        public TransportRepository(TransportDbConnection dbContext)
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

        public async Task<int> Create(Transport entity)
        {
            var model = mapper.Map<TransportDbModel>(entity);
            var addedRows = await dbContext.TransportDb
                .Value(t => t, model)
                .InsertAsync();
            return addedRows;
        }

        public async void Delete(Transport entity)
        {
            var model = mapper.Map<TransportDbModel>(entity);
            await dbContext.DeleteAsync(model);
        }

        public async void DeleteByNumber(string numberTransporPK)
        {
            await dbContext.TransportDb
                .Where(t => t.NumberTransport.Equals(numberTransporPK))
                .DeleteAsync();
        }

        public async void DeleteByPassport(string passportFK)
        {
            await dbContext.TransportDb
                .Where(t => t.DriverPassport.Equals(passportFK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<Transport>> GetAll()
        {
            var transports = await dbContext.TransportDb.ToListAsync();
            return transports.ConvertAll(t => mapper.Map<Transport>(t));
        }

        public async Task<Transport> GetByNumber(string numberTransporPK)
        {
            var transport = await dbContext.TransportDb
                .Where(t => t.NumberTransport.Equals(numberTransporPK))
                .FirstOrDefaultAsync();
            return mapper.Map<Transport>(transport);
        }

        public async Task<IEnumerable<Transport>> GetByPassport(string passportFK)
        {
            var transports = await dbContext.TransportDb
                .Where(t => t.DriverPassport.Equals(passportFK))
                .ToListAsync();
            return transports.ConvertAll(t => mapper.Map<Transport>(t));
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async void Update(Transport entity)
        {
            var model = mapper.Map<TransportDbModel>(entity);
            await dbContext.UpdateAsync(model);
        }

        public async void Update(string oldNumberTransporPK, Transport entity)
        {
            var model = mapper.Map<TransportDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.TransportDb
                    .Where(t => t.NumberTransport.Equals(oldNumberTransporPK))
                    .Set(t => t.NumberTransport, model.NumberTransport)
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
