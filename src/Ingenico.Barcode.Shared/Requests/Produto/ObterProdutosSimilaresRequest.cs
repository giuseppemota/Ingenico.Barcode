using Ingenico.Barcode.Shared.DTOs;
using MediatR;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Shared.Requests;

public class ObterProdutosSimilaresRequest : IRequest<Result<List<ProdutoResponseDTO>>>
{
    public Guid ProdutoId { get; set; }
}

