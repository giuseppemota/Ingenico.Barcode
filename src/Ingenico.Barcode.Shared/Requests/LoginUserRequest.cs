using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;
using System.ComponentModel.DataAnnotations;


namespace Ingenico.Barcode.Shared.Requests {
    public class LoginUserRequest : IRequest <Result<LoginUserResponse>> {

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
