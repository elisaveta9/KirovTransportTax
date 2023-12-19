using System.Linq.Expressions;

namespace KirovTransportTax.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        public Task SaveChanges();
    }
}
