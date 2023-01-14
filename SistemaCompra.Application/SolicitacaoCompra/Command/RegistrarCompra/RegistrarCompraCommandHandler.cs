using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository produtoAggRepository;

        public RegistrarCompraCommandHandler(SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, ProdutoAgg.IProdutoRepository produtoAggRepository,IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
            this.produtoAggRepository = produtoAggRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante,request.NomeFornecedor);

            for (int i = 0; i < request.Itens.Count; i++)
            {
                request.Itens[i].Produto = this.produtoAggRepository.Obter(request.Itens[0].Produto.Id);
            }

            solicitacaoCompra.RegistrarCompra(request.Itens);

            this.solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);
            
            Commit();
            PublishEvents(solicitacaoCompra.Events);
            return Task.FromResult(true);
        }

    }
}
