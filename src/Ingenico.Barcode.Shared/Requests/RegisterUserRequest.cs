using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Shared.Requests {
    public class RegisterUserRequest : IRequest<Result<RegisterUserResponse>> {


        public string Email { get; set; }

        public string Password { get; set; }

        /*
        [DataType(DataType.Password)]
        //[Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmPassword { get; set; }
        */
    }
}
