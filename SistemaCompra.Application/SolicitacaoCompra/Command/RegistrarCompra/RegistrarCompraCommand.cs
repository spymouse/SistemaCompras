using MediatR;
using Newtonsoft.Json;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;


namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public IList<Item> Itens { get; set; }
        [JsonIgnore]
        public decimal TotalGeral { get; set; }

    }
}
