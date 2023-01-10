using MediatR;
using Newtonsoft.Json;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante,request.NomeFornecedor);
            solicitacaoCompra.RegistrarCompra(request.Itens);

            this.solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);
            
            Commit();
            PublishEvents(solicitacaoCompra.Events);
            return Task.FromResult(true);
        }

    }
}
