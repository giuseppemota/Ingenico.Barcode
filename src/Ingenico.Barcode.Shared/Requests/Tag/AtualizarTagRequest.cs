﻿using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;


namespace Ingenico.Barcode.Shared.Requests {
    public class AtualizarTagRequest : IRequest<Result<AtualizarTagResponse>> {
        public int IdTag { get; set; }
        public string NomeTag { get; set; } = default!;
    }
}