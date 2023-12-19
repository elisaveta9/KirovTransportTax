using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    internal class TransportModelRepository : ITransportModelRepository
    {
        private readonly TransportDbConnection dbContext = new();
        private readonly Mapper mapper = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<TransportModel, TransportModelDbModel>();
        }));

        public void BeginTransaction()
        {
            dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            dbContext.CommitTransaction();
        }

        public async Task<int> Create(TransportModel entity)
        {
            var model = mapper.Map<TransportModelDbModel>(entity);
            var addedRows = await dbContext.TransportModelDbs
                .Value(tm => tm, model)
                .InsertAsync();
            return addedRows;
        }

        public async void Delete(TransportModel entity)
        {
            var model = mapper.Map<TransportModelDbModel>(entity);
            await dbContext.TransportModelDbs
                .Where(tm => tm.Equals(model))
                .DeleteAsync();
        }

        public async void DeleteByModel(string modelPK)
        {
            await dbContext.TransportModelDbs
                .Where(tm => tm.Model.Equals(modelPK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<TransportModel>> GetAll()
        {
            var transportModels = await dbContext.TransportModelDbs.ToListAsync();
            return transportModels.ConvertAll(tm => mapper.Map<TransportModel>(tm));
        }

        public async Task<TransportModel> GetModel(string modelPK)
        {
            var transportModel = await dbContext.TransportModelDbs
                .Where(tm => tm.Model.Equals(modelPK))
                .FirstOrDefaultAsync();
            return mapper.Map<TransportModel>(transportModel);
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async void Update(TransportModel entity)
        {
            var model = mapper.Map<TransportModelDbModel>(entity);
            await dbContext.UpdateAsync(model);
        }

        public async void Update(string oldModelPK, TransportModel entity)
        {
            var model = mapper.Map<TransportModelDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.TransportModelDbs
                    .Where(tm => tm.Model.Equals(oldModelPK))
                    .Set(tm => tm.Model, model.Model)
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
