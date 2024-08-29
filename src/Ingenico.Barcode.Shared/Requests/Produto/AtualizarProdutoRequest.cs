using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;
using System;
using System.Collections.Generic;

namespace Ingenico.Barcode.Shared.Requests
{
    public class AtualizarProdutoRequest : IRequest<Result<AtualizarProdutoResponse>>
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public string UnidadeMedida { get; set; }
        public string Ingredientes { get; set; }
        public string PaisOrigem { get; set; }
        public DateTime Validade { get; set; }
        public List<AtualizarCategoriaRequest> Categorias { get; set; }
        public List<AtualizarTagRequest> Tags { get; set; }
    }


}
