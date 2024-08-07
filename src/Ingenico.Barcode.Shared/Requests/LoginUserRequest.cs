using Ingenico.Barcode.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Shared.Requests {
    public class LoginUserRequest : IRequest<LoginUserResponse> {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
