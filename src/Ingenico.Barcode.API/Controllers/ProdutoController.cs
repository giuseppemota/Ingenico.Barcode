using Ingenico.Barcode.Data.Repositorios;
using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ingenico.Barcode.API.Controllers {
    [ApiController]
    [Route("[controller]/v1/Produtos")]
    // [Authorize]
    public class ProdutoController : BaseController {
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IMediator mediator, ILogger<ProdutoController> logger) : base(mediator) {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ObterTodosProdutosResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ObterTodosProdutosResponse>> ObterTodosProdutosAsync() {
            _logger.LogInformation("Obtendo todos produtos");
            return await SendCommand(new ObterTodosProdutosRequest());
        }

        [HttpGet("{ProdutoId}")]
        [ProducesResponseType(typeof(ObterProdutoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ObterProdutoResponse>> ObterProdutoAsync([FromRoute] Guid ProdutoId) {
            _logger.LogInformation($"Obtendo a produto {ProdutoId}");
            return await SendCommand(new ObterProdutoRequest() { ProdutoId = ProdutoId });
        }

        [HttpPost]
        [Consumes("multipart/form-data")] // Indica que o endpoint recebe multipart/form-data
        [ProducesResponseType(typeof(CadastrarProdutoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<CadastrarProdutoResponse>> CadastrarProdutoAsync([FromForm] CadastrarProdutoRequest request) {
            _logger.LogInformation("Cadastrando produto novo ");
            return await SendCommand(request);
        }



        [HttpPut]
        [ProducesResponseType(typeof(AtualizarProdutoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<AtualizarProdutoResponse>> AtualizarProdutoAsync([FromBody] AtualizarProdutoRequest request) {
            _logger.LogInformation($"Atualizando produto {request.ProdutoId}");
            return await SendCommand(request);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ExcluirProdutoResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ExcluirProdutoResponse>> ExcluirProdutoAsync([FromBody] ExcluirProdutoRequest request) {
            _logger.LogInformation($"Excluindo produto {request.ProdutoId}");
            return await SendCommand(request);
        }
    }
}
