using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Responses {
    public class RegisterUserResponse {
        public RegisterUserResponse(bool success) {
            Success = success;
        }

        public bool Success {  get; set; }
    }
}
