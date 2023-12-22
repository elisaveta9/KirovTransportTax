using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class TransportModelRepository : ITransportModelRepository
    {
        private readonly TransportDbConnection dbContext;
        private readonly Mapper mapperFrom = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<TransportModel, TransportModelDbModel>(); 
        }));
        private readonly Mapper mapperTo = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<TransportModelDbModel, TransportModel>()
            .ForMember(nameof(TransportModel.Brand), opt => opt.MapFrom(src => src.Brand))
            .ForMember(nameof(TransportModel.TransportType), opt => opt.MapFrom(src => src.TransportType));
        }));

        public TransportModelRepository(TransportDbConnection dbContext)
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

        public async Task<int> Create(TransportModel entity)
        {
            var model = mapperFrom.Map<TransportModelDbModel>(entity);
            return await dbContext.InsertAsync(model);
        }

        public async Task<int> Delete(TransportModel entity)
        {
            var model = mapperFrom.Map<TransportModelDbModel>(entity);
            return await dbContext.TransportModelDbs
                .Where(tm => tm.Equals(model))
                .DeleteAsync();
        }

        public async Task<int> DeleteByModel(string modelPK)
        {
            return await dbContext.TransportModelDbs
                .Where(tm => tm.Model.Equals(modelPK))
                .DeleteAsync();
        }

        public async Task<IEnumerable<TransportModel>> GetAll()
        {
            var models = await dbContext.TransportModelDbs.ToListAsync();
            return models.ConvertAll(m => mapperTo.Map<TransportModel>(m));
        }

        public async Task<TransportModel> GetModel(string modelPK)
        {
            var transportModel = await dbContext.TransportModelDbs
                .Where(tm => tm.Model.Equals(modelPK))
                .FirstOrDefaultAsync();
            return mapperTo.Map<TransportModel>(transportModel);
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async Task<int> Update(TransportModel entity)
        {
            var model = mapperFrom.Map<TransportModelDbModel>(entity);
            return await dbContext.UpdateAsync(model);
        }

        public async Task<int> Update(string oldModelPK, TransportModel entity)
        {
            var model = mapperFrom.Map<TransportModelDbModel>(entity);
            BeginTransaction();
            try
            {
                await dbContext.TransportModelDbs
                    .Where(tm => tm.Model.Equals(oldModelPK))
                    .Set(tm => tm.Model, model.Model)
                    .UpdateAsync();
                var updatedRows = await dbContext.UpdateAsync(model);
                CommitTransaction();
                return updatedRows;
            } catch
            {
                RollbackTransaction();
                return 0;
            }
        }
    }
}
