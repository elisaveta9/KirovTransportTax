using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using KirovTransportTax.Infrastucture.POCOs;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    internal class BrandRepository : IBrandRepository
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

        public async Task<int> Create(Brand entity)
        {
            var model = mapper.Map<BrandDbModel>(entity);
            var addedRows = await dbContext.BrandDbs
                .Value(b => b, model)
                .InsertAsync();
            return addedRows;
        }

        public async void Delete(Brand entity)
        {
            var model = mapper.Map<BrandDbModel>(entity);
            await dbContext.DeleteAsync(model);
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            var brands = await dbContext.BrandDbs.ToListAsync();
            return brands.ConvertAll(b => mapper.Map<Brand>(b));
        }

        public void RollbackTransaction()
        {
            dbContext.RollbackTransaction();
        }

        public async void Update(string oldBrandPK, Brand entity)
        {
            await dbContext.BrandDbs
                .Where(b => b.Name.Equals(oldBrandPK))
                .Set(b => b.Name, entity.Name)
                .UpdateAsync();
        }
    }
}
