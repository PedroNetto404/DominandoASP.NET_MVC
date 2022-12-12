using ASP_NET_MVC5_DEV_IO.Business.Core.Notificacoes;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.DataAbstraction;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Services;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.DataAbstrations;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Services;
using ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Context;
using ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Repositories;

namespace ASPNET_MVC5_DEV_IO.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>(); 
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IFornecedorService, FornecedorService>();

        services.AddScoped<INotificador, Notificador>(); 
    }
}