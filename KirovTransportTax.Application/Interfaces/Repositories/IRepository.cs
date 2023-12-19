namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        Task<IEnumerable<T>> GetAll();
    }
}
