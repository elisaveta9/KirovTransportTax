using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;
using Npgsql;

namespace KirovTransportTax.Infrastucture.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly TransportDbConnection dbContext;
        private readonly Mapper mapperFrom = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<Brand, BrandDbModel>();
        }));
        private readonly Mapper mapperTo = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<BrandDbModel, Brand>();
        }));

        public BrandRepository(TransportDbConnection dbContext)
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

        public async Task<int> Create(Brand entity)
        {
            try
            {
                var model = mapperFrom.Map<BrandDbModel>(entity);
                var addedRows = await dbContext
                    .InsertAsync(model);
                return addedRows;
            } catch (NpgsqlException ex) 
            {
                if (ex.SqlState == "23505")
                    return 0;
                throw;
            }
        }

        public async Task<int> Delete(Brand entity)
        {
            var model = mapperFrom.Map<BrandDbModel>(entity);
            return await dbContext.DeleteAsync(model);
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            var brands = await dbContext.BrandDbs.ToListAsync();
            return brands.ConvertAll(b => mapperTo.Map<Brand>(b));
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async Task<int> Update(string oldBrandPK, Brand entity)
        {
            return await dbContext.BrandDbs
                .Where(b => b.Name.Equals(oldBrandPK))
                .Set(b => b.Name, entity.Name)
                .UpdateAsync();
        }
    }
}
