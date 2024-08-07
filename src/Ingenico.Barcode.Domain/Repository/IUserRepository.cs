using Ingenico.Barcode.Domain.Entites;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IUserRepository {
        Task<bool> RegisterUserAsync(User user, string password);
        Task<bool> LoginUserAsync(string email, string password);
    }
}
