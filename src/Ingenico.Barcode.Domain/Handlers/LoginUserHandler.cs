using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Handlers {
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse> {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken) {
            var result = await _userRepository.LoginUserAsync(request.Email, request.Password);
            return new LoginUserResponse {
                Success = result,
                Message = result ? "User logged in successfully." : "Invalid login attempt."
            };
        }
    }
}
