using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : SolicitacaoCompraAgg.ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            this.context = context;
        }

        public void RegistrarCompra(SolicitacaoCompraAgg.SolicitacaoCompra solicitacaoCompra)
        {
            context.Set<SolicitacaoCompraAgg.SolicitacaoCompra>().Add(solicitacaoCompra);
        }
    }
}
