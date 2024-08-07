using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Microsoft.AspNetCore.Identity;
;

namespace Ingenico.Barcode.Data.Repositorio {
    public class UserRepository : IUserRepository {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(User user, string password) {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> LoginUserAsync(string email, string password) {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
