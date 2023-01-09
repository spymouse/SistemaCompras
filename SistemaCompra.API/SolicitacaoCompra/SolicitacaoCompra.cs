using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompra : Controller
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompra(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost, Route("produto/registrarcompra")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult RegistrarCompra([FromBody] RegistrarCompraCommand RegistrarCompraCommand)
        {
            _mediator.Send(RegistrarCompraCommand);
            return StatusCode(201);
        }
    }
}
