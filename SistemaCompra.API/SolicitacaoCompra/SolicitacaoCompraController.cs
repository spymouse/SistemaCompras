using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using System.Collections.Generic;

using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using produtoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : Controller
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }
    
        [HttpPost, Route("solicitacaocompra/registrarcompra")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult RegistrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            _mediator.Send(registrarCompraCommand);
            return StatusCode(201);
        }

        #region TESTE
#if DEBUG

        [HttpGet, Route("solicitacaocompra/Teste")]
        public IActionResult Teste()
        {
            var cmd = new Application.SolicitacaoCompra.Command.RegistrarCompra.RegistrarCompraCommand();
            cmd.NomeFornecedor = "EBER-TECNOLOGIA";
            cmd.UsuarioSolicitante = "EBER.ALVES";
            cmd.Itens = GerarItens(5);
            var json = JsonConvert.SerializeObject(cmd);
            _mediator.Send(cmd);
            return Ok("200");
        }

        private IList<SolicitacaoCompraAgg.Item> GerarItens(int qtd)
        {
            List<SolicitacaoCompraAgg.Item> list = new List<SolicitacaoCompraAgg.Item>();

            for (int i = 1; i < qtd; i++)
            {
                var produto = new produtoAgg.Produto("Cedro", "Transversal 3/3", produtoAgg.Categoria.Madeira.ToString(), 1001);
                list.Add(new SolicitacaoCompraAgg.Item(produto, i+1));
            }
            return list;
        }

#endif
        #endregion


    }
}
