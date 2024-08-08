using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Handlers {

    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Result<RegisterUserResponse>> {

        private readonly UserManager<IdentityUser> userManager;

        public RegisterUserHandler(UserManager<IdentityUser> userManager) {
            this.userManager = userManager;
        }

        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken) {
            var user = new IdentityUser {
                UserName = request.Email,
                Email = request.Email
            };

            // Armazena os dados do usuário na tabela AspNetUsers
            var result = await userManager.CreateAsync(user, request.Password);

            // Se o usuário foi criado com sucesso, faz o login do usuário

            return new RegisterUserResponse(result.Succeeded);

            
          
        }
    }
}
