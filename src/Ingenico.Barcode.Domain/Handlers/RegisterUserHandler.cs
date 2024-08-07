using Ingenico.Barcode.Domain.Entites;
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
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse> {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken) {
            var user = new User {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userRepository.RegisterUserAsync(user, request.Password);
            return new RegisterUserResponse {
                Success = result,
                Message = result ? "User registered successfully." : "User registration failed."
            };
        }
    }
}
