using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SistemaCompra.Application.Produto;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.UoW;
using System;

namespace SistemaCompra.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var assembly = AppDomain.CurrentDomain.Load("SistemaCompra.Application");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);
            services.AddSignalR();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<SistemaCompraContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), 
                    o=> o.MigrationsAssembly("SistemaCompra.API"))
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prova Sisprev", Version = "v1" });
            });          

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prova Sisprev V1");
            });
        }
    }
}
