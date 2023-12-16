using KirovTransportTax.Application.Interfaces;
using KirovTransportTax.Core.Entities;
using LinqToDB;

namespace KirovTransportTax.Infrastucture.Repositories
{
    internal class BrandRepository : IBrandRepository
    {
        private TransportDbConnection dbContext = new TransportDbConnection();

        public Task Create(Brand entity)
        {
            dbContext.brandDbs
                .Value(p => p.Name, entity.Name)
                .Insert();
            throw new NotImplementedException();
        }

        public void Delete(Brand entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Brand>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(string oldBrandPK, Brand entity)
        {
            throw new NotImplementedException();
        }
    }
}
