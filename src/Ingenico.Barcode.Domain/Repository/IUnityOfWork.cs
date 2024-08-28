

namespace Ingenico.Barcode.Domain.Repository {
    public interface IUnitOfWork : IDisposable {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        IRepository<T> Repository<T>() where T : class;
    }

}
