using Ingenico.Barcode.Shared.Exceptions;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ingenico.Barcode.API.Controllers {

    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : BaseController {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]

        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterUserResponse>> RegisterPeopleAsync(
        [FromBody] RegisterUserRequest request) => await SendCommand(request);

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
      
        public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(
            [FromBody] LoginUserRequest request) => await SendCommand(request);
    }
}
